// //-----------------------------------------------------------------------
// <copyright file="IntegrationEndpointConfigurationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using Communication;

    /// <summary>
    /// Integration Endpoint Configuration Memento.
    /// </summary>
    public class IntegrationEndpointConfigurationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEndpointConfigurationMemento" /> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="connectionDetails">The connection details.</param>
        /// <param name="triggerType">Type of the trigger.</param>
        public IntegrationEndpointConfigurationMemento(string protocol, string connectionDetails, EndpointTriggerType triggerType)
        {
            this.Protocol = protocol;
            this.ConnectionDetails = connectionDetails;
            this.Trigger = triggerType;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Protocol { get; private set; }

        /// <summary>
        /// Gets or sets the connection details.
        /// </summary>
        /// <value>The connection details.</value>
        public string ConnectionDetails { get; private set; }

        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        /// <value>The trigger.</value>
        public EndpointTriggerType Trigger { get; set; }
    }
}