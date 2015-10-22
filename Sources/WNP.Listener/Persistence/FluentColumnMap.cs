// <copyright file="FluentColumnMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence
{
    using System;

    /// <summary>
    /// Stores column mapping information for <see cref="FluentMapper{T}"/>
    /// </summary>
    public class FluentColumnMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColumnMap"/> class.
        /// </summary>
        public FluentColumnMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColumnMap"/> class.
        /// </summary>
        /// <param name="columnInfo">The column information.</param>
        public FluentColumnMap(ColumnInfo columnInfo)
            : this(columnInfo, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColumnMap"/> class.
        /// </summary>
        /// <param name="columnInfo">The column information.</param>
        /// <param name="fromDbConverter">From database converter.</param>
        public FluentColumnMap(ColumnInfo columnInfo, Func<object, object> fromDbConverter)
            : this(columnInfo, fromDbConverter, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColumnMap"/> class.
        /// </summary>
        /// <param name="columnInfo">The column information.</param>
        /// <param name="fromDbConverter">From database converter.</param>
        /// <param name="toDbConverter">To database converter.</param>
        public FluentColumnMap(ColumnInfo columnInfo, Func<object, object> fromDbConverter, Func<object, object> toDbConverter)
        {
            this.ColumnInfo = columnInfo;
            this.FromDbConverter = fromDbConverter;
            this.ToDbConverter = toDbConverter;
        }

        /// <summary>
        /// Gets the column information.
        /// </summary>
        /// <value>
        /// The column information.
        /// </value>
        public ColumnInfo ColumnInfo { get; private set; }

        /// <summary>
        /// Gets from database converter.
        /// </summary>
        /// <value>
        /// From database converter.
        /// </value>
        public Func<object, object> FromDbConverter { get; private set; }

        /// <summary>
        /// Gets to database converter.
        /// </summary>
        /// <value>
        /// To database converter.
        /// </value>
        public Func<object, object> ToDbConverter { get; private set; }
    }
}