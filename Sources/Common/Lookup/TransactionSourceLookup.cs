//-----------------------------------------------------------------------
// <copyright file="TransactionSourceLookup.cs" company="Advanced Metering Services LLC">
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
    /// Lists possible transaction source values.
    /// </summary>
    public enum TransactionSourceLookup
    {
        /// <summary>
        /// The transaction was initiated by WNP software
        /// </summary>
        WNP = 0,

        /// <summary>
        /// The transaction was initiated by incoming web service call
        /// </summary>
        WebServiceCall = 1
    }
}
