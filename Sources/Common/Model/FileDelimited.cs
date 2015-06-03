//-----------------------------------------------------------------------
// <copyright file="FileDelimited.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System.Collections.Generic;

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

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "At least protected modifier is required for nHibernate.")]
        public virtual IList<FileFieldDelimited> Fields { get; protected set; }
    }
}