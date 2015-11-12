// <copyright file="FailedToOpenTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Runtime.Serialization;

    public class FailedToOpenTransactionException : Exception
    {
        public FailedToOpenTransactionException()
        {
        }

        public FailedToOpenTransactionException(string message)
            : base(message)
        {
        }

        public FailedToOpenTransactionException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        protected FailedToOpenTransactionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}