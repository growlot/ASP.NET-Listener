// <copyright file="TransactionsCanceled.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Transactions Canceled domain event
    /// </summary>
    public class TransactionsCanceled : IDomainEvent
    {
        /// <summary>
        /// Gets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public Collection<Guid> RecordKeys { get; } = new Collection<Guid>();
    }
}