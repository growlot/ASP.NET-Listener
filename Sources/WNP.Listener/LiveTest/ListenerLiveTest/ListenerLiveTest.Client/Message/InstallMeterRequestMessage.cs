using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Message
{
    using AMSLLC.Listener.Client.Message;
    public class InstallMeterRequestMessage : BaseListenerRequestMessage
    {
        public string Test { get; set; }

        public string UserName { get; set; }

        public string EntityKey { get; set; }

        public InstallMeterRequestMessage()
            : base("EM", "Install")
        {
        }
    }
}
