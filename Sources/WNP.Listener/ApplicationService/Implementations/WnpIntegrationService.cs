// <copyright file="WnpIntegrationService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System.Threading.Tasks;
    using Commands;
    using Core;

    /// <summary>
    /// Wnp integration
    /// </summary>
    public class WnpIntegrationService : IWnpIntegrationService
    {
        /// <inheritdoc/>
        public Task<OpenBatchTransactionCommand> Create(
            string batchKey,
            string companyCode,
            string applicationKey,
            string name)
        {
            var builder = ApplicationIntegration.DependencyResolver.ResolveType<IBatchBuilder>();

            return builder.Create(batchKey, companyCode, applicationKey, name);
        }
    }
}