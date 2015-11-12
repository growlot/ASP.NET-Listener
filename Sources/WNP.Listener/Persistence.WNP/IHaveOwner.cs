// <copyright file="IHaveOwner.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    /// <summary>
    /// Interface implemented by POCO's that have 'owner' field.
    /// </summary>
    public interface IHaveOwner
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        int? Owner { get; set; }
    }
}
