//-----------------------------------------------------------------------
// <copyright file="TransactionLog.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing transaction log 
    /// </summary>
    public class TransactionLog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLog"/> class.
        /// </summary>
        public TransactionLog()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLog"/> class.
        /// </summary>
        /// <param name="id">The transaction identifier.</param>
        public TransactionLog(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>
        /// The transaction status.
        /// </value>
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public Device Device { get; set; }

        /// <summary>
        /// Gets or sets the device test.
        /// </summary>
        /// <value>
        /// The device test.
        /// </value>
        public DeviceTest DeviceTest { get; set; }

        /// <summary>
        /// Gets or sets the batch.
        /// </summary>
        /// <value>
        /// The batch.
        /// </value>
        public Batch Batch { get; set; }

        /// <summary>
        /// Gets or sets the transaction start.
        /// </summary>
        /// <value>
        /// The transaction start.
        /// </value>
        public DateTime? TransactionStart { get; set; }

        /// <summary>
        /// Gets or sets the transaction end.
        /// </summary>
        /// <value>
        /// The transaction end.
        /// </value>
        public DateTime? TransactionEnd { get; set; }

        /// <summary>
        /// Gets the transaction log state.
        /// </summary>
        /// <value>
        /// The list of states that transaction has passed.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive. Private setter is used by NHibernate")]
        public IList<TransactionLogState> TransactionLogState { get; private set; }

        /// <summary>
        /// Gets or sets the message related to this transaction.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the debug information needed to investigate transaction failure reasons.
        /// </summary>
        /// <value>
        /// The debug information.
        /// </value>
        public string DebugInfo { get; set; }

        /// <summary>
        /// Gets or sets the data hash.
        /// </summary>
        /// <value>
        /// The data hash. Used to track if integration data has changed.
        /// </value>
        public string DataHash { get; set; }
    }
}
