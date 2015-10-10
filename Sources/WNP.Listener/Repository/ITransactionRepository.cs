using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Repository
{
    using System.Threading.Tasks;
    using Domain;

    public interface ITransactionRepository : IRepository
    {
        Task Create(TransactionExecution transaction);
        Task Update(TransactionExecution transaction);
        Task<IMemento> GetExecutionContext(string transactionKey);
        Task Create(TransactionRegistry transactionRegistry);
        Task<IMemento> GetRegistryEntry(string transactionKey);
        Task Update(TransactionRegistry transactionRegistry);
        Task<string> GetTransactionData(string transactionKey);
        Task<int> GetHashCount(int enabledOperationId, string hash);
        Task UpdateHash(int transactionId, string hash);
        Task<string> GetTransactionHeader(string transactionKey);
    }
}
