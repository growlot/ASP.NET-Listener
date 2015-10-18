// //-----------------------------------------------------------------------
// // <copyright file="TransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;

    public class TransactionKeyBuilder : ITransactionKeyBuilder
    {
        public string Create(object data, IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            object workData = data;
            var value = data as string;
            if (value != null)
            {
                workData = JsonConvert.DeserializeObject<ExpandoObject>(value);
            }


            Dictionary<short, object> keyElements = new Dictionary<short, object>();
            foreach (var fieldConfiguration in fieldConfigurations)
            {
                DynamicUtilities.ProcessProperty(workData, fieldConfiguration.Name, null,
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
                //keyBuilder.AppendFormat("{0}#", JsonConvert.SerializeObject(hashElement));
            }

            return string.Join("#", keyBuilder);
        }
    }
}