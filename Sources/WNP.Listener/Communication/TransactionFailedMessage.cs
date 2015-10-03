// //-----------------------------------------------------------------------
// // <copyright file="TransactionFailedMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    public class TransactionFailedMessage
    {
        public string TransactionKey { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}