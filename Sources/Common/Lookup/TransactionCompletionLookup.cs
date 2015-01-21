//-----------------------------------------------------------------------
// <copyright file="TransactionCompletionLookup.cs" company="Advanced Metering Services LLC">
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
    /// Lists possible transaction completion values.
    /// </summary>
    public enum TransactionCompletionLookup
    {
        /// <summary>
        /// No value, should never be used
        /// </summary>
        None = 0,

        /// <summary>
        /// The transaction will be finished at the end of call
        /// </summary>
        Default = 1,

        /// <summary>
        /// The transaction will be left in progress until callback is received from external system
        /// </summary>
        Callback = 2,

        /// <summary>
        /// Periodic checks to external system will be performed until transaction status is provided by external system.
        /// </summary>
        Check = 3
    }
}