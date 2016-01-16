// <copyright file="EntityOperationUpdated.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation.OperationEvents
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// [Entity Operation Updated] event
    /// </summary>
    public class EntityOperationUpdated : IDomainEvent
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the field configuration identifier.
        /// </summary>
        /// <value>The field configuration identifier.</value>
        public Guid? FieldConfigurationId { get; set; }

        /// <summary>
        /// Gets the endpoint collection.
        /// </summary>
        /// <value>The endpoint collection.</value>
        public ReadOnlyCollection<Endpoint> EndpointCollection => new ReadOnlyCollection<Endpoint>(this.Endpoints);

        internal IList<Endpoint> Endpoints { get; } = new List<Endpoint>();
    }
}