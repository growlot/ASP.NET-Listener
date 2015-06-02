//-----------------------------------------------------------------------
// <copyright file="FileFixedModeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible fixed field length file mode types
    /// </summary>
    public enum FileFixedModeLookup
    {
        /// <summary>
        /// The records must have the length equals to the sum of each field length.
        /// </summary>
        ExactLength = 0,

        /// <summary>
        /// The records can contain more chars in the last field.
        /// </summary>
        AllowMoreChars = 1,

        /// <summary>
        /// The records can contain less chars in the last field.
        /// </summary>
        AllowLessChars = 2,

        /// <summary>
        /// The records can contain more or less chars in the last field.
        /// </summary>
        AllowVariableLength = 3
    }
}