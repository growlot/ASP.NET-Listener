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
        private IEnumerable<string> parentKey = null;

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
        public Collection<RelationInformation> Relations { get; } = new Collection<RelationInformation>();

        /// <summary>
        /// Defines if this entity is contained in parent
        /// </summary>
        public bool IsContained { get; set; }

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
        /// Defines if this entity is contained in parent
        /// Parent key fields should be excluded from the current entity key.
        /// </summary>
        /// <param name="parentKeyFields">The parent key columns.</param>
        /// <returns>
        /// Current instance of EntityConfiguration
        /// </returns>
        public EntityConfiguration Contained(params string[] parentKeyFields)
        {
            this.IsContained = true;
            this.parentKey = parentKeyFields;
            this.UpdateKeys();

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
            this.UpdateKeys();

            return this;
        }

        /// <summary>
        /// Defines required "virtual" one-to-one relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="isContained">Defines if this entity is contained in parent</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasRequired(string tableName, bool isContained, params ColumnMatch[] matchOn)
        {
            this.Relations.Add(new RelationInformation(RelationType.OneToOneRequired, tableName, new Collection<ColumnMatch>(matchOn.ToList()), isContained));

            return this;
        }

        /// <summary>
        /// Defines an optional "virtual" zero-to-one relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="isContained">Defines if this entity is contained in parent</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasOptional(string tableName, bool isContained, params ColumnMatch[] matchOn)
        {
            this.Relations.Add(new RelationInformation(RelationType.OneToOneOptional, tableName, new Collection<ColumnMatch>(matchOn.ToList()), isContained));

            return this;
        }

        /// <summary>
        /// Defines "virtual" one-to-many relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="isContained">Defines if this entity is contained in parent</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasMany(string tableName, bool isContained, params ColumnMatch[] matchOn)
        {
            this.Relations.Add(new RelationInformation(RelationType.OneToMany, tableName, new Collection<ColumnMatch>(matchOn.ToList()), isContained));

            return this;
        }

        /// <summary>
        /// When composite key is used it usually consists of parent entity key and current entity key parts.
        /// Only current entity key should be used as a key, and parent entity key will be enforced by select joins.
        /// </summary>
        private void UpdateKeys()
        {
            if (this.parentKey != null && this.Key != null)
            {
                var tempList = this.Key.ToList();
                tempList.RemoveAll(item => this.parentKey.Contains(item, StringComparer.OrdinalIgnoreCase));
                this.Key = tempList;
            }
        }
    }
}