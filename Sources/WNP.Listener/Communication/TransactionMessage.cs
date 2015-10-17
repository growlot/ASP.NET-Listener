using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class TransactionMessage
    {
        public object Data { get; set; }
        public string RecordKey { get; set; }
        public Dictionary<string, object> Header { get; set; }
    }
}
