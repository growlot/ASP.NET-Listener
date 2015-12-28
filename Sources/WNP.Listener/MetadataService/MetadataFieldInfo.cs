// <copyright file="MetadataFieldInfo.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    using System;

    /// <summary>
    /// Metadata information about database field.
    /// </summary>
    public class MetadataFieldInfo
    {
        /// <summary>
        /// Gets or sets the type of the data in the field.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the clr data type of the field.
        /// </summary>
        /// <value>
        /// The CLR type of the field.
        /// </value>
        public Type ClrDataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field is a part of primary key.
        /// </summary>
        /// <value>
        /// <c>true</c> if this field is part of primary key; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryKey { get; set; }
    }
}
