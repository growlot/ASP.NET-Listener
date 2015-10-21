// <copyright file="TransactionStatusType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication
{
    /// <summary>
    /// Defines transaction statuses
    /// </summary>
    public enum TransactionStatusType
    {
        /// <summary>
        /// The transaction finished successfully
        /// </summary>
        Success = 0,

        /// <summary>
        /// The transaction is currently in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The transaction has failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// The transaction was skipped
        /// </summary>
        Skipped = 3
    }
}
