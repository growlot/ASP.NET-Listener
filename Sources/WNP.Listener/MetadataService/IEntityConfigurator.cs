// <copyright file="IEntityConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Contract for the entities configuration service.
    /// </summary>
    public interface IEntityConfigurator
    {
        /// <summary>
        /// Gets entity configuration by full table name.
        /// </summary>
        /// <param name="tableName"> The table name. </param>
        /// <returns> The <see cref="EntityConfiguration"/>. </returns>
        EntityConfiguration GetEntityConfiguration(string tableName);
    }
}