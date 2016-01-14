// <copyright file="TransactionFilter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Shared;

    /// <summary>
    /// Filters that can be used while retrieving transaction logs.
    /// </summary>
    public class TransactionFilter
    {
        /// <summary>
        /// Gets or sets the entity category.
        /// </summary>
        /// <value>
        /// The entity category.
        /// </value>
        public string EntityCategory { get; set; }

        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        /// <value>
        /// The entity key.
        /// </value>
        public string EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        public string BatchNumber { get; set; }

        /// <summary>
        /// Gets the status types.
        /// </summary>
        /// <value>
        /// The status types.
        /// </value>
        public ICollection<TransactionStatusType> StatusTypes { get; } = new Collection<TransactionStatusType>();

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        /// <value>
        /// The transaction date.
        /// </value>
        public DateTime? TransactionDate { get; set; }
    }
}