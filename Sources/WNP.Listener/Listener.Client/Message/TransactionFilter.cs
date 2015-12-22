// <copyright file="TransactionFilter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;
    using System.Collections.Generic;
    using Shared;

    public class TransactionFilter
    {
        public string EntityCategory { get; set; }

        public string EntityKey { get; set; }

        public string BatchNumber { get; set; }

        public List<TransactionStatusType> StatusTypes { get; } = new List<TransactionStatusType>();

        public DateTime? TransactionDate { get; set; }
    }
}