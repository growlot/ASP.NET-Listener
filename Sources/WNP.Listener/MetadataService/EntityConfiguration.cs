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

        /// <summary>Gets table name</summary>
        public string TableName { get; }

        /// <summary>
        /// Gets or sets the part of the entity key that
        /// should be taken from parent entity if entity is contained
        /// </summary>
        public IEnumerable<string> ParentKey { get; set; } = null;

        /// <summary>Gets key</summary>
        public IEnumerable<string> Key { get; private set; } = null;

        /// <summary>
        /// Gets full (parent + child) key
        /// </summary>
        public IEnumerable<string> FullKey
        {
            get
            {
                var fullKey = this.ParentKey.Union(this.Key);
                if (this.IsOwnerSpecific)
                {
                    fullKey = fullKey.Union(new[] { "OWNER" });
                }

                return fullKey;
            }
        }

        /// <summary>Gets a value indicating whether the entity is owner specific</summary>
        public bool IsOwnerSpecific { get; private set; } = false;

        /// <summary>Gets relations</summary>
        public Collection<RelationInformation> Relations { get; } = new Collection<RelationInformation>();

        /// <summary>Gets virtual relations</summary>
        public Collection<VirtualRelationInformation> VirtualRelations { get; } = new Collection<VirtualRelationInformation>();

        /// <summary>
        /// Gets or sets a value indicating whether this entity is contained in parent
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
            this.ParentKey = parentKeyFields;
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
            this.Relations.Add(
                new RelationInformation(
                    RelationType.OneToOne,
                    tableName,
                    new Collection<ColumnMatch>(matchOn.ToList()),
                    isContained));

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
            this.Relations.Add(
                new RelationInformation(
                    RelationType.ZeroToOne,
                    tableName,
                    new Collection<ColumnMatch>(matchOn.ToList()),
                    isContained));

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
            this.Relations.Add(
                new RelationInformation(
                    RelationType.OneToMany,
                    tableName,
                    new Collection<ColumnMatch>(matchOn.ToList()),
                    isContained));

            return this;
        }

        /// <summary>
        /// Defines "virtual" one-to-many relationship with another table (entity)
        /// </summary>
        /// <param name="tableName">Table to link to</param>
        /// <param name="isContained">Defines if this entity is contained in parent</param>
        /// <param name="matchValue">Force specific field to be of specific value.</param>
        /// <param name="matchOn">On which field to form a match</param>
        /// <returns>
        /// Current instance of EntityConfiguration
        /// </returns>
        public EntityConfiguration HasMany(
            string tableName,
            bool isContained,
            ColumnValueMatch matchValue,
            params ColumnMatch[] matchOn)
        {
            this.Relations.Add(
                new RelationInformation(
                    RelationType.OneToMany,
                    tableName,
                    matchValue,
                    new Collection<ColumnMatch>(matchOn.ToList()),
                    isContained));

            return this;
        }

        /// <summary>
        /// Defines relationship
        /// </summary>
        /// <param name="relationType">Relation type</param>
        /// <param name="virtualEntityName">Virtual entity name</param>
        /// <param name="discriminator">One or more column names by which internal relation is defined</param>
        /// <param name="columnList">Columns that will form an "internal" entity</param>
        /// <returns>Current instance of EntityConfiguration</returns>
        public EntityConfiguration HasVirtualRelation(
            RelationType relationType,
            string virtualEntityName,
            string[] discriminator,
            string[] columnList)
        {
            this.VirtualRelations.Add(
                new VirtualRelationInformation(
                    relationType,
                    virtualEntityName,
                    new Collection<string>(discriminator),
                    new Collection<string>(columnList)));
            this.UpdateKeys();

            return this;
        }

        /// <summary>
        /// Detect, if this database column name is in one of virtual relations of the entity
        /// </summary>
        /// <param name="dbColName">Database column name</param>
        /// <returns>If this database column name is in one of virtual relations</returns>
        public bool IsFieldInVirtualRelation(string dbColName)
        {
            return
                this.VirtualRelations.SelectMany(
                    information => information.ColumnList.Select(s => s.ToUpperInvariant())).Contains(dbColName);
        }

        /// <summary>
        /// When composite key is used it usually consists of parent entity key and current entity key parts.
        /// Only current entity key should be used as a key, and parent entity key will be enforced by select joins.
        /// Same also applies to virtual entity keys. They shouldn't be a part of entity key.
        /// </summary>
        private void UpdateKeys()
        {
            if (this.ParentKey != null && this.Key != null)
            {
                var tempList = this.Key.ToList();
                tempList.RemoveAll(item => this.ParentKey.Contains(item, StringComparer.OrdinalIgnoreCase));
                this.Key = tempList;
            }

            if (this.VirtualRelations != null && this.Key != null)
            {
                var virtualEntitiesKeys = new List<string>();
                foreach (var virtualRelationship in this.VirtualRelations)
                {
                    virtualEntitiesKeys.AddRange(virtualRelationship.Discriminator.ToList());
                }

                this.Key = this.Key.Except(virtualEntitiesKeys);
            }
        }
    }
}