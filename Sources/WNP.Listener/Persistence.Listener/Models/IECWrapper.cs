// <copyright file="IECWrapper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using AMSLLC.Listener.Domain.Listener.Transaction;

namespace AMSLLC.Listener.Persistence.Listener.Models
{
    /// <summary>
    /// Integration Endpoint configuration wrapper
    /// </summary>
    public class IECWrapper
    {
        public int EnabledOperationId { get; private set; }
        public IntegrationEndpointConfigurationMemento IntegrationEndpointConfigurationMemento { get; private set; }

        public IECWrapper(IntegrationEndpointConfigurationMemento integrationEndpointConfigurationMemento, int enabledOperationId)
        {
            this.IntegrationEndpointConfigurationMemento = integrationEndpointConfigurationMemento;
            this.EnabledOperationId = enabledOperationId;
        }
    }
}