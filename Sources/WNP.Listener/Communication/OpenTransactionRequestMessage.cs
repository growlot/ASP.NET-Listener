// //-----------------------------------------------------------------------
// // <copyright file="OpenTransactionRequestMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    using System.Collections.Generic;

    public class OpenTransactionRequestMessage
    {
        public string SourceApplicationKey { get; set; }
        public string OperationKey { get; set; }
        public string CompanyCode { get; set; }
        public string User { get; set; }
        public string Data { get; set; }
        public Dictionary<string, object> Header { get; } = new Dictionary<string, object>();
    }
}