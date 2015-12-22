// <copyright file="ProcessingFailedRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    public class ProcessingFailedRequestMessage
    {
        public string Message { get; set; }

        public string Details { get; set; }
    }
}
