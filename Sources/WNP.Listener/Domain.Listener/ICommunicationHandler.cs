// <copyright file="ICommunicationHandler.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for complex domain event handlers
    /// </summary>
    public interface ICommunicationHandler
    {
        /// <summary>
        /// Handles the specified event data.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="connectionConfiguration">The connection configuration.</param>
        /// <param name="protocolConfiguration">The protocol configuration.</param>
        /// <returns>Task.</returns>
        Task Handle(object requestData, IConnectionConfiguration connectionConfiguration, IProtocolConfiguration protocolConfiguration);
    }
}