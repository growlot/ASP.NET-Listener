// //-----------------------------------------------------------------------
// // <copyright file="JmsDataReady.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent
{
    using Endpoint;

    /// <summary>
    /// JmsDataReady event data
    /// </summary>
    public class JmsDataReady : IEvent
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the endpoint configuration.
        /// </summary>
        /// <value>The endpoint.</value>
        public JmsEndpointConfiguration Endpoint { get; } = new JmsEndpointConfiguration();
    }
}