//-----------------------------------------------------------------------
// <copyright file="IListenerDataProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation
{
    using System.Collections.Generic;
    using AMSLLC.Listener.Client.Implementation.Messages;

    /// <summary>
    /// Interface for accessing Listener data from external systems.
    /// </summary>
    public interface IListenerDataProvider
    {
        /// <summary>
        /// Gets the transaction log.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// List of transaction log entries for specified device
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve transaction log when request is not specified</exception>
        IList<TransactionLogResponse> GetTransactionLog(TransactionLogRequest request);
    }
}
