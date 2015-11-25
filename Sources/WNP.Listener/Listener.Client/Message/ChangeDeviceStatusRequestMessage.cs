using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
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
