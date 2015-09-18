// //-----------------------------------------------------------------------
// // <copyright file="CommonUtilities.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Core
{
    /// <summary>
    /// Common utilities
    /// </summary>
    public static class CommonUtilities
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}