using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Message
{
    public abstract class BaseRequestMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRequestMessage"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="opearation">The opearation.</param>
        protected BaseRequestMessage(string category, string opearation)
        {
            EntityCategory = category;
            OperationKey = opearation;
        }

        public string EntityCategory { get; }
        public string OperationKey { get; }
    }
}
