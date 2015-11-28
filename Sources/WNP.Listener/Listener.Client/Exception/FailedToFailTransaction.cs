using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Exception
{
    using System.Runtime.Serialization;
    using Exception = System.Exception;

    public class FailedToFailTransaction : Exception
    {
        public FailedToFailTransaction()
        {
        }

        public FailedToFailTransaction(string message)
            : base(message)
        {
        }

        public FailedToFailTransaction(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        protected FailedToFailTransaction(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
