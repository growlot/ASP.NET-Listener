// <copyright file="ErrorMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    /// <summary>
    /// Defines error message structure
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the property associated with the message.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public string Property { get; set; }
    }
}
