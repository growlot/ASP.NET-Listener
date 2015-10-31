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
        /// Transaction is pending
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Transaction being processed
        /// </summary>
        Processing = 4,

        /// <summary>
        /// The transaction finished successfully
        /// </summary>
        Success = 0,

        /// <summary>
        /// The transaction failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// Transaction has been skipped
        /// </summary>
        Skipped = 3,

        /// <summary>
        /// Transaction is invalid. Indicates transactions, which should not be retried
        /// </summary>
        Invalid = 5,

        /// <summary>
        /// Transaction is canceled. Canceled transactions where not attempted to be processed nor they are going to be in a future
        /// Example of canceled transaction: Child transaction has been canceled if batch root transaction has been skipped
        /// </summary>
        Canceled = 6
    }
}
