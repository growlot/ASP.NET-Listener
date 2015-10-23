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

    public class SummaryBuilder : ISummaryBuilder
    {
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
