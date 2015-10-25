// <copyright file="IntegrationEndpointConfiguration.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using Communication;

    /// <summary>
    /// Integration endpoint configuration
    /// </summary>
    public class IntegrationEndpointConfiguration : IOriginator
    {
        /// <summary>
        /// Gets the connection configuration.
        /// </summary>
        /// <value>The connection configuration.</value>
        public IConnectionConfiguration ConnectionConfiguration { get; private set; }

        /// <summary>
        /// Gets the protocol configuration.
        /// </summary>
        /// <value>The protocol configuration.</value>
        public IProtocolConfiguration ProtocolConfiguration { get; private set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>The protocol.</value>
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the endpoint trigger.
        /// </summary>
        /// <value>The trigger.</value>
        public EndpointTriggerType Trigger { get; set; }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected void SetMemento(IMemento memento)
        {
            var myMemento = (IntegrationEndpointConfigurationMemento)memento;
            this.Protocol = myMemento.Protocol;
            this.Trigger = myMemento.Trigger;
            this.ConnectionConfiguration = ConnectionConfigurationFactory.Create(myMemento.Protocol, myMemento);
            this.ProtocolConfiguration = ProtocolConfigurationFactory.Create(myMemento.Protocol, myMemento);
        }
    }
}