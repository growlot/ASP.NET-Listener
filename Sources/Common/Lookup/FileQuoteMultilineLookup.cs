//-----------------------------------------------------------------------
// <copyright file="FileQuoteMultilineLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible flat file field quoted multiline mode types
    /// </summary>
    public enum FileQuoteMultilineLookup
    {
        /// <summary>
        /// Allow multiline quoted field for read and write
        /// </summary>
        AllowForBoth = 0,

        /// <summary>
        /// Allow multiline quoted field for read
        /// </summary>
        AllowForRead = 1,

        /// <summary>
        /// Allow multiline quoted field for write
        /// </summary>
        AllowForWrite = 2,

        /// <summary>
        /// Field can not be multiline
        /// </summary>
        NotAllow = 3
    }
}