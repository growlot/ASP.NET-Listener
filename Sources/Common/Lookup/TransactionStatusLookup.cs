//-----------------------------------------------------------------------
// <copyright file="TransactionStatusLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Lists possible transaction statuses.
    /// </summary>
    public enum TransactionStatusLookup
    {
        /// <summary>
        /// The transaction has succeeded
        /// </summary>
        Succeeded = 0,

        /// <summary>
        /// The transaction is in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The transaction has failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// The transaction has been skipped
        /// </summary>
        Skipped = 3
    }
}
