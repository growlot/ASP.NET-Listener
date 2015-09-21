// //-----------------------------------------------------------------------
// // <copyright file="ApiResponseMessage.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System.Collections.Generic;
    using Core;

    public class ApiResponseMessage
    {
        public object Data { get; set; }
        public List<StatusMessage> Messages { get; } = new List<StatusMessage>();
        public bool Success { get; set; }
    }
}