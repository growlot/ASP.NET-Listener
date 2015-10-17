using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Repository
{
    using System.Threading.Tasks;
    using Domain;

    public interface ITransactionRepository : IRepository
    {
        Task Create(TransactionExecution transaction);

        Task Update(TransactionExecution transaction);

        Task<IMemento> GetExecutionContext(string recordKey);

        Task Create(TransactionRegistry transactionRegistry);

        Task<IMemento> GetRegistryEntry(string recordKey);

        Task Update(TransactionRegistry transactionRegistry);

        Task<string> GetTransactionData(string recordKey);
        Task<int> GetHashCount(int enabledOperationId, string hash);
        Task UpdateHash(int transactionId, string hash);
        Task<string> GetTransactionHeader(string recordKey);
    }
}
