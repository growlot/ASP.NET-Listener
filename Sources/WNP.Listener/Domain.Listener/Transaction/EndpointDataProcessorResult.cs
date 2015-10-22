// //-----------------------------------------------------------------------
// <copyright file="EndpointDataProcessorResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Endpoint data processor result
    /// </summary>
    public class EndpointDataProcessorResult : IEndpointDataProcessorResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the data hash.
        /// </summary>
        /// <value>The hash.</value>
        public string Hash { get; set; }
    }
}