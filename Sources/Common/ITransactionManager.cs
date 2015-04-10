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
        /// <param name="transactionLog">The transaction log.</param>
        /// <returns>
        /// The transaction identifier for this new transaction.
        /// </returns>
        int NewTransaction(TransactionLog transactionLog);

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
        /// Updates the transaction status, based on operation return code.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="returnCode">The return code.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        void UpdateTransactionStatus(int transactionId, int returnCode, string message, string debugInfo);

        /// <summary>
        /// Updates the transaction data hash.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="dataHash">The data hash.</param>
        void UpdateTransactionDataHash(int transactionId, string dataHash);

        /// <summary>
        /// Gets all the transactions for specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="includeSkipped">If set to <c>true</c> includes skipped transactions in the search results.</param>
        /// <returns>
        /// List of transactions for specified search criteria
        /// </returns>
        IList<TransactionLog> GetTransactions(TransactionLog searchCriteria, bool includeSkipped);

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// The transaction information.
        /// </returns>
        TransactionLog GetTransaction(int transactionId);

        /// <summary>
        /// Gets the transaction type list.
        /// </summary>
        /// <param name="transactionData">The transaction data.</param>
        /// <param name="transactionDirection">The transaction direction.</param>
        /// <param name="transactionSource">The transaction source.</param>
        /// <returns>The list of transactions that need to be run.</returns>
        IList<TransactionType> GetTransactionTypes(TransactionDataLookup transactionData, TransactionDirectionLookup transactionDirection, TransactionSourceLookup transactionSource);
        
        /// <summary>
        /// Gets the transaction type list.
        /// </summary>
        /// <param name="transactionData">The transaction data.</param>
        /// <param name="transactionDirection">The transaction direction.</param>
        /// <param name="transactionSource">The transaction source.</param>
        /// <param name="externalSystemName">Name of the external system.</param>
        /// <returns>The list of transactions that need to be run.</returns>
        IList<TransactionType> GetTransactionTypes(TransactionDataLookup transactionData, TransactionDirectionLookup transactionDirection, TransactionSourceLookup transactionSource, string externalSystemName);

        /// <summary>
        /// Gets the latest successful transaction hash for specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        string GetLastSuccessfulDeviceTransactionDataHash(Device device, TransactionType transactionType);

        /// <summary>
        /// Gets the latest successful transaction hash for specified device test.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        string GetLastSuccessfulDeviceTestTransactionDataHash(DeviceTest deviceTest, TransactionType transactionType);
    }
}
