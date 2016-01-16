// <copyright file="EnabledOperationConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using OperationEvents;

    /// <summary>
    /// Enabled operation configuration
    /// </summary>
    public class EnabledOperationConfiguration : AggregateRoot<Guid>
    {
        private ICollection<Endpoint> Endpoints { get; } = new Collection<Endpoint>();

        /// <summary>
        /// Gets or sets the field configuration identifier.
        /// </summary>
        /// <value>The field configuration identifier.</value>
        private FieldConfiguration FieldConfiguration { get; set; }

        /// <summary>
        /// Update the current entity operation configuration
        /// </summary>
        /// <param name="fieldConfiguration">The field configuration.</param>
        /// <param name="endpoints">The endpoints.</param>
        public void Update(
            IMemento fieldConfiguration,
            ICollection<IMemento> endpoints)
        {
            if (endpoints.Any(e => e == null))
            {
                throw new InvalidOperationException("One or more endpoints are not available");
            }

            if (fieldConfiguration != null)
            {
                var fc = new FieldConfiguration();
                ((IOriginator)fc).SetMemento(fieldConfiguration);

                this.FieldConfiguration = fc;
            }
            else
            {
                this.FieldConfiguration = null;
            }

            this.SetEndpointMemento(endpoints);

            var ev = new EntityOperationUpdated
            {
                Id = this.Id,
                FieldConfigurationId = this.FieldConfiguration?.Id
            };

            ev.Endpoints.AddRange(this.Endpoints);

            this.Events.Add(ev);
        }

        /// <inheritdoc/>
        protected override void SetMemento(
            IMemento memento)
        {
            var myMemento = (EnabledOperationConfigurationMemento)memento;

            this.Id = myMemento.Id;
            if (myMemento.FieldConfiguration != null)
            {
                var fcfg = new FieldConfiguration();
                ((IOriginator)fcfg).SetMemento(myMemento.FieldConfiguration);

                this.FieldConfiguration = fcfg;
            }

            this.SetEndpointMemento(myMemento.Endpoints);
        }

        private void SetEndpointMemento(
            IEnumerable<IMemento> mems)
        {
            foreach (var endpoint in mems)
            {
                var ep = new Endpoint();
                ((IOriginator)ep).SetMemento(endpoint);
                this.Endpoints.Add(ep);
            }
        }
    }
}