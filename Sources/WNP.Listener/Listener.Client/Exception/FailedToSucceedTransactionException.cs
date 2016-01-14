// <copyright file="FailedToSucceedTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom exception for an error while trying to mark transaction as successfull.
    /// </summary>
    [Serializable]
    public class FailedToSucceedTransactionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToSucceedTransactionException"/> class.
        /// </summary>
        public FailedToSucceedTransactionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToSucceedTransactionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FailedToSucceedTransactionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToSucceedTransactionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public FailedToSucceedTransactionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToSucceedTransactionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected FailedToSucceedTransactionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}