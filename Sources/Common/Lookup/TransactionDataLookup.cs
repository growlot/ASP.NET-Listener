//-----------------------------------------------------------------------
// <copyright file="TransactionDataLookup.cs" company="Advanced Metering Services LLC">
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
    /// Lists possible transaction data values.
    /// </summary>
    public enum TransactionDataLookup
    {
        /// <summary>
        /// No value, should never be used
        /// </summary>
        None = 0,

        /// <summary>
        /// The transaction processes device data
        /// </summary>
        Device = 1,

        /// <summary>
        /// The transaction processes device test data
        /// </summary>
        DeviceTest = 2,

        /// <summary>
        /// The transaction processes barcode data
        /// </summary>
        Barcode = 3,

        /// <summary>
        /// The transaction processes new batch data
        /// </summary>
        NewBatch = 4,

        /// <summary>
        /// The transaction processes site data
        /// </summary>
        Site = 5
    }
}
