// <copyright file="EntityCategoryMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Entity category memento
    /// </summary>
    public class EntityCategoryMemento : IMemento
    {
        /// <summary>
        /// Gets or sets the enabled entity category name.
        /// </summary>
        /// <value>The enabled entity name.</value>
        public string EntityCategoryName { get; set; }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public ICollection<EntityCategoryOperationMemento> Operations { get; } = new Collection<EntityCategoryOperationMemento>();

        /// <summary>
        /// Gets the enabled operations collection.
        /// </summary>
        /// <value>The enabled operations.</value>
        public ICollection<string> EnabledOperations { get; } = new Collection<string>();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
    }
}