// <copyright file="DeviceTestResultMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    public class DeviceTestResultMessage
    {
        public string EquipmentNumber { get; set; }

        public string EquipmentType { get; set; }

        public string CompanyId { get; set; }

        public DateTime TestDate { get; set; }
    }
}
