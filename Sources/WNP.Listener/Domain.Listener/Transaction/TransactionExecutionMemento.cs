// <copyright file="TransactionExecutionMemento.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

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
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <param name="childTransactions">The child transactions.</param>
        public TransactionExecutionMemento(int transactionId, string recordKey, int enabledOperationId, IEnumerable<IntegrationEndpointConfigurationMemento> endpointConfiguration, IEnumerable<FieldConfigurationMemento> fieldConfigurations, IEnumerable<TransactionExecutionMemento> childTransactions)
        {
            this.EndpointConfigurations = new ReadOnlyCollection<IntegrationEndpointConfigurationMemento>(endpointConfiguration.ToList());
            this.TransactionId = transactionId;
            this.RecordKey = recordKey;
            this.EnabledOperationId = enabledOperationId;
            this.FieldConfigurations = new ReadOnlyCollection<FieldConfigurationMemento>(new List<FieldConfigurationMemento>(fieldConfigurations));
            this.ChildTransactions = new ReadOnlyCollection<TransactionExecutionMemento>(new List<TransactionExecutionMemento>(childTransactions ?? new TransactionExecutionMemento[0]));
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
        public string RecordKey { get; set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public ReadOnlyCollection<IntegrationEndpointConfigurationMemento> EndpointConfigurations { get; private set; }

        /// <summary>
        /// Gets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EnabledOperationId { get; private set; }

        /// <summary>
        /// Gets or sets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        public ReadOnlyCollection<FieldConfigurationMemento> FieldConfigurations { get; private set; }

        /// <summary>
        /// Gets the child transactions.
        /// </summary>
        /// <value>The child transactions.</value>
        public ReadOnlyCollection<TransactionExecutionMemento> ChildTransactions { get; private set; }
    }
}