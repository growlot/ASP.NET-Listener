// <copyright file="IWnpBatchRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// WNP batch repository
    /// </summary>
    public interface IWnpBatchRepository
    {
        /// <summary>
        /// Gets the meter test batch asynchronous.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>Task&lt;Collection&lt;System.Object&gt;&gt;.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "As designed")]
        Task<ICollection<Meter>> GetMeterTestBatchAsync(
            string batchNumber);
    }
}