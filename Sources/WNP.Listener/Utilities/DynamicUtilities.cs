// <copyright file="DynamicUtilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Utilities for dynamic data type
    /// </summary>
    public static class DynamicUtilities
    {
        /// <summary>
        /// Processes the property.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="name">The name.</param>
        /// <param name="propRef">The property reference.</param>
        /// <param name="propertyFoundCallback">The property found callback.</param>
        public static void ProcessProperty(object data, string name, string propRef, Action<DataPropertyInfo, object, string> propertyFoundCallback)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Can not process property if property name is not specified.");
            }

            object owner = data;
            string[] namePieces = name.Split('.');
            string currentRef = propRef;
            for (int i = 0; i < namePieces.Length - 1; i++)
            {
                var namePiece = namePieces[i];
                currentRef = currentRef == null ? namePiece : StringUtilities.Invariant($"{currentRef}.{namePiece}");
                if (owner == null)
                {
                    break;
                }

                DataPropertyInfo property = FindProperty(owner, namePiece);
                if (property.IsList)
                {
                    var originalList = property.GetValue(owner) as IList<object>;
                    if (originalList != null)
                    {
                        for (int j = 0; j < originalList.Count(); j++)
                        {
                            var o = originalList[j]; // .GetValue(j);
                            ProcessProperty(
                                o,
                                string.Join(".", namePieces.Skip(i + 1)),
                                StringUtilities.Invariant($"{currentRef}[{j}]"),
                                propertyFoundCallback);
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
                                ProcessProperty(
                                    o,
                                    string.Join(".", namePieces.Skip(i + 1)),
                                    StringUtilities.Invariant($"{currentRef}[{j}]"),
                                    propertyFoundCallback);
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
                currentRef = currentRef == null ? l : StringUtilities.Invariant($"{currentRef}.{l}");
                if (finalProperty != null && propertyFoundCallback != null)
                {
                    propertyFoundCallback(finalProperty, owner, currentRef);
                }
            }
        }

        /// <summary>
        /// Converts object to dynamic.
        /// </summary>
        /// <typeparam name="TType">The type of the data object.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The dynamic representation of data object.</returns>
        public static dynamic ConvertToDynamic<TType>(TType data)
        {
            var converter = new ExpandoObjectConverter();
            dynamic d = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(data), converter);
            return d;
        }

        /// <summary>
        /// Adds the property as.
        /// </summary>
        /// <param name="expando">The expando.</param>
        /// <param name="currentValue">The current value.</param>
        /// <param name="mapTo">The map to.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="index">The index.</param>
        /// <exception cref="System.Exception">Missing index</exception>
        public static void AddPropertyAs(dynamic expando, object currentValue, string mapTo, int[] sequence, int index)
        {
            if (mapTo == null)
            {
                throw new ArgumentNullException(nameof(mapTo), "Can not add property if possition is not specified.");
            }

            var expandoDictionary = expando as IDictionary<string, object>;
            if (expandoDictionary == null)
            {
                return;
            }

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
                    if (sequence == null)
                    {
                        throw new ArgumentNullException(nameof(sequence), "Sequence must be specified if adding to list.");
                    }

                    if (sequence.Length <= index)
                    {
                        throw new ArgumentException("Incorrect index specified", nameof(index));
                    }

                    if (asList.Count < sequence[index])
                    {
                        asList.Add(new ExpandoObject());
                    }

                    AddPropertyAs(
                        asList[sequence[index]],
                        currentValue,
                        string.Join(".", parts.Skip(i + 1)),
                        sequence,
                        index + 1);
                    return;
                }
                else
                {
                    currentPath = (ExpandoObject)currentPath[source];
                }
            }

            var name = parts.Last();
            if (currentPath.ContainsKey(name))
            {
                currentPath[name] = currentValue;
            }
            else
            {
                currentPath.Add(name, currentValue);
            }
        }

        /// <summary>
        /// Removes the property.
        /// </summary>
        /// <param name="expando">The expando.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="System.Exception">Array reference must have index resolved</exception>
        public static void RemoveProperty(dynamic expando, string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName), "Can not process property if property name is not specified.");
            }

            var expandoDictionary = expando as IDictionary<string, object>;
            if (expandoDictionary == null)
            {
                return;
            }

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
                    index = int.Parse(matches.Single(), CultureInfo.InvariantCulture);
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
                        throw new InvalidOperationException("Array reference must have index resolved");
                    }

                    var o = asList.ElementAt(index.Value);
                    RemoveProperty(o, string.Join((string)".", (IEnumerable<string>)parts.Skip(i + 1)));
                    return;
                }
                else
                {
                    currentPath = (ExpandoObject)currentPath[source];
                }
            }

            var name = parts.Last();
            if (currentPath.ContainsKey(name))
            {
                currentPath.Remove(name);
            }
        }

        /// <summary>
        /// Finds the property.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="namePiece">The name piece.</param>
        /// <returns>The property information.</returns>
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
        }
    }
}
