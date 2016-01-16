// <copyright file="EntityOperationAdded.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation.OperationEvents
{
    using System;

    /// <summary>
    /// [Entity Operation Added] event
    /// </summary>
    public class EntityOperationAdded : IDomainEvent
    {
        /// <summary>
        /// Gets or sets the entity category operation.
        /// </summary>
        /// <value>The entity category operation.</value>
        public EntityCategoryOperation EntityCategoryOperation { get; set; }

        /// <summary>
        /// Gets or sets the entity category identifier.
        /// </summary>
        /// <value>The entity category identifier.</value>
        public Guid EntityCategoryId { get; set; }
    }
}