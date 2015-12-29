// <copyright file="DeviceUpdateMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    /// <summary>
    /// Class DeviceUpdateMessage.
    /// </summary>
    public class DeviceUpdateMessage
    {
        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>The equipment number.</value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>The type of the equipment.</value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }
    }
}
