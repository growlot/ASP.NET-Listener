//-----------------------------------------------------------------------
// <copyright file="FileFieldFixedLength.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing FileFieldDelimited entities
    /// </summary>
    public class FileFieldFixedLength : BaseFileField
    {
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public virtual FileFixedLength File { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public virtual int Length { get; set; }

        /// <summary>
        /// Gets or sets the align character.
        /// </summary>
        /// <value>
        /// The align character.
        /// </value>
        public virtual char AlignChar { get; set; }

        /// <summary>
        /// Gets or sets the align mode.
        /// </summary>
        /// <value>
        /// The align mode.
        /// </value>
        public virtual FileAlignMode AlignMode { get; set; }
    }
}