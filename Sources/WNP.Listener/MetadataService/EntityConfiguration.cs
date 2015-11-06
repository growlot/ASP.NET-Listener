// <copyright file="EntityConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Entity configurations service that defines entity relations.
    /// </summary>
    public class EntityConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration"/> class.
        /// </summary>
        /// <param name="tableName">Entity table name</param>
        public EntityConfiguration(string tableName)
        {
            this.TableName = tableName;
        }

        /// <summary />
        public string TableName { get; }

        /// <summary />
        public IEnumerable<string> Key { get; private set; } = null;

        /// <summary />
        public bool IsOwnerSpecific { get; private set; } = false;

        /// <summary />
        public Collection<RelationInformation> RequiredRelations { get; } = new Collection<RelationInformation>();

        /// <summary />
        public Collection<RelationInformation> OptionalRelations { get; } = new Collection<RelationInformation>();

        /// <summary />
        public Collection<RelationInformation> ManyRelations { get; } = new Collection<RelationInformation>();

        /// <summary>
        /// Creates default entity configuration.
        /// </summary>
        /// <param name="tableName">Entity table name.</param>
        /// <param name="key">Entity key</param>
        /// <returns>Default EntityConfiguration.</returns>
        public static EntityConfiguration CreateDefault(string tableName, string[] key)
        {
            return new EntityConfiguration(tableName).HasKey(key);
        }

        /// <summary>
        /// Marks this entity as per-owner.
        /// This is needed to auto-bind Owner column from login information.
        /// </summary>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration OwnerSpecific()
        {
            this.IsOwnerSpecific = true;

            return this;
        }

        /// <summary>
        /// Defines composite key for this entity.
        /// This composite key will be used when constructing OData entity.
        /// </summary>
        /// <param name="columnNames">Columns to form composite key</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasKey(params string[] columnNames)
        {
            this.Key = columnNames;

            return this;
        }

        /// <summary>
        /// Defines required "virtual" one-to-one relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasRequired(string tableName, params ColumnMatch[] matchOn)
        {
            this.RequiredRelations.Add(new RelationInformation(tableName, new Collection<ColumnMatch>(matchOn.ToList())));

            return this;
        }

        /// <summary>
        /// Defines an optional "virtual" zero-to-one relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasOptional(string tableName, params ColumnMatch[] matchOn)
        {
            this.OptionalRelations.Add(new RelationInformation(tableName, new Collection<ColumnMatch>(matchOn.ToList())));

            return this;
        }

        /// <summary>
        /// Defines "virtual" one-to-many relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasMany(string tableName, params ColumnMatch[] matchOn)
        {
            this.ManyRelations.Add(new RelationInformation(tableName, new Collection<ColumnMatch>(matchOn.ToList())));

            return this;
        }
    }
}