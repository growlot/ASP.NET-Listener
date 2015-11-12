// <copyright file="ITrackCreation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;

    /// <summary>
    /// Interface implemented by POCO's that have 'create_by' and 'create_date' columns
    /// </summary>
    public interface ITrackCreation
    {
        /// <summary>
        /// Gets or sets the create by.
        /// </summary>
        /// <value>
        /// The create by.
        /// </value>
        string CreateBy { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        DateTime? CreateDate { get; set; }
    }
}
