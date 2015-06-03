//-----------------------------------------------------------------------
// <copyright file="FileDelimited.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing FileDelimited entities
    /// </summary>
    public class FileDelimited : BaseFile
    {
        /// <summary>
        /// Gets or sets the file delimiter.
        /// </summary>
        /// <value>
        /// The file delimiter.
        /// </value>
        public virtual string Delimiter { get; set; }
    }
}