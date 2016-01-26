// <copyright file="UpdateEntityCategoryOperationCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Update Entity Category Operation Command.
    /// </summary>
    public class UpdateEntityCategoryOperationCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the field configuration identifier.
        /// </summary>
        /// <value>The field configuration identifier.</value>
        public Guid? FieldConfigurationId { get; set; }

        /// <summary>
        /// Gets the endpoints.
        /// </summary>
        /// <value>The endpoints.</value>
        public ICollection<Guid> Endpoints { get; } = new Collection<Guid>();
    }
}