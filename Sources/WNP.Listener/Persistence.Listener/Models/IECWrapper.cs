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
        /// <summary>
        /// Initializes a new instance of the <see cref="IECWrapper"/> class.
        /// </summary>
        /// <param name="integrationEndpointConfigurationMemento">The integration endpoint configuration memento.</param>
        /// <param name="entityCategoryOperationId">The entity category operation identifier.</param>
        public IECWrapper(IntegrationEndpointConfigurationMemento integrationEndpointConfigurationMemento, int entityCategoryOperationId)
        {
            this.IntegrationEndpointConfigurationMemento = integrationEndpointConfigurationMemento;
            this.EntityCategoryOperationId = entityCategoryOperationId;
        }

        /// <summary>
        /// Gets the entity category operation identifier.
        /// </summary>
        /// <value>
        /// The entity category operation identifier.
        /// </value>
        public int EntityCategoryOperationId { get; private set; }

        /// <summary>
        /// Gets the integration endpoint configuration memento.
        /// </summary>
        /// <value>
        /// The integration endpoint configuration memento.
        /// </value>
        public IntegrationEndpointConfigurationMemento IntegrationEndpointConfigurationMemento { get; private set; }
    }
}