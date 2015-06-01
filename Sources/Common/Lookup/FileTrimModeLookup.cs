//-----------------------------------------------------------------------
// <copyright file="FileTrimModeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible flat file field trim mode types
    /// </summary>
    public enum FileTrimModeLookup
    {
        /// <summary>
        /// Field is not trimmed
        /// </summary>
        None = 0,

        /// <summary>
        /// Field is trimmed from both ends
        /// </summary>
        Both = 1,

        /// <summary>
        /// Field is trimmed from left
        /// </summary>
        Left = 2,

        /// <summary>
        /// Field is trimmed from right
        /// </summary>
        Right = 3
    }
}