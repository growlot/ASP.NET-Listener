using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Repository
{
    using System.Threading.Tasks;
    using Domain;

    public interface ITransactionRepository : IRepository
    {
        Task<IMemento> Get(int transactionId);
        Task<IMemento> Get(string sourceApplicationId, string destinationApplicationId, string operationKey);
    }
}
