using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
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
