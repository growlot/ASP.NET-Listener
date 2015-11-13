// <copyright file="RetryPolicyType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Transaction execution retry policy type
    /// </summary>
    public enum RetryPolicyType
    {
        /// <summary>
        /// The undefined policy type
        /// </summary>
        Undefined,

        /// <summary>
        /// No retry
        /// </summary>
        None,

        /// <summary>
        /// Simple retry
        /// </summary>
        Retry,

        /// <summary>
        /// Force non-successful
        /// </summary>
        Force
    }
}