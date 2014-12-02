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
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="transactionSource">The transaction source.</param>
        /// <returns>
        /// The transaction identifier for this new transaction.
        /// </returns>
        public int NewTransaction(TransactionTypeLookup transactionType, int? deviceId, int? deviceTestId, int? batchId, TransactionSourceLookup transactionSource)
        {
            return this.listenerSystem.AddTransactionLog((int)transactionType, (int)TransactionStatusLookup.InProgress, deviceId, deviceTestId, batchId, (int)transactionSource);
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
            this.listenerSystem.UpdateTransactionLogStatus(transactionId, (int)transactionStatus, message, debugInfo);
        }

        /// <summary>
        /// Gets all the transactions for specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns>
        /// List of transactions for specified search criteria
        /// </returns>
        public IList<TransactionLog> GetTransactions(TransactionLog searchCriteria)
        {
            return this.listenerSystem.GetTransactions(searchCriteria);
        }
    }
}
