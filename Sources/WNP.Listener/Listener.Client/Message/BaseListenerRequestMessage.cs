// <copyright file="BaseListenerRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    public abstract class BaseListenerRequestMessage
    {
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

        public string EntityCategory { get; }

        public string OperationKey { get; }
    }
}
