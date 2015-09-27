// //-----------------------------------------------------------------------
// // <copyright file="ProcessTransactionRequestMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    public class ProcessTransactionRequestMessage
    {
        public string TransactionId { get; set; }
        public object Data { get; set; }
    }
}