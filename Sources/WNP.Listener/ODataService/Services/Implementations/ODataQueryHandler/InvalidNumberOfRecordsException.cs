// <copyright file="InvalidNumberOfRecordsException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.ODataQueryHandler
{
    using System;

    /// <summary>
    /// The exception that is thrown if number of records fetched is not expected.
    /// </summary>
    public class InvalidNumberOfRecordsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNumberOfRecordsException"/> class.
        /// </summary>
        /// <param name="message">The actual error message</param>
        public InvalidNumberOfRecordsException(string message)
            : base(message)
        {
        }
    }
}