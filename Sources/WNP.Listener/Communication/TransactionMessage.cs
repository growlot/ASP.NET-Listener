// <copyright file="TransactionMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication
{
    /// <summary>
    /// Transaction message
    /// </summary>
    public class TransactionMessage
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public string RecordKey { get; set; }
    }
}