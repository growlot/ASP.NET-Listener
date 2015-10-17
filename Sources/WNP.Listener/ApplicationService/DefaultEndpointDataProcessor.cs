// //-----------------------------------------------------------------------
// // <copyright file="DefaultEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Utilities;

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
            returnValue.Data = remap ? DynamicUtilities.ConvertToDynamic(data) : data;
            Dictionary<string, object> hashElements = new Dictionary<string, object>();
            foreach (var fieldConfiguration in configuration.FieldConfigurations)
            {
                DynamicUtilities.ProcessProperty(data, fieldConfiguration.Name, null,
                    (targetProperty, owner, propRef) =>
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
                            DynamicUtilities.RemoveProperty(returnValue.Data, propRef);
                            int[] sequence =
                                Regex.Matches(propRef, @"\[([^]]*)\]")
                                    .Cast<Match>()
                                    .Select(x => int.Parse(x.Groups[1].Value))
                                    .ToArray();

                            DynamicUtilities.AddPropertyAs(returnValue.Data, targetValue, fieldConfiguration.MapToName,
                                sequence, 0);
                        }
                        else
                        {
                            //var converter = TypeDescriptor.GetConverter(targetProperty.PropertyType);
                            targetProperty.SetValue(owner, targetValue);
                                //converter.ConvertFrom(targetValue?.ToString())
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
    }
}