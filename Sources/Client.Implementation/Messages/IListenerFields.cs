//-----------------------------------------------------------------------
// <copyright file="IListenerFields.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;

    /// <summary>
    /// Interface for required Listener fields.
    /// </summary>
    public interface IListenerFields
    {
        /// <summary>
        /// Gets or sets the listener URL.
        /// </summary>
        /// <value>
        /// The listener URL.
        /// </value>
        Uri ListenerUrl { get; set; }
    }
}
