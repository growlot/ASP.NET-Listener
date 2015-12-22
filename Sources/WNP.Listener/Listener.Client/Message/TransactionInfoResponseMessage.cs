// <copyright file="TransactionInfoResponseMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;
    using Shared;

    public class TransactionInfoResponseMessage
    {
        public string OperationKey { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }

        public string EntityCategory { get; set; }

        public string EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the event start date.
        /// Event example: Test
        /// </summary>
        /// <value>The event start date.</value>
        public DateTime? EventStartDate { get; set; }

        public TransactionStatusType TransactionStatus { get; set; }

        public string Message { get; set; }

        public string Debug { get; set; }

        public string TransactionSource { get; set; }
    }
}
