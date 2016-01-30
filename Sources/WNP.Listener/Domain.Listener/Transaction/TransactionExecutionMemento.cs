// <copyright file="TransactionExecutionMemento.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Shared;

    /// <summary>
    /// Transaction execution memento
    /// </summary>
    public class TransactionExecutionMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionExecutionMemento" /> class.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="recordKey">The transaction key.</param>
        /// <param name="entityCategoryOperationId">The entity category operation identifier.</param>
        /// <param name="autoSucceed">The automatic succeed.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <param name="childTransactions">The child transactions.</param>
        /// <param name="data">The data.</param>
        /// <param name="duplicates">The duplicates.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
        public TransactionExecutionMemento(
            int transactionId,
            Guid recordKey,
            int entityCategoryOperationId,
            bool autoSucceed,
            IEnumerable<IntegrationEndpointConfigurationMemento> endpointConfiguration,
            IEnumerable<FieldConfigurationMemento> fieldConfigurations,
            IEnumerable<TransactionExecutionMemento> childTransactions,
            object data,
            IEnumerable<Guid> duplicates,
            TransactionStatusType status,
            int? priority)
        {
            this.EndpointConfigurations = new ReadOnlyCollection<IntegrationEndpointConfigurationMemento>(endpointConfiguration.ToList());
            this.TransactionId = transactionId;
            this.RecordKey = recordKey;
            this.EntityCategoryOperationId = entityCategoryOperationId;
            this.FieldConfigurations = new ReadOnlyCollection<FieldConfigurationMemento>(new List<FieldConfigurationMemento>(fieldConfigurations));
            this.ChildTransactions = new ReadOnlyCollection<TransactionExecutionMemento>(new List<TransactionExecutionMemento>(childTransactions ?? new TransactionExecutionMemento[0]));
            this.Data = data;
            this.DuplicateRecords = new ReadOnlyCollection<Guid>(new List<Guid>(duplicates));
            this.Status = status;
            this.Priority = priority;
            this.AutoSucceed = autoSucceed;
        }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public Guid RecordKey { get; set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public ReadOnlyCollection<IntegrationEndpointConfigurationMemento> EndpointConfigurations { get; private set; }

        /// <summary>
        /// Gets the enabled entity category operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; private set; }

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        public ReadOnlyCollection<FieldConfigurationMemento> FieldConfigurations { get; private set; }

        /// <summary>
        /// Gets the child transactions.
        /// </summary>
        /// <value>The child transactions.</value>
        public ReadOnlyCollection<TransactionExecutionMemento> ChildTransactions { get; private set; }

        /// <summary>
        /// Gets the duplicate records.
        /// </summary>
        /// <value>The duplicate records.</value>
        public ReadOnlyCollection<Guid> DuplicateRecords { get; private set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public TransactionStatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int? Priority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic succeed the transaction
        /// </summary>
        /// <value><c>true</c> if [automatic succeed]; otherwise, <c>false</c>.</value>
        public bool AutoSucceed { get; set; }
    }
}