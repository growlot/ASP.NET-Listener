namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class DynamicUtilities
    {
        public static void ProcessProperty(object data, string name, string propRef,
            Action<DataPropertyInfo, object, string> propertyFoundCallback)
        {
            object owner = data;
            string[] namePieces = name.Split('.');
            string currentRef = propRef;
            for (int i = 0; i < namePieces.Length - 1; i++)
            {
                var namePiece = namePieces[i];
                currentRef = currentRef == null ? namePiece : $"{currentRef}.{namePiece}";
                if (owner == null)
                    break;
                DataPropertyInfo property = FindProperty(owner, namePiece);
                if (property.IsList)
                {
                    var originalList = property.GetValue(owner) as IList<object>;
                    if (originalList != null)
                    {
                        for (int j = 0; j < originalList.Count(); j++)
                        {
                            var o = originalList[j]; //.GetValue(j);
                            ProcessProperty(o, string.Join((string) ".", (IEnumerable<string>) namePieces.Skip(i + 1)),
                                $"{currentRef}[{j}]", propertyFoundCallback);
                        }
                    }
                    else
                    {
                        var originalarray = property.GetValue(owner) as Array;
                        if (originalarray != null)
                        {
                            for (int j = 0; j < originalarray.Length; j++)
                            {
                                var o = originalarray.GetValue(j);
                                ProcessProperty(o, string.Join((string) ".", (IEnumerable<string>) namePieces.Skip(i + 1)),
                                    $"{currentRef}[{j}]", propertyFoundCallback);
                            }
                        }
                    }
                    return;
                }
                else
                {
                    owner = property.GetValue(owner);
                }
            }
            if (owner != null)
            {
                var l = namePieces.Last();
                var finalProperty = FindProperty(owner, l);
                currentRef = currentRef == null ? l : $"{currentRef}.{l}";
                if (finalProperty != null)
                {
                    propertyFoundCallback(finalProperty, owner, currentRef);
                }
            }
        }

        public static dynamic ConvertToDynamic<TType>(TType data)
        {
            var converter = new ExpandoObjectConverter();
            dynamic d = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(data), converter);
            return d;
        }

        private static DataPropertyInfo FindProperty(object data, string namePiece)
        {
            PropertyInfo prop = null;
            var expandoDictionary = data as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                if (expandoDictionary.ContainsKey(namePiece))
                {
                    return new DataPropertyInfo(namePiece, expandoDictionary[namePiece] is IList);
                }
                return null;
            }
            var type = data.GetType();
            prop = type.GetProperty(namePiece);
            return new DataPropertyInfo(prop.Name, prop.PropertyType.IsArray);
            //return null;
        }

        public static void AddPropertyAs(dynamic expando, object currentValue, string mapTo, int[] sequence, int pos)
        {
            var expandoDictionary = expando as IDictionary<string, object>;
            if (expandoDictionary == null)
                return;
            var parts = mapTo.Split('.');
            var currentPath = expandoDictionary;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var source = parts[i];
                var indexer = source.IndexOf("[]", StringComparison.InvariantCulture);

                if (indexer > -1)
                {
                    source = source.Substring(0, source.IndexOf("[", StringComparison.InvariantCulture));
                }

                if (!currentPath.ContainsKey(source))
                {
                    if (indexer > -1)
                    {
                        currentPath[source] = new List<ExpandoObject>();
                    }
                    else
                    {
                        currentPath[source] = new ExpandoObject();
                    }
                }

                var asList = currentPath[source] as List<object>;
                if (asList != null)
                {
                    if (sequence.Length <= pos)
                    {
                        throw new Exception("Missing index");
                    }

                    if (asList.Count < sequence[pos])
                    {
                        asList.Add(new ExpandoObject());
                    }

                    AddPropertyAs(asList[sequence[pos]], currentValue, string.Join((string) ".", (IEnumerable<string>) parts.Skip(i + 1)), sequence,
                        pos + 1);
                    return;
                }
                else
                {
                    currentPath = (ExpandoObject)currentPath[source];
                }
            }

            var name = parts.Last();
            if (currentPath.ContainsKey(name))
                currentPath[name] = currentValue;
            else
                currentPath.Add(name, currentValue);
        }

        public static void RemoveProperty(dynamic expando, string propertyName)
        {
            var expandoDictionary = expando as IDictionary<string, object>;
            if (expandoDictionary == null)
                return;
            var parts = propertyName.Split('.');
            var currentPath = expandoDictionary;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var source = parts[i];
                var matches = Regex.Matches(source, @"\[([^]]*)\]")
                    .Cast<Match>()
                    .Select(x => x.Groups[1].Value).ToList();

                int? index = null;
                if (matches.Count == 1)
                {
                    source = source.Substring(0, source.IndexOf("[", StringComparison.InvariantCulture));
                    index = int.Parse(matches.Single());
                }

                if (!currentPath.ContainsKey(source))
                {
                    return;
                }
                var asList = currentPath[source] as List<object>;
                if (asList != null)
                {
                    if (!index.HasValue)
                    {
                        throw new Exception("Array reference must have index resolved");
                    }
                    var o = asList.ElementAt(index.Value);
                    RemoveProperty(o, string.Join((string) ".", (IEnumerable<string>) parts.Skip(i + 1)));
                    return;
                }
                else
                {
                    currentPath = (ExpandoObject)currentPath[source];
                }
            }

            var name = parts.Last();
            if (currentPath.ContainsKey(name))
                currentPath.Remove(name);
        }
    }
}
