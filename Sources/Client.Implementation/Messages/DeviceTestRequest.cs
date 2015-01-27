//-----------------------------------------------------------------------
// <copyright file="DeviceTestRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;
    using AMSLLC.Listener.Common.Lookup;

    /// <summary>
    /// Request message type for device shop test
    /// </summary>
    public class DeviceTestRequest : DeviceRequest
    {
        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        public DateTime TestDate { get; set; }
    }
}
