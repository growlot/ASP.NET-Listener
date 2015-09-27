using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Repository
{
    using System.Threading.Tasks;
    using Domain;

    public interface ITransactionRepository : IRepository
    {
        Task<IMemento> Get(string transactionId);
        Task Create(TransactionExecution transaction);
        Task Update(TransactionExecution transaction);
    }
}
