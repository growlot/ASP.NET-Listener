// <copyright file="IStringToTypeConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    /// <summary>
    /// Interface for string convertion to specific type.
    /// </summary>
    public interface IStringToTypeConverter
    {
        /// <summary>
        /// Converts the specified string to specific object.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The converted object.</returns>
        object Convert(string source);
    }
}
