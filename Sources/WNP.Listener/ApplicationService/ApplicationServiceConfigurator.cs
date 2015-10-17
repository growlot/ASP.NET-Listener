using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ApplicationService
{
    using Communication;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;

    public class ApplicationServiceConfigurator
    {
        public void Configure()
        {
            EventsRegister.RegisterAsync<TransactionSkipped>(
                msg => ApplicationIntegration.DependencyResolver.ResolveType<ITransactionService>()
                        .Skipped(new TransactionSkippedRequestMessage { RecordKey = msg.RecordKey }));
        }
    }
}
