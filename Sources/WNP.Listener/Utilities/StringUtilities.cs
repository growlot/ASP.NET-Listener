// //-----------------------------------------------------------------------
// <copyright file="StringUtilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines custom extensions for <see cref="string"/> type
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// Converts interpolated string to culture invariant.
        /// </summary>
        /// <param name="formattable">The interpolated string.</param>
        /// <returns>Culture invariant string.</returns>
        public static string Invariant(FormattableString formattable)
        {
            if (formattable == null)
            {
                return null;
            }

            return formattable.ToString(CultureInfo.InvariantCulture);
        }
    }
}