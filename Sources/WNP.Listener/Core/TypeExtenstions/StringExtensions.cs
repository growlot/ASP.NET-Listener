// //-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace System
{
    using Globalization;

    /// <summary>
    /// Defines custom extensions for <see cref="string"/> type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Uses string as format string and provides arguments to this format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return format.FormatWith(CultureInfo.InvariantCulture, args);
        }

        /// <summary>
        /// Uses string as format string and provides arguments to this format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="provider">The object that supplies culture specific information.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string FormatWith(this string format, CultureInfo provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }
    }
}