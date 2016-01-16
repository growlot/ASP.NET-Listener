// <copyright file="EnabledOperationConfigurationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;

    /// <summary>
    /// Enabled operation configuration memento
    /// </summary>
    public class EnabledOperationConfigurationMemento : EntityCategoryOperationMemento
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
    }
}