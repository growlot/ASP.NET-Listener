// <copyright file="EndpointMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;

    /// <summary>
    /// Endpoint Memento
    /// </summary>
    public class EndpointMemento : IMemento
    {
        /// <summary>
        /// Gets or sets the endpoint identifier.
        /// </summary>
        /// <value>The endpoint identifier.</value>
        public Guid EndpointId { get; set; }

        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>The protocol identifier.</value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the trigger identifier.
        /// </summary>
        /// <value>The trigger identifier.</value>
        public Guid TriggerId { get; set; }
    }
}