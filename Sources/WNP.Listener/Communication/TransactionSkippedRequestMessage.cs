using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class TransactionSkippedRequestMessage
    {
        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public string RecordKey { get; set; }
    }
}
