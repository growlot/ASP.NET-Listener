// //-----------------------------------------------------------------------
// // <copyright file="OpenTransactionRequestMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    public class OpenTransactionRequestMessage
    {
        public string SourceApplicationKey { get; set; }
        public string OperationKey { get; set; }
        public string CompanyCode { get; set; }
        public string User { get; set; }
        public object Data { get; set; }
    }
}