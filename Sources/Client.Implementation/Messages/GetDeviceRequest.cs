//-----------------------------------------------------------------------
// <copyright file="GetDeviceRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;
    using AMSLLC.Listener.Common.Lookup;

    /// <summary>
    /// Request message type for device retrieval
    /// </summary>
    public class GetDeviceRequest : DeviceRequest
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the tester identifier.
        /// </summary>
        /// <value>
        /// The tester identifier.
        /// </value>
        public string TesterId { get; set; }

        /// <summary>
        /// Gets or sets the test standard.
        /// </summary>
        /// <value>
        /// The test standard.
        /// </value>
        public string TestStandard { get; set; }
    }
}
