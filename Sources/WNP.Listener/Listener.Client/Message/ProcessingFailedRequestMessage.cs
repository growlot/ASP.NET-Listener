using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
    public class ProcessingFailedRequestMessage
    {
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
