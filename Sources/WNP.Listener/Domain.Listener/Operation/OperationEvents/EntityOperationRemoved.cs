// <copyright file="EntityOperationRemoved.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation.OperationEvents
{
    using System;

    /// <summary>
    /// Entity operation removed
    /// </summary>
    public class EntityOperationRemoved : IDomainEvent
    {
        /// <summary>
        /// Gets or sets the entity category identifier.
        /// </summary>
        /// <value>The entity category identifier.</value>
        public Guid EntityCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the entity operation identifier.
        /// </summary>
        /// <value>The entity operation identifier.</value>
        public Guid EntityOperationId { get; set; }
    }
}