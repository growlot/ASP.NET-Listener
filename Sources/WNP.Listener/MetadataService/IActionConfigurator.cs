// <copyright file="IActionConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System;

    /// <summary>
    /// Provides access to dynamically defined OData Actions
    /// </summary>
    public interface IActionConfigurator
    {
        /// <summary>
        /// Determines whether there is any actions defined for specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>True if there are actions defined for the table, false otherwise.</returns>
        bool IsEntityActionsContainerAvailable(string tableName);

        /// <summary>
        /// Gets the action container bound to specific entity or collection.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>The CLR type that contains implementation for these Actions.</returns>
        Type GetEntityActionContainer(string tableName);

        /// <summary>
        /// Gets the unbound action container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>The CLR type that contains implementation for these Actions.</returns>
        Type GetUnboundActionContainer(string containerName);
    }
}