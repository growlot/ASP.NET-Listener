using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class OpenTransactionRequestMessage
    {
        public string SourceApplicationId { get; set; }
        public string OperationKey { get; set; }
        public string CompanyId { get; set; }
    }
}
