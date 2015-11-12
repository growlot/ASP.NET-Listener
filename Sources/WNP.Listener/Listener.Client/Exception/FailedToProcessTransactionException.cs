// <copyright file="FailedToProcessTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Runtime.Serialization;

    public class FailedToProcessTransactionException : Exception
    {
        public FailedToProcessTransactionException()
        {
        }

        public FailedToProcessTransactionException(string message)
            : base(message)
        {
        }

        public FailedToProcessTransactionException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        protected FailedToProcessTransactionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}