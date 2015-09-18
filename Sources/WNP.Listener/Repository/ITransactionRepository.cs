using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMSLLC.Listener.Domain;
using AMSLLC.Listener.Domain.Listener.Transaction;

namespace Repository
{
    public interface ITransactionRepository : IRepository
    {
        TransactionConfigurationMemento Get(int transactionId);
        TransactionExecutionMemento Get(int sourceApplicationId, int destinationApplicationId, string operationKey);
    }
}
