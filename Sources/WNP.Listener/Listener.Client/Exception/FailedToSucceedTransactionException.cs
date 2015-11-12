// <copyright file="FailedToSucceedTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Runtime.Serialization;

    public class FailedToSucceedTransactionException : Exception
    {
        public FailedToSucceedTransactionException()
        {
        }

        public FailedToSucceedTransactionException(string message)
            : base(message)
        {
        }

        public FailedToSucceedTransactionException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        protected FailedToSucceedTransactionException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}