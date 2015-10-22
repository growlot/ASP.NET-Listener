// //-----------------------------------------------------------------------
// <copyright file="IEndpointDataProcessor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Endpoint data processor interface
    /// </summary>
    public interface IEndpointDataProcessor
    {
        /// <summary>
        /// Prepare data for the endpoint.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <returns>IEndpointDataProcessorResult.</returns>
        IEndpointDataProcessorResult Process(object data, IList<FieldConfiguration> fieldConfigurations);
    }
}