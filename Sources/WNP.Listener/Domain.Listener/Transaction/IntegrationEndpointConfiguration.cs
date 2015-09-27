// //-----------------------------------------------------------------------
// // <copyright file="IntegrationEndpointConfiguration.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        /// Gets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        public ReadOnlyCollection<FieldConfiguration> FieldConfigurations { get; private set; } =
            new ReadOnlyCollection<FieldConfiguration>(new List<FieldConfiguration>());

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

            List<FieldConfiguration> configurations = new List<FieldConfiguration>();
            foreach (var fieldConfiguration in myMemento.FieldConfigurations)
            {
                var cfg = new FieldConfiguration();
                ((IOriginator)cfg).SetMemento(fieldConfiguration);
                configurations.Add(cfg);
            }

            this.FieldConfigurations = new ReadOnlyCollection<FieldConfiguration>(configurations);
        }
    }
}