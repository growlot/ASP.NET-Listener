// <copyright file="ITransactionRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        Task<IMemento> GetExecutionContextAsync(Guid recordKey);

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
        Task<IMemento> GetRegistryEntry(Guid recordKey);

        /// <summary>
        /// Updates the transaction registry.
        /// </summary>
        /// <param name="transactionRegistry">The transaction registry.</param>
        /// <returns>The empty task</returns>
        Task UpdateTransactionRegistryAsync(TransactionRegistry transactionRegistry);

        /*
        /// <summary>
        /// Gets the transaction data.
        /// </summary>
        /// <param name="recordKey">The transaction record key.</param>
        /// <returns>The transaction data.</returns>
        Task<string> GetTransactionDataAsync(Guid recordKey);
        */

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
        /// <param name="hashCodes">The hash codes.</param>
        /// <returns>The empty task</returns>
        Task UpdateHashAsync(Dictionary<Guid, string> hashCodes);

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="sourceApplicationKey">The source application key.</param>
        /// <returns>The field configuration mementos.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Specifics of async programming.")]
        Task<Dictionary<string, IEnumerable<IMemento>>> GetFieldConfigurationsAsync(string companyCode, string sourceApplicationKey);

        /// <summary>
        /// Gets the enabled entity operations.
        /// </summary>
        /// <returns>Task&lt;List&lt;EnabledOperationLookup&gt;&gt;.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is expected to be too complex for property")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Specifics of async programming.")]
        Task<List<EntityOperationLookup>> GetEnabledEntityOperations();

        /// <summary>
        /// Updates the transaction registry bulk asynchronously.
        /// </summary>
        /// <param name="modifiedRegistries">The modified registries.</param>
        /// <returns>Task.</returns>
        Task UpdateTransactionRegistryBulkAsync(Collection<TransactionRegistry> modifiedRegistries);
    }
}
