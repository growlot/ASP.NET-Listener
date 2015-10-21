// <copyright file="IDateTimeProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// Interface to abstract implementation of DateTime provider
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Get current time.
        /// </summary>
        /// <returns>The current time.</returns>
        DateTime Now();
    }
}
