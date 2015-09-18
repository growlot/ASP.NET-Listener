using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WNP.Listener.Communication
{
    public class TransactionRequestMessage
    {
        public int SourceApplicationId { get; set; }
        public int DestinationApplicationId { get; set; }
        public string OperationKey { get; set; }
    }
}
