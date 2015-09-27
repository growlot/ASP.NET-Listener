// //-----------------------------------------------------------------------
// // <copyright file="TransactionDataReady.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using Communication;

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
    }
}