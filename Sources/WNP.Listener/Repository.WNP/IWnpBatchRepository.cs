// <copyright file="IWnpBatchRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// WNP batch repository
    /// </summary>
    public interface IWnpBatchRepository
    {
        /// <summary>
        /// Gets the meter test batch asynchronously.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>The collection of meters that belong to the batch.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "As designed")]
        Task<ICollection<Meter>> GetMeterTestBatchAsync(string batchNumber);
    }
}