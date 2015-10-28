// //-----------------------------------------------------------------------
// <copyright file="TransactionRegistryMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Communication;

    /// <summary>
    /// Transaction Registry Memento.
    /// </summary>
    public class TransactionRegistryMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryMemento" /> class.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="recordKey">The record key.</param>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <param name="status">The status.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="createdDateTime">The created date time.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <param name="data">The request data.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="childTransactions">The child transactions.</param>
        public TransactionRegistryMemento(
            int transactionId,
            string recordKey,
            string transactionKey,
            string companyCode,
            string applicationKey,
            string operationKey,
            TransactionStatusType status,
            string userName,
            DateTime createdDateTime,
            DateTime? updatedDateTime,
            string data,
            string message,
            string details,
            int enabledOperationId,
            IEnumerable<TransactionRegistryMemento> childTransactions)
        {
            this.TransactionId = transactionId;
            this.RecordKey = recordKey;
            this.CompanyCode = companyCode;
            this.ApplicationKey = applicationKey;
            this.OperationKey = operationKey;
            this.Status = status;
            this.UserName = userName;
            this.CreatedDateTime = createdDateTime;
            this.UpdatedDateTime = updatedDateTime;
            this.Data = data;
            this.Message = message;
            this.Details = details;
            this.TransactionKey = transactionKey;
            this.EnabledOperationId = enabledOperationId;
            this.ChildTransactions = new ReadOnlyCollection<TransactionRegistryMemento>(new List<TransactionRegistryMemento>(childTransactions ?? new TransactionRegistryMemento[0]));
        }

        /// <summary>
        /// Gets or sets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string TransactionKey { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public string RecordKey { get; private set; }

        /// <summary>
        /// Gets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; private set; }

        /// <summary>
        /// Gets the application key.
        /// </summary>
        /// <value>The application key.</value>
        public string ApplicationKey { get; private set; }

        /// <summary>
        /// Gets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public TransactionStatusType Status { get; private set; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the created date time.
        /// </summary>
        /// <value>The created date time.</value>
        public DateTime CreatedDateTime { get; private set; }

        /// <summary>
        /// Gets the updated date time.
        /// </summary>
        /// <value>The updated date time.</value>
        public DateTime? UpdatedDateTime { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; private set; }

        /// <summary>
        /// Gets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EnabledOperationId { get; private set; }

        /// <summary>
        /// Gets the child transactions.
        /// </summary>
        /// <value>The child transactions.</value>
        public ReadOnlyCollection<TransactionRegistryMemento> ChildTransactions { get; private set; } = new ReadOnlyCollection<TransactionRegistryMemento>(new TransactionRegistryMemento[0]);
    }
}