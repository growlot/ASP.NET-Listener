//-----------------------------------------------------------------------
// <copyright file="TransactionLogResponse.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;

    /// <summary>
    /// Response message type for transaction log output used in WNP
    /// </summary>
    public class TransactionLogResponse
    {
        /// <summary>
        /// Gets or sets the transaction start.
        /// </summary>
        /// <value>
        /// The transaction start.
        /// </value>
        public DateTime TransactionStart { get; set; }

        /// <summary>
        /// Gets or sets the transaction source.
        /// </summary>
        /// <value>
        /// The transaction source.
        /// </value>
        public string TransactionSource { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>
        /// The transaction status.
        /// </value>
        public string TransactionStatus { get; set; }
        
        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date if transaction was sending test results. Null otherwise.
        /// </value>
        public DateTime? TestDate { get; set; }
    }
}
