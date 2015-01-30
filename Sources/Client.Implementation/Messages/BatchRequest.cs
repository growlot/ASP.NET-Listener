//-----------------------------------------------------------------------
// <copyright file="BatchRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;
    using AMSLLC.Listener.Common.Lookup;

    /// <summary>
    /// Request message type for batch
    /// </summary>
    public class BatchRequest : IListenerFields
    {
        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        public string BatchNumber { get; set; }

        /// <summary>
        /// Gets or sets the listener URL.
        /// </summary>
        /// <value>
        /// The listener URL.
        /// </value>
        public Uri ListenerUrl { get; set; }
    }
}
