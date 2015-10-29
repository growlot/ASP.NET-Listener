// //-----------------------------------------------------------------------
// <copyright file="TransactionSkipped.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Transaction Skipped event data
    /// </summary>
    public class TransactionSkipped : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSkipped" /> class.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        public TransactionSkipped(Guid recordKey)
        {
            this.RecordKey = recordKey;
        }

        /// <summary>
        /// Gets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public Guid RecordKey { get; }
    }
}