using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Message
{
    public class OpenTransactionRequestMessage : BaseRequestMessage
    {
        public OpenTransactionRequestMessage(string category, string opearation) : base(category, opearation)
        {
        }

        public string EntityKey { get; set; }
    }
}
