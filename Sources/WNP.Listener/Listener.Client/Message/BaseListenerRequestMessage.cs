// <copyright file="BaseListenerRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    /// <summary>
    /// Base class for any Listener request.
    /// </summary>
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

        /// <summary>
        /// Gets the entity category.
        /// </summary>
        /// <value>The entity category.</value>
        public string EntityCategory { get; }

        /// <summary>
        /// Gets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; }
    }
}
