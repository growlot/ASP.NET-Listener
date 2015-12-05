// <copyright file="InvalidNumberOfRecordsException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class InvalidNumberOfRecordsException : Exception
    {
        public InvalidNumberOfRecordsException(string message) : base(message)
        {
        }
    }
}