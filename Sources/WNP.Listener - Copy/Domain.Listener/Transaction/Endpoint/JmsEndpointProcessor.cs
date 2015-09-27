// //-----------------------------------------------------------------------
// // <copyright file="JmsEndpointProcessor.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction.Endpoint
{
    using System;
    using Core;
    using DomainEvent;

    /// <summary>
    /// Jms endpoint processor implementation
    /// </summary>
    public class JmsEndpointProcessor : IEndpointProcessor
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
        /// <exception cref="System.ArgumentException">Endpoint configuration is not provided or it is not JMS endpoint configuration</exception>
        public virtual IEvent Process(string sourceApplicationId, string destinationApplicationId, string operationKey,
            string transactionId,
            IIntegrationEndpointConfiguration configuration)
        {
            var data = new JmsDataReady
            {
                Message = "Transaction:{0}".FormatWith(transactionId),
                OperationKey = operationKey,
                TransactionId = transactionId
            };
            var endpointCfg = configuration as JmsEndpointConfiguration;
            if (endpointCfg == null)
            {
                throw new ArgumentException(
                    "Endpoint configuration is not provided or it is not JMS endpoint configuration");
            }

            data.Endpoint.Host = endpointCfg.Host;
            data.Endpoint.Port = endpointCfg.Port;
            data.Endpoint.QueueName = endpointCfg.QueueName;
            data.Endpoint.UserName = endpointCfg.UserName;
            data.Endpoint.Password = endpointCfg.Password;

            return data;
        }
    }
}