// <copyright file="SummaryBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Implements <see cref="ISummaryBuilder" />
    /// </summary>
    public class SummaryBuilder : ISummaryBuilder
    {
        /// <summary>
        /// Builds the summary from specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="summary">The summary generation function.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        public void Build(object data, Dictionary<string, object> summary, IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            object workData = data;
            var value = data as string;
            if (value != null)
            {
                workData = JsonConvert.DeserializeObject<ExpandoObject>(value);
            }

            if (fieldConfigurations != null)
            {
                foreach (var fieldConfiguration in fieldConfigurations.Where(f => f.IncludeInSummary))
                {
                    DynamicUtilities.ProcessProperty(
                        workData,
                        fieldConfiguration.Name,
                        null,
                        (targetProperty, owner, propRef) =>
                        {
                            var targetValue = targetProperty.GetValue(owner);

                            summary.Add(fieldConfiguration.Name, targetValue);
                        });
                }
            }
        }
    }
}
