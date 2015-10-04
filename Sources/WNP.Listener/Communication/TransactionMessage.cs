using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class TransactionMessage
    {
        public object Data { get; set; }
        public string EntityCategory { get; set; }
        public string EntityKey { get; set; }
        public string TransactionKey { get; set; }
    }
}
