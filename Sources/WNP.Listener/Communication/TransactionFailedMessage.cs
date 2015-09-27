using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class TransactionFailedMessage
    {
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
