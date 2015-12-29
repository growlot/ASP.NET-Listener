// <copyright file="TransactionInfoResponseMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;
    using Shared;

    /// <summary>
    /// Class TransactionInfoResponseMessage.
    /// </summary>
    public class TransactionInfoResponseMessage
    {
        /// <summary>
        /// Gets or sets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the entity category.
        /// </summary>
        /// <value>The entity category.</value>
        public string EntityCategory { get; set; }

        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        /// <value>The entity key.</value>
        public string EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the event start date.
        /// Event example: Test
        /// </summary>
        /// <value>The event start date.</value>
        public DateTime? EventStartDate { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>The transaction status.</value>
        public TransactionStatusType TransactionStatus { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the debug.
        /// </summary>
        /// <value>The debug.</value>
        public string Debug { get; set; }

        /// <summary>
        /// Gets or sets the transaction source.
        /// </summary>
        /// <value>The transaction source.</value>
        public string TransactionSource { get; set; }
    }
}
