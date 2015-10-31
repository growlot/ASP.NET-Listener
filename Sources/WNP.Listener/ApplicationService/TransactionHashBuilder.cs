// <copyright file="TransactionHashBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Implement <see cref="ITransactionHashBuilder"/>
    /// </summary>
    public class TransactionHashBuilder : ITransactionHashBuilder
    {
        /// <inheritdoc/>
        public string Create(
            Dictionary<object, FieldConfigurationCollection> elements,
            Func<FieldConfiguration, short?> hashSequenceSelector)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(
                    nameof(elements),
                    "Can not process data there are no hash elements.");
            }

            if (elements.Any(e => e.Value == null))
            {
                throw new ArgumentException(
                    "All hash elements must have field configuration specified");
            }

            if (hashSequenceSelector == null)
            {
                throw new ArgumentNullException(nameof(hashSequenceSelector), "Hash sequence selector is required");
            }

            List<string> hashCollection = new List<string>();

            foreach (var element in elements)
            {
                var data = element.Key;
                var fieldConfigurations = element.Value;

                object workData = data;
                var value = data as string;
                if (value != null)
                {
                    workData = JsonConvert.DeserializeObject<ExpandoObject>(value);
                }

                Dictionary<short, object> hashElements = new Dictionary<short, object>();
                foreach (var fieldConfiguration in fieldConfigurations)
                {
                    var seq = hashSequenceSelector(fieldConfiguration);
                    if (seq.HasValue)
                    {
                        DynamicUtilities.ProcessProperty(workData, fieldConfiguration.Name, null, (
                            targetProperty,
                            owner,
                            propRef) =>
                        {
                            var targetValue = targetProperty.GetValue(owner);
                            hashElements.Add(seq.Value, targetValue);
                        });
                    }
                }

                List<KeyValuePair<short, object>> hashSorted =
                   hashElements.Where(s => s.Value != null).OrderBy(s => s.Key).ToList();

                StringBuilder hashSourceBuilder = new StringBuilder();
                for (int i = 0; i < hashSorted.Count; i++)
                {
                    var hashElement = hashSorted[i].Value;
                    hashSourceBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}_", JsonConvert.SerializeObject(hashElement));
                }

                hashCollection.Add(hashSourceBuilder.ToString());
            }

            return GetHash(string.Join("#", hashCollection.OrderBy(s => s)));
        }

        /// <summary>
        /// Gets the SHA-1 hash.
        /// </summary>
        /// <param name="data">The data that needs to be hashed.</param>
        /// <returns>The has of the data</returns>
        private static string GetHash(string data)
        {
            byte[] hash;

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            }

            string result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }
    }
}