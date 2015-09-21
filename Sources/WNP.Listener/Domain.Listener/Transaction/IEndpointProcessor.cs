// //-----------------------------------------------------------------------
// // <copyright file="IEndpointProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Endpoint processor interface
    /// </summary>
    public interface IEndpointProcessor
    {
        /// <summary>
        /// Prepare data for the endpoint.
        /// </summary>
        /// <param name="sourceApplicationId">The source application identifier.</param>
        /// <param name="destinationApplicationId">The destination application identifier.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="configuration">The endpoint configuration.</param>
        /// <returns>Task.</returns>
        IEvent Process(string sourceApplicationId, string destinationApplicationId, string operationKey,
            string transactionId, IIntegrationEndpointConfiguration configuration);
    }
}