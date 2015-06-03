//-----------------------------------------------------------------------
// <copyright file="FileFixedLength.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing FileFixedLength entities
    /// </summary>
    public class FileFixedLength : BaseFile
    {
        /// <summary>
        /// Gets or sets the file fixed mode.
        /// </summary>
        /// <value>
        /// The file fixed mode.
        /// </value>
        public virtual FileFixedMode FileFixedMode { get; set; }
    }
}