// <copyright file="ITransactionDataRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Transaction data repository
    /// </summary>
    public interface ITransactionDataRepository : IRepository
    {
        /// <summary>
        /// Saves the data asynchronous.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>Task.</returns>
        Task SaveDataAsync(Guid recordId, object data);
    }
}