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
        /// <param name="targetTableName">Table name to create relation with</param>
        /// <param name="matchOn">Collection of fields to match</param>
        public RelationInformation(string targetTableName, Collection<ColumnMatch> matchOn)
        {
            this.TargetTableName = targetTableName;
            this.MatchOn = matchOn;
        }

        /// <summary>
        /// Entity table name that is linked
        /// </summary>
        public string TargetTableName { get; }

        /// <summary>
        /// Column matches for SQL join
        /// </summary>
        public Collection<ColumnMatch> MatchOn { get; }
    }
}