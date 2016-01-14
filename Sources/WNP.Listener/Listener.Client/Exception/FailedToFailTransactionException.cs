// <copyright file="FailedToFailTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Runtime.Serialization;
    using Exception = System.Exception;

    /// <summary>
    /// Custom exception for a case when failure occures while trying to mark transaction as failed.
    /// </summary>
    [Serializable]
    public class FailedToFailTransactionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToFailTransactionException"/> class.
        /// </summary>
        public FailedToFailTransactionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToFailTransactionException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FailedToFailTransactionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToFailTransactionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public FailedToFailTransactionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToFailTransactionException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected FailedToFailTransactionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
