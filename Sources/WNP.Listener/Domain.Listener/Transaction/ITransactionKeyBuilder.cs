// //-----------------------------------------------------------------------
// // <copyright file="ITransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Transaction key builder
    /// </summary>
    public interface ITransactionKeyBuilder
    {
        /// <summary>
        /// Creates the transaction key
        /// </summary>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>Transaction key</returns>
        string Create(int enabledOperationId, object data);
    }
}