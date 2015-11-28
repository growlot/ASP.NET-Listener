// <copyright file="UnableToSucceedTransactionException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Exception
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception indicating that system was unable to succeed transaction
    /// </summary>
    public class UnableToSucceedTransactionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToSucceedTransactionException"/> class.
        /// </summary>
        /// <param name="recordKey">The record key.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public UnableToSucceedTransactionException(
            Guid recordKey,
            string message)
            : base("Unable to succeed transaction {0} due to:{1}".FormatWith(CultureInfo.InvariantCulture, recordKey, message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToSucceedTransactionException"/> class.
        /// </summary>
        public UnableToSucceedTransactionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToSucceedTransactionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnableToSucceedTransactionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToSucceedTransactionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UnableToSucceedTransactionException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}