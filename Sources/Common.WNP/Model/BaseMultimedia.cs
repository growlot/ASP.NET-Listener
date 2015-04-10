//-----------------------------------------------------------------------
// <copyright file="BaseMultimedia.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing common fields for equipment and site multimedia 
    /// </summary>
    public class BaseMultimedia
    {
        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        /// <value>
        /// The comment identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the index of the comment.
        /// </summary>
        /// <value>
        /// The index of the comment.
        /// </value>
        public int FileIndex { get; set; }

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        /// <value>
        /// The file description.
        /// </value>
        public string FileDescription { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This is neede for nHibernate and WCF")]
        public byte[] FileContent { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created this comment.
        /// </summary>
        /// <value>
        /// The user who created this comment.
        /// </value>
        public string CreateUser { get; set; }
    }
}