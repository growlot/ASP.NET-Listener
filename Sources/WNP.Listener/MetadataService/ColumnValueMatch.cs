// <copyright file="ColumnValueMatch.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Column value match for joining parent and child entities.
    /// </summary>
    public class ColumnValueMatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnValueMatch"/> class.
        /// </summary>
        /// <param name="targetColumn">Column of a target entity</param>
        /// <param name="targetColumnValue">The value that target column should have.</param>
        public ColumnValueMatch(string targetColumn, string targetColumnValue)
        {
            this.TargetColumn = targetColumn;
            this.TargetColumnValue = targetColumnValue;
        }

        /// <summary>
        /// Gets the target column.
        /// </summary>
        /// <value>
        /// The target column.
        /// </value>
        public string TargetColumn { get; }

        /// <summary>
        /// Gets the target column value.
        /// </summary>
        /// <value>
        /// The target column value.
        /// </value>
        public string TargetColumnValue { get; }
    }
}