using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Repository
{
    public interface ITransactionRepository : IRepository
    {
        TransactionConfigurationMemento Get(int transactionId);
        TransactionExecutionMemento Get(int sourceApplicationId, int destinationApplicationId, string operationKey);
    }
}
