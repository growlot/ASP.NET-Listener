// <copyright file="IWnpRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    /// <summary>
    /// WNP repository
    /// </summary>
    public interface IWnpRepository
    {
        /// <summary>
        /// Gets the meter test batch asynchronous.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>Task&lt;Collection&lt;System.Object&gt;&gt;.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "As designed")]
        Task<Collection<object>> GetMeterTestBatchAsync(string batchNumber);
    }
}