// <copyright file="ITrackModification.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;

    /// <summary>
    /// Interface implemented by POCO's that have 'mod_by' and 'mod_date' columns
    /// </summary>
    public interface ITrackModification
    {
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        string ModBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        DateTime? ModDate { get; set; }
    }
}
