// <copyright file="DeviceUpdateMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    public class DeviceUpdateMessage
    {
        public string EquipmentNumber { get; set; }

        public string EquipmentType { get; set; }

        public string CompanyId { get; set; }
    }
}
