// <copyright file="DeviceTestResultRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    public class DeviceTestResultRequestMessage : BaseListenerRequestMessage
    {
        public DeviceTestResultRequestMessage(string entityCategory)
            : base(entityCategory, "Test")
        {
        }

        public string EntityKey { get; set; }

        public DateTime TestDate { get; set; }

        public string Owner { get; set; }
    }
}
