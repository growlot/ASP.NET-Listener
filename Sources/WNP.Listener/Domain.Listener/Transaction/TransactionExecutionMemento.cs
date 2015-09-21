// //-----------------------------------------------------------------------
// // <copyright file="TransactionExecutionMemento.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Transaction execution memento
    /// </summary>
    public class TransactionExecutionMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionExecutionMemento"/> class.
        /// </summary>
        /// <param name="sourceApplicationId">The source application identifier.</param>
        /// <param name="destinationApplicationId">The destination application identifier.</param>
        /// <param name="destinationOperationKey">The destination operation key.</param>
        public TransactionExecutionMemento(string sourceApplicationId, string destinationApplicationId,
            string destinationOperationKey)
        {
            this.SourceApplicationId = sourceApplicationId;
            this.DestinationApplicationId = destinationApplicationId;
            this.DestinationOperationKey = destinationOperationKey;
        }

        /// <summary>
        /// Gets the source application identifier.
        /// </summary>
        /// <value>The source application identifier.</value>
        public string SourceApplicationId { get; private set; }

        /// <summary>
        /// Gets the destination application identifier.
        /// </summary>
        /// <value>The destination application identifier.</value>
        public string DestinationApplicationId { get; private set; }

        /// <summary>
        /// Gets the destination operation key.
        /// </summary>
        /// <value>The destination operation key.</value>
        public string DestinationOperationKey { get; private set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public IList<IIntegrationEndpointConfiguration> EndpointConfigurations { get; } =
            new List<IIntegrationEndpointConfiguration>();
    }
}