// <copyright file="ITransactionExecutionData.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Transaction execution data
    /// </summary>
    public interface ITransactionExecutionData
    {
        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        ReadOnlyCollection<IntegrationEndpointConfiguration> EndpointConfigurations { get; }

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        ReadOnlyCollection<FieldConfiguration> FieldConfigurations { get; }

        /// <summary>
        /// Gets or sets the transaction hash.
        /// </summary>
        /// <value>The transaction hash.</value>
        string OutgoingHash { get; set; }

        /// <summary>
        /// Gets or sets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        int EnabledOperationId { get; }

        /// <summary>
        /// Gets the record key.
        /// </summary>
        /// <value>The record key.</value>
        Guid RecordKey { get; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        object Data { get; }

        /// <summary>
        /// Gets the duplicate transaction.
        /// </summary>
        /// <value>The duplicate transaction.</value>
        Collection<Guid> DuplicateTransactions { get; }
    }
}