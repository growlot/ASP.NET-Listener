// //-----------------------------------------------------------------------
// // <copyright file="TransactionRequestMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    public class TransactionRequestMessage
    {
        public string SourceApplicationId { get; set; }
        public string DestinationApplicationId { get; set; }
        public string OperationKey { get; set; }
        public string TransactionId { get; set; }
    }
}