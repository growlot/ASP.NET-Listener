// <copyright file="ProcessingFailedRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    /// <summary>
    /// Class ProcessingFailedRequestMessage.
    /// </summary>
    public class ProcessingFailedRequestMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; set; }
    }
}
