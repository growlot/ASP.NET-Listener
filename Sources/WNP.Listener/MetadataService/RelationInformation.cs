// <copyright file="RelationInformation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Relation information holder
    /// </summary>
    public class RelationInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationInformation"/> class.
        /// </summary>
        /// <param name="relationType">Entity relation type</param>
        /// <param name="targetTableName">Table name to create relation with</param>
        /// <param name="matchOn">Collection of fields to match</param>
        /// <param name="isContained">Defines if this entity is contained in parent</param>
        public RelationInformation(RelationType relationType, string targetTableName, Collection<ColumnMatch> matchOn, bool isContained)
        {
            this.TargetTableName = targetTableName;
            this.MatchOn = matchOn;
            this.IsContained = isContained;
            this.RelationType = relationType;
        }

        /// <summary>
        /// Defines if this entity is contained in parent
        /// </summary>
        public bool IsContained { get; }

        /// <summary>
        /// Entity table name that is linked
        /// </summary>
        public string TargetTableName { get; }

        /// <summary>
        /// Column matches for SQL join
        /// </summary>
        public Collection<ColumnMatch> MatchOn { get; }

        /// <summary>
        /// Entity relation type
        /// </summary>
        public RelationType RelationType { get; }
    }
}