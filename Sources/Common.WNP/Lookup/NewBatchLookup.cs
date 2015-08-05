//-----------------------------------------------------------------------
// <copyright file="NewBatchLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// List of possible new batch status values
    /// </summary>
    public enum NewBatchLookup
    {
        /// <summary>
        /// Status unknown.
        /// </summary>
        [Description("")]
        None,

        /// <summary>
        /// The new batch is accepted.
        /// </summary>
        [Description("A")]
        Accepted,

        /// <summary>
        /// The new batch is new. Not all required devices are tested.
        /// </summary>
        [Description("N")]
        New,

        /// <summary>
        /// The new batch is pending. All required devices are tested, but batch is not accepted or rejected.
        /// </summary>
        [Description("P")]
        Pending,

        /// <summary>
        /// The new batch is rejected. All devices are removed from WNP.
        /// </summary>
        [Description("R")]
        Rejected,

        /// <summary>
        /// The new batch is returned to vendor.
        /// </summary>
        [Description("V")]
        ReturnedToVendor
    }
}