using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Communication
{
    public class TransactionSkippedRequestMessage
    {
        /// <summary>
        /// Gets or sets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string TransactionKey { get; set; }
    }
}
