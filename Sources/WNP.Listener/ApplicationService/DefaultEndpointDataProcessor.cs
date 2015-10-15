// //-----------------------------------------------------------------------
// // <copyright file="DefaultEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using Core;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Default endpoint data processor
    /// </summary>
    public class DefaultEndpointDataProcessor : IEndpointDataProcessor
    {
        /// <summary>
        /// Prepare data for the endpoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="configuration">The endpoint configuration.</param>
        /// <returns>Task.</returns>
        public virtual IEndpointDataProcessorResult Process(object data, IntegrationEndpointConfiguration configuration)
        {
            IEndpointDataProcessorResult returnValue = new EndpointDataProcessorResult();
            bool remap = configuration.FieldConfigurations.Any(f => !string.IsNullOrWhiteSpace(f.MapToName));
            returnValue.Data = remap ? ConvertToDynamic(data) : data;
            Dictionary<string, object> hashElements = new Dictionary<string, object>();
            foreach (var fieldConfiguration in configuration.FieldConfigurations)
            {
                ProcessProperty(data, fieldConfiguration.Name, null, (targetProperty, owner, propRef) =>
                {
                    var targetValue = targetProperty.GetValue(owner);
                    var targetKey = targetValue?.ToString() ?? string.Empty;
                    if (fieldConfiguration.ValueMap != null && fieldConfiguration.ValueMap.ContainsKey(targetKey))
                    {
                        targetValue = fieldConfiguration.ValueMap[targetKey];
                    }

                    if (fieldConfiguration.IncludeInHash)
                    {
                        hashElements.Add(propRef, targetValue);
                    }

                    if (remap)
                    {
                        RemoveProperty(returnValue.Data, propRef);
                        int[] sequence =
                            Regex.Matches(propRef, @"\[([^]]*)\]")
                                .Cast<Match>()
                                .Select(x => int.Parse(x.Groups[1].Value))
                                .ToArray();

                        AddPropertyAs(returnValue.Data, targetValue, fieldConfiguration.MapToName, sequence, 0);
                    }
                    else
                    {
                        //var converter = TypeDescriptor.GetConverter(targetProperty.PropertyType);
                        targetProperty.SetValue(owner, targetValue); //converter.ConvertFrom(targetValue?.ToString())
                    }
                });
            }

            List<KeyValuePair<string, object>> hashSorted =
                hashElements.Where(s => s.Value != null).OrderBy(s => s.Key).ToList();
            StringBuilder hashSourceBuilder = new StringBuilder();
            for (int i = 0; i < hashSorted.Count; i++)
            {
                var hashElement = hashSorted[i].Value;
                hashSourceBuilder.AppendFormat("{0}_", JsonConvert.SerializeObject(hashElement));
            }

            returnValue.Hash = GetHash(hashSourceBuilder.ToString());

            return returnValue;
        }

        /// <summary>
        /// Gets the SHA-1 hash.
        /// </summary>
        /// <param name="data">The data that needs to be hashed.</param>
        /// <returns>The has of the data</returns>
        public static string GetHash(string data)
        {
            string result;
            byte[] hash;

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            }

            result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }

        private void ProcessProperty(object data, string name, string propRef,
            Action<DataPropertyInfo, object, string> propertyFoundCallback)
        {
            object owner = data;
            string[] namePieces = name.Split('.');
            string currentRef = propRef;
            for (int i = 0; i < namePieces.Length - 1; i++)
            {
                var namePiece = namePieces[i];
                currentRef = currentRef == null ? namePiece : "{0}.{1}".FormatWith(currentRef, namePiece);
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
                            ProcessProperty(o, string.Join(".", namePieces.Skip(i + 1)),
                                "{0}[{1}]".FormatWith(currentRef, j), propertyFoundCallback);
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
                                ProcessProperty(o, string.Join(".", namePieces.Skip(i + 1)),
                                    "{0}[{1}]".FormatWith(currentRef, j), propertyFoundCallback);
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
                currentRef = currentRef == null ? l : "{0}.{1}".FormatWith(currentRef, l);
                if (finalProperty != null)
                {
                    propertyFoundCallback(finalProperty, owner, currentRef);
                }
            }
        }

        private dynamic ConvertToDynamic<TType>(TType data)
        {
            var converter = new ExpandoObjectConverter();
            dynamic d = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(data), converter);
            return d;
        }

        private DataPropertyInfo FindProperty(object data, string namePiece)
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

        private void AddPropertyAs(dynamic expando, object currentValue, string mapTo, int[] sequence, int pos)
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

                    AddPropertyAs(asList[sequence[pos]], currentValue, string.Join(".", parts.Skip(i + 1)), sequence,
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

        private void RemoveProperty(dynamic expando, string propertyName)
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
                    RemoveProperty(o, string.Join(".", parts.Skip(i + 1)));
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