//-----------------------------------------------------------------------
// <copyright file="UpdateDeviceRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Device message format
    /// </summary>
    public class UpdateDeviceRequest
    {
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
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets the circuit index.
        /// </summary>
        /// <value>
        /// The circuit index.
        /// </value>
        public int? CircuitIndex { get; set; }

        /// <summary>
        /// Gets or sets the vehicle number.
        /// </summary>
        /// <value>
        /// The vehicle number.
        /// </value>
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the received by.
        /// </summary>
        /// <value>
        /// The received by.
        /// </value>
        public string ReceivedBy { get; set; }

        /// <summary>
        /// Gets or sets the test program.
        /// </summary>
        /// <value>
        /// The test program.
        /// </value>
        public string TestProgram { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the shop status.
        /// </summary>
        /// <value>
        /// The shop status.
        /// </value>
        public string ShopStatus { get; set; }

        /// <summary>
        /// Gets or sets the equipment status.
        /// </summary>
        /// <value>
        /// The equipment status.
        /// </value>
        public string EquipmentStatus { get; set; }
    }
}