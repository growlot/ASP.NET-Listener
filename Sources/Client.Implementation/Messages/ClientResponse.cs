//-----------------------------------------------------------------------
// <copyright file="ClientResponse.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;

    /// <summary>
    /// Response message type for device retrieval
    /// </summary>
    public class ClientResponse
    {
        /// <summary>
        /// Gets or sets the return code.
        /// </summary>
        /// <value>
        /// The return code.
        /// </value>
        public int ReturnCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the debug information.
        /// </summary>
        /// <value>
        /// The debug information.
        /// </value>
        public string DebugInfo { get; set; }
    }
}
