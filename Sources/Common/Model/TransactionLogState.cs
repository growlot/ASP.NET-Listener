//-----------------------------------------------------------------------
// <copyright file="TransactionLogState.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing TransactionStatistics 
    /// </summary>
    public class TransactionLogState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLogState"/> class.
        /// </summary>
        public TransactionLogState()
        {
            this.Id = -1;
            this.TransactionLog = null;
            this.TransactionState = new TransactionState();
            this.ExecutionTime = new DateTime();
        }

        /// <summary>
        /// Gets or sets the transaction statistics identifier.
        /// </summary>
        /// <value>
        /// The transaction statistics identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>
        /// The transaction type identifier.
        /// </value>
        public TransactionLog TransactionLog { get; set; }

        /// <summary>
        /// Gets or sets the time when transaction entered this specified state.
        /// </summary>
        /// <value>
        /// The call time.
        /// </value>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the state of the transaction.
        /// </summary>
        /// <value>
        /// The state of the transaction.
        /// </value>
        public TransactionState TransactionState { get; set; }
    }
}
