// <copyright file="SummaryBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ApplicationService
{
    using System.Dynamic;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;

    /// <summary>
    /// Implments <see cref="ISummaryBuilder"/>
    /// </summary>
    public class SummaryBuilder : ISummaryBuilder
    {
        /// <summary>
        /// Builds the summary from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="summary">The summary generation function.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Build(object data, Dictionary<string, object> summary, IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            object workData = data;
            var value = data as string;
            if (value != null)
            {
                workData = JsonConvert.DeserializeObject<ExpandoObject>(value);
            }


            foreach (var fieldConfiguration in fieldConfigurations)
            {
                DynamicUtilities.ProcessProperty(workData, fieldConfiguration.Name, null,
                    (targetProperty, owner, propRef) =>
                    {
                        var targetValue = targetProperty.GetValue(owner);
                        if (fieldConfiguration.HashSequence.HasValue)
                        {
                            summary.Add(fieldConfiguration.Name, targetValue);
                        }
                    });
            }
        }
    }
}
