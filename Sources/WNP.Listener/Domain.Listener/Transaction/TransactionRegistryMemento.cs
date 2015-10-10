// //-----------------------------------------------------------------------
// // <copyright file="TransactionRegistryMemento.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using Communication;
    using Core;

    /// <summary>
    /// Transaction Registry Memento.
    /// </summary>
    public class TransactionRegistryMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryMemento" /> class.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <param name="status">The status.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="header">The header.</param>
        /// <param name="createdDateTime">The created date time.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <param name="data">The request data.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        public TransactionRegistryMemento(string transactionKey, string companyCode, string applicationKey,
            string operationKey, TransactionStatusType status, string userName, Dictionary<string, object> header, DateTime createdDateTime,
            DateTime? updatedDateTime, string data, string message, string details)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }

            this.TransactionKey = transactionKey;
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
            foreach (var o in header)
            {
                this.Header.Add(o.Key, o.Value);
            }
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>The header.</value>
        public Dictionary<string, object> Header { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string TransactionKey { get; private set; }

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
    }
}