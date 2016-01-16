// <copyright file="Endpoint.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;

    /// <summary>
    /// Endpoint entity
    /// </summary>
    public class Endpoint : Entity<Guid>
    {
        /// <summary>
        /// Gets the protocol identifier.
        /// </summary>
        /// <value>The protocol identifier.</value>
        public Guid ProtocolId { get; private set; }

        /// <summary>
        /// Gets the trigger identifier.
        /// </summary>
        /// <value>The trigger identifier.</value>
        public Guid TriggerId { get; private set; }

        /// <inheritdoc/>
        protected override void SetMemento(
            IMemento memento)
        {
            var myMemento = (EndpointMemento)memento;
            this.Id = myMemento.EndpointId;
            this.ProtocolId = myMemento.ProtocolId;
            this.TriggerId = myMemento.TriggerId;
        }
    }
}