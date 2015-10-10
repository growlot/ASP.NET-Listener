// //-----------------------------------------------------------------------
// // <copyright file="TransactionExecutionMemento.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

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
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        public TransactionExecutionMemento(string transactionKey, int enabledOperationId,
            IEnumerable<IntegrationEndpointConfigurationMemento> endpointConfiguration)
        {
            this.EndpointConfigurations =
                new ReadOnlyCollection<IntegrationEndpointConfigurationMemento>(endpointConfiguration.ToList());
            this.TransactionKey = transactionKey;
            this.EnabledOperationId = enabledOperationId;
        }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionKey { get; set; }

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
    }
}