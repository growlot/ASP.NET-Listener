// <copyright file="ITransactionRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Listener.Transaction;

    /// <summary>
    /// Repository for Listener transactions
    /// </summary>
    public interface ITransactionRepository : IRepository
    {
        /// <summary>
        /// Creates new transaction registry.
        /// </summary>
        /// <param name="transaction">The executing transaction.</param>
        /// <returns>The empty task</returns>
        Task CreateAsync(TransactionExecution transaction);

        /// <summary>
        /// Updates transaction registry.
        /// </summary>
        /// <param name="transaction">The executing transaction.</param>
        /// <returns>The empty task</returns>
        Task UpdateAsync(TransactionExecution transaction);

        /// <summary>
        /// Gets the execution context.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        /// <returns>The memento</returns>
        Task<IMemento> GetExecutionContextAsync(string recordKey);

        /// <summary>
        /// Creates new transaction registry.
        /// </summary>
        /// <param name="transactionRegistry">The transaction registry.</param>
        /// <returns>The empty task</returns>
        Task CreateTransactionRegistryAsync(TransactionRegistry transactionRegistry);

        /// <summary>
        /// Gets the registry entry.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        /// <returns>The memento</returns>
        Task<IMemento> GetRegistryEntry(string recordKey);

        /// <summary>
        /// Updates the transaction registry.
        /// </summary>
        /// <param name="transactionRegistry">The transaction registry.</param>
        /// <returns>The empty task</returns>
        Task UpdateTransactionRegistryAsync(TransactionRegistry transactionRegistry);

        /// <summary>
        /// Gets the transaction data.
        /// </summary>
        /// <param name="recordKey">The transaction record key.</param>
        /// <returns>The transaction data.</returns>
        Task<string> GetTransactionDataAsync(string recordKey);

        /// <summary>
        /// Gets the hash count.
        /// </summary>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>The number of records with same hash</returns>
        Task<int> GetHashCountAsync(int enabledOperationId, string hash);

        /// <summary>
        /// Updates the hash.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>The empty task</returns>
        Task UpdateHashAsync(int transactionId, string hash);

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="sourceApplicationKey">The source application key.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <returns>The field configuration mementos.</returns>
        Task<IEnumerable<IMemento>> GetFieldConfigurationsAsync(string companyCode, string sourceApplicationKey, string operationKey);
    }
}
