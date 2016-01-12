// <copyright file="InvalidNumberOfRecordsException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Query
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown if number of records fetched is not expected.
    /// </summary>
    public class InvalidNumberOfRecordsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNumberOfRecordsException"/> class.
        /// </summary>
        public InvalidNumberOfRecordsException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNumberOfRecordsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidNumberOfRecordsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNumberOfRecordsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidNumberOfRecordsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNumberOfRecordsException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected InvalidNumberOfRecordsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}