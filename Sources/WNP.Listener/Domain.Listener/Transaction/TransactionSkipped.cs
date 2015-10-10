// //-----------------------------------------------------------------------
// // <copyright file="TransactionSkipped.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Transaction Skipped event data
    /// </summary>
    public class TransactionSkipped : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSkipped" /> class.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        public TransactionSkipped(string transactionKey)
        {
            this.TransactionKey = transactionKey;
        }

        /// <summary>
        /// Gets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string TransactionKey { get; }
    }
}