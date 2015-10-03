﻿// //-----------------------------------------------------------------------
// // <copyright file="IEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Dynamic;

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
        /// <returns>IEndpointDataProcessorResult.</returns>
        IEndpointDataProcessorResult Process(object data, IntegrationEndpointConfiguration configuration);
    }
}