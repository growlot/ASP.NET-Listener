// //-----------------------------------------------------------------------
// // <copyright file="TransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;
    using Domain.Listener.Transaction;

    public class TransactionKeyBuilder : ITransactionKeyBuilder
    {
        public string Create(int operationId, object data)
        {
            return "AAA";
        }
    }
}