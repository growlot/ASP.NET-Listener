// //-----------------------------------------------------------------------
// // <copyright file="IRecordKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Transaction key builder interface
    /// </summary>
    public interface IRecordKeyBuilder
    {
        /// <summary>
        /// Create new transaction key
        /// </summary>
        /// <returns>System.String.</returns>
        string Create();
    }
}