// //-----------------------------------------------------------------------
// // <copyright file="RecordKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Transaction record key builder
    /// </summary>
    public class RecordKeyBuilder : IRecordKeyBuilder
    {
        /// <summary>
        /// Create new transaction record key
        /// </summary>
        /// <returns>System.String.</returns>
        public string Create()
        {
            return Guid.NewGuid().ToString("D");
        }
    }
}