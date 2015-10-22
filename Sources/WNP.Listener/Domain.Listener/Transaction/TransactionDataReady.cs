// //-----------------------------------------------------------------------
// <copyright file="TransactionDataReady.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Transaction data ready
    /// </summary>
    public class TransactionDataReady : IEvent
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the transaction record key.
        /// </summary>
        /// <value>
        /// The transaction record key.
        /// </value>
        public string RecordKey { get; set; }
    }
}