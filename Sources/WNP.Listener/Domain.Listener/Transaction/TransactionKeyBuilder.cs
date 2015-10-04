// //-----------------------------------------------------------------------
// // <copyright file="TransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Transaction key builder
    /// </summary>
    public class TransactionKeyBuilder : ITransactionKeyBuilder
    {
        /// <summary>
        /// Create new transaction key
        /// </summary>
        /// <returns>System.String.</returns>
        public string Create()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}