//-----------------------------------------------------------------------
// <copyright file="FileAlignModeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible flat file field alignment mode types
    /// </summary>
    public enum FileAlignModeLookup
    {
        /// <summary>
        /// Field is aligned to the left
        /// </summary>
        Left = 0,

        /// <summary>
        /// Field is centered
        /// </summary>
        Center = 1,

        /// <summary>
        /// Field is aligned to the right
        /// </summary>
        Right = 2
    }
}