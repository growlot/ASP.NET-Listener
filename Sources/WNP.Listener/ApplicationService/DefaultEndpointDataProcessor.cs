// //-----------------------------------------------------------------------
// <copyright file="DefaultEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <returns>Task.</returns>
        public virtual IEndpointDataProcessorResult Process(object data, IList<FieldConfiguration> fieldConfigurations)
        {
            if (fieldConfigurations == null)
            {
                throw new ArgumentNullException(nameof(fieldConfigurations), "Can not process data if field configurations are not specified.");
            }

            IEndpointDataProcessorResult returnValue = new EndpointDataProcessorResult();
            bool remap = fieldConfigurations.Any(f => !string.IsNullOrWhiteSpace(f.MapToName));
            returnValue.Data = remap ? DynamicUtilities.ConvertToDynamic(data) : data;
            foreach (var fieldConfiguration in fieldConfigurations)
            {
                Action<DataPropertyInfo, object, string> action = (targetProperty, owner, propRef) =>
                {
                    var targetValue = targetProperty.GetValue(owner);
                    var targetKey = targetValue?.ToString() ?? string.Empty;
                    if (fieldConfiguration.ValueMap != null && fieldConfiguration.ValueMap.ContainsKey(targetKey))
                    {
                        targetValue = fieldConfiguration.ValueMap[targetKey];
                    }

                    if (remap)
                    {
                        DynamicUtilities.RemoveProperty(returnValue.Data, propRef);
                        int[] sequence =
                            Regex.Matches(propRef, @"\[([^]]*)\]")
                                .Cast<Match>()
                                .Select(x => int.Parse(x.Groups[1].Value, CultureInfo.InvariantCulture))
                                .ToArray();

                        DynamicUtilities.AddPropertyAs(returnValue.Data, targetValue, fieldConfiguration.MapToName, sequence, 0);
                    }
                    else
                    {
                        targetProperty.SetValue(owner, targetValue);
                    }
                };

                DynamicUtilities.ProcessProperty(data, fieldConfiguration.Name, null, action);
            }

            return returnValue;
        }
    }
}