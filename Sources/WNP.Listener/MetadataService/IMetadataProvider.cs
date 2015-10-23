// <copyright file="IMetadataProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides access to metadata information
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Gets the namespace for generated OData model.
        /// </summary>
        /// <value>
        /// The OData model namespace.
        /// </value>
        string ODataModelNamespace { get; }

        /// <summary>
        /// Gets dynamically generated assembly that contains all generated OData entities.
        /// </summary>
        /// <value>
        /// The OData model assembly.
        /// </value>
        Assembly ODataModelAssembly { get; }

        /// <summary>
        /// Gets the CLR type of the entity from EDM entity type.
        /// </summary>
        /// <param name="edmEntityType">The EDM entity type as string.</param>
        /// <returns>The CLR type of the entity</returns>
        Type GetEntityType(string edmEntityType);

        /// <summary>
        /// Gets the CLR type of the entity from EDM entity collection type.
        /// </summary>
        /// <param name="edmEntityCollectionType">The EDM entity collection type as string.</param>
        /// <returns>The CLR type of the entity</returns>
        Type GetEntityTypeBySetName(string edmEntityCollectionType);

        /// <summary>
        /// Gets the metadata model for specified CLR entity.
        /// </summary>
        /// <param name="clrEntityType">The CLR entity type as string.</param>
        /// <returns>The metadata entity model.</returns>
        MetadataEntityModel GetModelMapping(string clrEntityType);

        /// <summary>
        /// Gets the metadata model for specified CLR entity.
        /// </summary>
        /// <param name="clrEntityType">The CLR entity type.</param>
        /// <returns>The metadata entity model.</returns>
        MetadataEntityModel GetModelMapping(Type clrEntityType);

        /// <summary>
        /// Gets the metadata model for specified database table.
        /// </summary>
        /// <param name="tableName">The database table name.</param>
        /// <returns>The metadata entity model.</returns>
        MetadataEntityModel GetModelMappingByTableName(string tableName);
    }
}