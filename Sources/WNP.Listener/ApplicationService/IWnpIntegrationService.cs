// <copyright file="IWnpIntegrationService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;

    /// <summary>
    /// Wnp integration service interface
    /// </summary>
    public interface IWnpIntegrationService
    {
        /// <summary>
        /// Create batch using data from WNP
        /// </summary>
        /// <param name="batchKey">The batch key.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="name">The name.</param>
        /// <returns>Task&lt;IEnumerable&lt;OpenBatchTransactionCommand&gt;&gt;.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Review later")]
        Task<ICollection<OpenBatchTransactionCommand>> Create(
            string batchKey,
            string companyCode,
            string applicationKey,
            string name);
    }
}