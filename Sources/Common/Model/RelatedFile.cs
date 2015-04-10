//-----------------------------------------------------------------------
// <copyright file="RelatedFile.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;

    /// <summary>
    /// Data model class representing RelatedFile entity
    /// </summary>
    public class RelatedFile
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the index of the file.
        /// </summary>
        /// <value>
        /// The index of the file.
        /// </value>
        public int FileIndex { get; set; }

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        /// <value>
        /// The content of the file.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Needed for WCF")]
        public byte[] FileContent { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }
    }
}