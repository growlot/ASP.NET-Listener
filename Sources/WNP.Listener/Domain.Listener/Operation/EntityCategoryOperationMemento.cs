// <copyright file="EntityCategoryOperationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Entity Category Operation Configuration Memento.
    /// </summary>
    public class EntityCategoryOperationMemento : IMemento
    {
        /// <summary>
        /// Gets or sets the entity category operation identifier.
        /// </summary>
        /// <value>The entity category operation identifier.</value>
        public Guid EntityCategoryOperationId { get; set; }

        /// <summary>
        /// Gets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        public ICollection<EndpointMemento> Endpoints { get; } = new List<EndpointMemento>();

        /// <summary>
        /// Gets or sets the field configuration.
        /// </summary>
        /// <value>The field configuration identifier.</value>
        public FieldConfigurationMemento FieldConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>The name of the operation.</value>
        public string OperationName { get; set; }
    }
}