// <copyright file="MetadataEntityModel.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model defining specific metadata entity.
    /// </summary>
    public class MetadataEntityModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataEntityModel"/> class.
        /// </summary>
        /// <param name="tableName">Name of the database table.</param>
        /// <param name="className">Name of the CLR class.</param>
        /// <param name="modelToColumnMappings">The model to column mappings.</param>
        /// <param name="columnToModelMappings">The column to model mappings.</param>
        /// <param name="fieldInfo">The field information.</param>
        /// <param name="actionsContainer">The actions container.</param>
        /// <param name="entityConfiguration">The entity configuration.</param>
        public MetadataEntityModel(string tableName, string className, Dictionary<string, string> modelToColumnMappings, Dictionary<string, string> columnToModelMappings, Dictionary<string, MetadataFieldInfo> fieldInfo, Type actionsContainer, EntityConfiguration entityConfiguration)
        {
            this.TableName = tableName;
            this.ClassName = className;
            this.ModelToColumnMappings = modelToColumnMappings;
            this.ColumnToModelMappings = columnToModelMappings;
            this.FieldInfo = fieldInfo;
            this.ActionsContainer = actionsContainer;
            this.EntityConfiguration = entityConfiguration;
        }

        /// <summary>
        /// Gets database table that is used by this entity.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; }

        /// <summary>
        /// Gets the CLR class name of this entity.
        /// </summary>
        /// <value>
        /// The CLR class name.
        /// </value>
        public string ClassName { get; }

        /// <summary>
        /// Gets the metadata information about each database field.
        /// </summary>
        /// <value>
        /// The field information.
        /// </value>
        public Dictionary<string, MetadataFieldInfo> FieldInfo { get; }

        /// <summary>
        /// Gets the mappings defining how CLR properties map to database table columns.
        /// </summary>
        /// <value>
        /// The model to column mappings.
        /// </value>
        public Dictionary<string, string> ModelToColumnMappings { get; }

        /// <summary>
        /// Gets the mappings defining how database table columns map to CLR properties.
        /// </summary>
        /// <value>
        /// The column to model mappings.
        /// </value>
        public Dictionary<string, string> ColumnToModelMappings { get; }

        /// <summary>
        /// Gets the container for actions bound to this metadata entity.
        /// </summary>
        /// <value>
        /// The actions container.
        /// </value>
        public Type ActionsContainer { get; }

        /// <summary>
        /// Gets custom entity configuration that defines relationship and other information about the entity.
        /// </summary>
        public EntityConfiguration EntityConfiguration { get; }
    }
}
