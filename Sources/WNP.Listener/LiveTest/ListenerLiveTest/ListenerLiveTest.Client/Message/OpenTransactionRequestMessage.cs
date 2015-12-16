using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Message
{
    using AMSLLC.Listener.Client.Message;

    public class OpenTransactionRequestMessage : BaseListenerRequestMessage
    {
        public OpenTransactionRequestMessage(string category, string opearation) : base(category, opearation)
        {
        }

        public string EntityKey { get; set; }
    }
}
