// <copyright file="EntityCategoryOperation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Entity Category Operation Configuration Model.
    /// </summary>
    public class EntityCategoryOperation : Entity<Guid>
    {
        /// <summary>
        /// Gets the endpoint collection.
        /// </summary>
        /// <value>The endpoint collection.</value>
        public ReadOnlyCollection<Endpoint> EndpointCollection => new ReadOnlyCollection<Endpoint>(this.Endpoints);

        /// <summary>
        /// Gets the field configuration.
        /// </summary>
        /// <value>The field configuration identifier.</value>
        public FieldConfiguration FieldConfiguration { get; private set; }

        /// <summary>
        /// Gets the operation identifier.
        /// </summary>
        /// <value>The operation identifier.</value>
        public string Operation { get; private set; }

        /// <summary>
        /// Gets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        internal IList<Endpoint> Endpoints { get; } = new List<Endpoint>();

        /// <inheritdoc/>
        protected override void SetMemento(
            IMemento memento)
        {
            var myMemento = (EntityCategoryOperationMemento)memento;
            this.Id = myMemento.EntityCategoryOperationId;
            this.Operation = myMemento.OperationName;

            if (myMemento.FieldConfiguration != null)
            {
                var fcfg = new FieldConfiguration();
                ((IOriginator)fcfg).SetMemento(myMemento.FieldConfiguration);

                this.FieldConfiguration = fcfg;
            }

            foreach (EndpointMemento endpointMemento in myMemento.Endpoints)
            {
                var e = new Endpoint();
                ((IOriginator)e).SetMemento(endpointMemento);
                this.Endpoints.Add(e);
            }
        }
    }
}