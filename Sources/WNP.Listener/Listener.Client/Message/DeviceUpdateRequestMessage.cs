using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
    public class DeviceUpdateRequestMessage : BaseListenerRequestMessage
    {
        public DeviceUpdateRequestMessage(string entityCategory)
            : base(entityCategory, "Update")
        {
        }

        public string EntityKey { get; set; }
        public string Owner { get; set; }
    }
}
