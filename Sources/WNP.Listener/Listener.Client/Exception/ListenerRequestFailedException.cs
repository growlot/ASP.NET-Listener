// <copyright file="ListenerRequestFailedException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Exception
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Runtime.Serialization;
    using Exception = System.Exception;

    /// <summary>
    /// Custom exception for Listener request failure.
    /// </summary>
    [Serializable]
    public class ListenerRequestFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestFailedException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public ListenerRequestFailedException(HttpStatusCode statusCode)
            : base(string.Format(CultureInfo.CurrentCulture, "Listener request failed with status code: {0}", statusCode))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestFailedException"/> class.
        /// </summary>
        public ListenerRequestFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestFailedException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public ListenerRequestFailedException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestFailedException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ListenerRequestFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestFailedException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected ListenerRequestFailedException(SerializationInfo info, StreamingContext streamingContext)
            : base(info, streamingContext)
        {
        }
    }
}
