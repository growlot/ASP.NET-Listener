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
    public class GetDeviceRequest
    {
        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

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

        /// <summary>
        /// Gets or sets the listener URL.
        /// </summary>
        /// <value>
        /// The listener URL.
        /// </value>
        public Uri ListenerUrl { get; set; }
    }
}
