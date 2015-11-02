// <copyright file="IBatchBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Model;

    /// <summary>
    /// Batch builder interface
    /// </summary>
    public interface IBatchBuilder
    {
        /// <summary>
        /// Creates the batch using batch number.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;List&lt;BatchTransactionEntry&gt;&gt;.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Review later")]
        Task<List<OpenBatchTransactionCommand>> Create(string batchNumber, string companyCode, string applicationKey, string userName);
    }
}