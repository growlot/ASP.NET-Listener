// //-----------------------------------------------------------------------
// // <copyright file="IEndpointDataProcessorResult.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Endpoint data processor result
    /// </summary>
    public interface IEndpointDataProcessorResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        object Data { get; set; }

        /// <summary>
        /// Gets or sets the data hash.
        /// </summary>
        /// <value>The hash.</value>
        string Hash { get; set; }
    }
}