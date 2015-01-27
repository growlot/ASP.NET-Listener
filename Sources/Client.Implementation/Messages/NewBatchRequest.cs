//-----------------------------------------------------------------------
// <copyright file="NewBatchRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;
    using AMSLLC.Listener.Common.Lookup;

    /// <summary>
    /// Request message type for new batch 
    /// </summary>
    public class NewBatchRequest : IListenerFields
    {
        /// <summary>
        /// Gets or sets the new batch number.
        /// </summary>
        /// <value>
        /// The new batch number.
        /// </value>
        public string NewBatchNumber { get; set; }

        /// <summary>
        /// Gets or sets the listener URL.
        /// </summary>
        /// <value>
        /// The listener URL.
        /// </value>
        public Uri ListenerUrl { get; set; }
    }
}
