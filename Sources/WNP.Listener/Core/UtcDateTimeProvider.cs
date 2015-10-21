// <copyright file="UtcDateTimeProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// Provides time in UTC.
    /// </summary>
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Get current time.
        /// </summary>
        /// <returns>
        /// The current time.
        /// </returns>
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}
