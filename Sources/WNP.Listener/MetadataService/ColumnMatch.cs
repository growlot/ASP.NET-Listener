// <copyright file="ColumnMatch.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Column match for SQL join
    /// </summary>
    public class ColumnMatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnMatch"/> class.
        /// </summary>
        /// <param name="sourceColumn">Column of entity which is currently being configured.</param>
        /// <param name="targetColumn">Column of a target entity</param>
        public ColumnMatch(string sourceColumn, string targetColumn)
        {
            this.SourceColumn = sourceColumn;
            this.TargetColumn = targetColumn;
        }

        /// <summary>Gets source column</summary>
        public string SourceColumn { get; }

        /// <summary>Gets target column</summary>
        public string TargetColumn { get; }
    }
}