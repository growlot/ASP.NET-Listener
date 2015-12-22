// <copyright file="ChangeDeviceStatusRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    public class ChangeDeviceStatusRequestMessage : BaseListenerRequestMessage
    {
        public ChangeDeviceStatusRequestMessage(string entityCategory)
            : base(entityCategory, "StateChange")
        {
        }

        public string EntityKey { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
