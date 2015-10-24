// //-----------------------------------------------------------------------
// <copyright file="TransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Implement <see cref="ITransactionKeyBuilder"/>
    /// </summary>
    public class TransactionKeyBuilder : ITransactionKeyBuilder
    {
        /// <inheritdoc/>
        public string Create(object data, IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            if (fieldConfigurations == null)
            {
                throw new ArgumentNullException(nameof(fieldConfigurations), "Can not process data if field configurations are not specified.");
            }

            object workData = data;
            var value = data as string;
            if (value != null)
            {
                workData = JsonConvert.DeserializeObject<ExpandoObject>(value);
            }

            Dictionary<short, object> keyElements = new Dictionary<short, object>();
            foreach (var fieldConfiguration in fieldConfigurations)
            {
                DynamicUtilities.ProcessProperty(
                    workData,
                    fieldConfiguration.Name,
                    null,
                    (targetProperty, owner, propRef) =>
                    {
                        var targetValue = targetProperty.GetValue(owner);
                        if (fieldConfiguration.HashSequence.HasValue)
                        {
                            keyElements.Add(fieldConfiguration.HashSequence.Value, targetValue);
                        }
                    });
            }

            List<KeyValuePair<short, object>> keySorted =
                keyElements.Where(s => s.Value != null).OrderBy(s => s.Key).ToList();

            List<string> keyBuilder = new List<string>();
            for (int i = 0; i < keySorted.Count; i++)
            {
                var keyElement = keySorted[i].Value;
                keyBuilder.Add(keyElement?.ToString());
            }

            return string.Join("#", keyBuilder);
        }
    }
}