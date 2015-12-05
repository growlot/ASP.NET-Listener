// <copyright file="RelationType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Entity relation type
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// Source entity has 0 or many related entities
        /// </summary>
        OneToMany,

        /// <summary>
        /// Source entity has 0 or one related entities
        /// </summary>
        ZeroToOne,

        /// <summary>
        /// Source entity has exactly one related entity
        /// </summary>
        OneToOne,
    }
}