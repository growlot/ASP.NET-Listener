// <copyright file="ChangeDeviceStatusMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    public class ChangeDeviceStatusMessage
    {
        public string EquipmentNumber { get; set; }

        public string EquipmentType { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CompanyId { get; set; }
    }
}
