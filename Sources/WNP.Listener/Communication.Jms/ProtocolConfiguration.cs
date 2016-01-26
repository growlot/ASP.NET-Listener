// <copyright file="ProtocolConfiguration.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication.Jms
{
    using Domain.Listener;

    /// <summary>
    /// JMS Protocol configuration
    /// </summary>
    public class ProtocolConfiguration : IProtocolConfiguration
    {
        /// <summary>
        /// Gets or sets the message type template.
        /// </summary>
        /// <value>The message type template.</value>
        public string MessageTypeTemplate { get; set; }
    }
}