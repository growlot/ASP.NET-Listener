using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client.Message
{
    public abstract class BaseListenerRequestMessage
    {
        public string EntityCategory { get; }
        public string OperationKey { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseListenerRequestMessage"/> class.
        /// </summary>
        /// <param name="entityCategory">The entity category.</param>
        /// <param name="operationKey">The operation key.</param>
        protected BaseListenerRequestMessage(
            string entityCategory,
            string operationKey)
        {
            this.EntityCategory = entityCategory;
            this.OperationKey = operationKey;
        }
    }
}
