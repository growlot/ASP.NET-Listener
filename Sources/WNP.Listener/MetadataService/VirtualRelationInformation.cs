// <copyright file="VirtualRelationInformation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Relation information holder for "virtual" relations
    /// </summary>
    public class VirtualRelationInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualRelationInformation"/> class.
        /// </summary>
        /// <param name="relationType">Relation type</param>
        /// <param name="virtualEntityName">Virtual entity name</param>
        /// <param name="discriminator">Field list that represents "key" by which we can extract the record(s) related to this entity</param>
        /// <param name="columnList">Entity columns list</param>
        public VirtualRelationInformation(RelationType relationType, string virtualEntityName, Collection<string> discriminator, Collection<string> columnList)
        {
            this.RelationType = relationType;
            this.VirtualEntityName = virtualEntityName;
            this.Discriminator = discriminator;
            this.ColumnList = columnList;
        }

        /// <summary>Gets relation type</summary>
        public RelationType RelationType { get; }

        /// <summary>Gets virtual entity name</summary>
        public string VirtualEntityName { get; }

        /// <summary>Gets the internal discriminator key set</summary>
        public Collection<string> Discriminator { get; }

        /// <summary>Gets column list</summary>
        public Collection<string> ColumnList { get; }
    }
}