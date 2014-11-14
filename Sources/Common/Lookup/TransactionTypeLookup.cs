//-----------------------------------------------------------------------
// <copyright file="TransactionTypeLookup.cs" company="Advanced Metering Services LLC">
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
    /// Lists possible transaction types.
    /// </summary>
    public enum TransactionTypeLookup
    {
        /// <summary>
        /// No value, should never be used
        /// </summary>
        None = 0,

        /// <summary>
        /// Send the test data to external system.
        /// </summary>
        SendTestData = 100,

        /// <summary>
        /// Send the test data to file
        /// </summary>
        SendTestDataToFile = 110,

        /// <summary>
        /// Get the device from external system.
        /// </summary>
        GetDevice = 400,

        /// <summary>
        /// Send device data to external system.
        /// </summary>
        SendDevice = 701,

        /// <summary>
        /// Send device status update to external system.
        /// </summary>
        SendDeviceStatusUpdate = 801,

        /// <summary>
        /// Get the classification codes from external system.
        /// </summary>
        GetClassificationCodes = 1200
    }
}
