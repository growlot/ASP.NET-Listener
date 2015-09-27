// //-----------------------------------------------------------------------
// // <copyright file="IEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Endpoint data processor interface
    /// </summary>
    public interface IEndpointDataProcessor
    {
        /// <summary>
        /// Prepare data for the endpoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="configuration">The endpoint configuration.</param>
        /// <returns>Task.</returns>
        object Process(object data, IntegrationEndpointConfiguration configuration);
    }
}