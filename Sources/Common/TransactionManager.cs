//-----------------------------------------------------------------------
// <copyright file="TransactionManager.cs" company="Advanced Metering Services LLC">
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
    using log4net;

    /// <summary>
    /// Implements transaction mechanism based on the information stored in transaction log.
    /// </summary>
    public class TransactionManager : ITransactionManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The listener system
        /// </summary>
        private ListenerSystem listenerSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionManager"/> class.
        /// </summary>
        /// <param name="persistenceController">The persistence controller.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when persistence controllers is not provided.
        /// </exception>
        public TransactionManager(IPersistenceController persistenceController)
        {
            if (persistenceController == null)
            {
                string exceptionMessage = "TransactionLogManager class can not be created because persistenceController is null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("persistenceController", exceptionMessage);
            }

            this.listenerSystem = persistenceController.ListenerSystem;
        }

        /// <summary>
        /// Starts new transaction.
        /// </summary>
        /// <param name="transactionTypeId">The transaction type identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <returns>
        /// The transaction identifier for this new transaction.
        /// </returns>
        public int NewTransaction(int transactionTypeId, int? deviceId, int? deviceTestId, int? batchId)
        {
            return this.listenerSystem.AddTransactionLog(transactionTypeId, (int)TransactionStatusLookup.InProgress, deviceId, deviceTestId, batchId);
        }

        /// <summary>
        /// Updates the state of the transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionState">New state of the transaction.</param>
        public void UpdateTransactionState(int transactionId, TransactionStateLookup transactionState)
        {
            this.listenerSystem.UpdateTransactionLogState(transactionId, (int)transactionState);
        }

        /// <summary>
        /// Updates the transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStatus">New transaction status.</param>
        public void UpdateTransactionStatus(int transactionId, TransactionStatusLookup transactionStatus)
        {
            this.listenerSystem.UpdateTransactionLogStatus(transactionId, (int)transactionStatus, string.Empty, string.Empty);
        }

        /// <summary>
        /// Updates the transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStatus">New transaction status.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        public void UpdateTransactionStatus(int transactionId, TransactionStatusLookup transactionStatus, string message, string debugInfo)
        {
            // message can not exceed 1000 symbols, because of database field limitation.
            if (message != null && message.Length > 1000)
            {
                message = message.Substring(0, 1000);
            } 
            
            this.listenerSystem.UpdateTransactionLogStatus(transactionId, (int)transactionStatus, message, debugInfo);
        }

        /// <summary>
        /// Updates the transaction status, based on operation return code.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="returnCode">The return code.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        public void UpdateTransactionStatus(int transactionId, int returnCode, string message, string debugInfo)
        {
            if (returnCode != 0)
            {
                this.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, message, debugInfo);
            }
            else
            {
                this.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Succeeded, message, debugInfo);
            }
        }

        /// <summary>
        /// Updates the transaction data hash.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="dataHash">The data hash.</param>
        public void UpdateTransactionDataHash(int transactionId, string dataHash)
        {
            this.listenerSystem.UpdateTransactionDataHash(transactionId, dataHash);
        }

        /// <summary>
        /// Gets all the transactions for specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="includeSkipped">If set to <c>true</c> includes skipped transactions in the search results.</param>
        /// <returns>
        /// List of transactions for specified search criteria
        /// </returns>
        public IList<TransactionLog> GetTransactions(TransactionLog searchCriteria, bool includeSkipped)
        {
            return this.listenerSystem.GetTransactions(searchCriteria, includeSkipped);
        }

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// The transaction information.
        /// </returns>
        public TransactionLog GetTransaction(int transactionId)
        {
            return this.listenerSystem.GetTransaction(transactionId);
        }

        /// <summary>
        /// Gets the transaction type list.
        /// </summary>
        /// <param name="transactionData">The transaction data.</param>
        /// <param name="transactionDirection">The transaction direction.</param>
        /// <param name="transactionSource">The transaction source.</param>
        /// <returns>The list of transactions that need to be run.</returns>
        public IList<TransactionType> GetTransactionTypes(TransactionDataLookup transactionData, TransactionDirectionLookup transactionDirection, TransactionSourceLookup transactionSource)
        {
            return this.listenerSystem.GetTransactionTypes((int)transactionData, (int)transactionDirection, (int)transactionSource);
        }

        /// <summary>
        /// Gets the latest successful transaction hash for specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        public string GetLastSuccessfulDeviceTransactionDataHash(Device device, TransactionType transactionType)
        {
            return this.listenerSystem.GetLastSuccessfulDeviceTransactionDataHash(device, transactionType);
        }

        /// <summary>
        /// Gets the latest successful transaction hash for specified device test.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        public string GetLastSuccessfulDeviceTestTransactionDataHash(DeviceTest deviceTest, TransactionType transactionType)
        {
            return this.listenerSystem.GetLastSuccessfulDeviceTestTransactionDataHash(deviceTest, transactionType);
        }
    }
}
