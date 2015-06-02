//-----------------------------------------------------------------------
// <copyright file="FileQuoteModeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible flat file field quote mode types
    /// </summary>
    public enum FileQuoteModeLookup
    {
        /// <summary>
        /// Field is always quoted
        /// </summary>
        AlwaysQuoted = 0,

        /// <summary>
        /// Field can be non-quoted when reading
        /// </summary>
        OptionalForRead = 1,

        /// <summary>
        /// Field can be non-quoted when writing
        /// </summary>
        OptionalForWrite = 2,

        /// <summary>
        /// Field can be non-quoted
        /// </summary>
        OptionalForBoth = 3
    }
}