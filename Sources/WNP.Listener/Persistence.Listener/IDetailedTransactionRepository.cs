namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Repository.Listener;

    public interface IDetailedTransactionRepository : ITransactionRepository

    {
        /// <summary>
        /// Selects the transactions asynchronously.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        /// <returns>Task&lt;List&lt;TransactionRegistryEntry&gt;&gt;.</returns>
        Task<List<TransactionRegistryEntry>> SelectTransactionsAsync(
            Guid recordKey);

        /// <summary>
        /// Selects the child transactions asynchronously.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        /// <returns>Task&lt;List&lt;TransactionRegistryEntry&gt;&gt;.</returns>
        Task<List<TransactionRegistryEntry>> SelectChildTransactionsAsync(
            Guid recordKey);
    }
}