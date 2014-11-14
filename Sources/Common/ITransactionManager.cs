//-----------------------------------------------------------------------
// <copyright file="ITransactionManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Interface for reading and writing Listener transaction log.
    /// </summary>
    public interface ITransactionManager
    {
        /// <summary>
        /// Starts new transaction.
        /// </summary>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="transactionSource">The transaction source.</param>
        /// <returns>
        /// The transaction identifier for this new transaction.
        /// </returns>
        int NewTransaction(TransactionTypeLookup transactionType, int? deviceId, int? deviceTestId, int? batchId, TransactionSourceLookup transactionSource);

        /// <summary>
        /// Updates the state of the transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionState">New state of the transaction.</param>
        void UpdateTransactionState(int transactionId, TransactionStateLookup transactionState);

        /// <summary>
        /// Updates the transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStatus">New transaction status.</param>
        void UpdateTransactionStatus(int transactionId, TransactionStatusLookup transactionStatus);

        /// <summary>
        /// Updates the transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStatus">New transaction status.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        void UpdateTransactionStatus(int transactionId, TransactionStatusLookup transactionStatus, string message, string debugInfo);

        /// <summary>
        /// Gets all the transactions for specified device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// List of transactions for specified device
        /// </returns>
        IList<TransactionLog> GetDeviceTransactions(int deviceId);
    }
}
