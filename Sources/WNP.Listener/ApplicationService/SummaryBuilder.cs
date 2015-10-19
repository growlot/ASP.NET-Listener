namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using Domain.Listener.Transaction;

    public class SummaryBuilder : ISummaryBuilder
    {
        public void Build(string data, Dictionary<string, object> summary, IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            throw new NotImplementedException();
        }
    }
}