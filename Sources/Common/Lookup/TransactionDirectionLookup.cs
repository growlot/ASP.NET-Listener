//-----------------------------------------------------------------------
// <copyright file="TransactionDirectionLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Lists possible transaction direction values.
    /// </summary>
    public enum TransactionDirectionLookup
    {
        /// <summary>
        /// No value, should never be used
        /// </summary>
        None = 0,

        /// <summary>
        /// The data is saved to WNP during this transaction
        /// </summary>
        Incoming = 1,

        /// <summary>
        /// The data is sent to external system during this transaction
        /// </summary>
        Outgoing = 2
    }
}