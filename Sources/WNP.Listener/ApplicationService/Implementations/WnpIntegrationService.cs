// <copyright file="WnpIntegrationService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Core;

    /// <summary>
    /// Wnp integration
    /// </summary>
    public class WnpIntegrationService : IWnpIntegrationService
    {
        /// <summary>
        /// Create batch using data from WNP
        /// </summary>
        /// <param name="batchKey">The batch key.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="name">The name.</param>
        /// <returns>Task&lt;IEnumerable&lt;OpenBatchTransactionCommand&gt;&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
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