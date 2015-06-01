//-----------------------------------------------------------------------
// <copyright file="FileConverterKindLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    /// <summary>
    /// List of possible flat file converter types
    /// </summary>
    public enum FileConverterKindLookup
    {
        /// <summary>
        /// File converter is not used
        /// </summary>
        None = 0,

        /// <summary>
        /// File converter for date type
        /// </summary>
        Date = 1,

        /// <summary>
        /// File converter for boolean type
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// File converter for byte type
        /// </summary>
        Byte = 3,

        /// <summary>
        /// File converter for 16 bit integer type
        /// </summary>
        Int16 = 4,

        /// <summary>
        /// File converter for 32 bit integer type
        /// </summary>
        Int32 = 5,

        /// <summary>
        /// File converter for 64 bit integer type
        /// </summary>
        Int64 = 6,

        /// <summary>
        /// File converter for decimal type
        /// </summary>
        Decimal = 7,

        /// <summary>
        /// File converter for double type
        /// </summary>
        Double = 8,

        /// <summary>
        /// File converter for double converted to percent type
        /// </summary>
        PercentDouble = 9,

        /// <summary>
        /// File converter for single type
        /// </summary>
        Single = 10,

        /// <summary>
        /// File converter for SByte type
        /// </summary>
        SByte = 11,

        /// <summary>
        /// File converter for 16 bit unsigned integer type
        /// </summary>
        UInt16 = 12,

        /// <summary>
        /// File converter for 32 bit unsigned integer type
        /// </summary>
        UInt32 = 13,

        /// <summary>
        /// File converter for 64 bit unsigned integer type
        /// </summary>
        UInt64 = 14,

        /// <summary>
        /// File converter for date that supports multiple formats type
        /// </summary>
        DateMultipleFormat = 15,

        /// <summary>
        /// File converter for char type
        /// </summary>
        Char = 16,

        /// <summary>
        /// File converter for GUID type
        /// </summary>
        Guid = 17
    }
}