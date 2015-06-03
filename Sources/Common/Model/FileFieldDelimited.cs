//-----------------------------------------------------------------------
// <copyright file="FileFieldDelimited.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing FileFieldDelimited entities
    /// </summary>
    public class FileFieldDelimited : BaseFileField
    {
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public virtual FileDelimited File { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is quoted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is quoted; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsQuoted { get; set; }

        /// <summary>
        /// Gets or sets the quote character.
        /// </summary>
        /// <value>
        /// The quote character.
        /// </value>
        public virtual char QuoteChar { get; set; }

        /// <summary>
        /// Gets or sets the quote mode.
        /// </summary>
        /// <value>
        /// The quote mode.
        /// </value>
        public virtual FileQuoteMode QuoteMode { get; set; }

        /// <summary>
        /// Gets or sets the quote multiline mode.
        /// </summary>
        /// <value>
        /// The quote multiline mode.
        /// </value>
        public virtual FileQuoteMultiline QuoteMultiline { get; set; }
    }
}