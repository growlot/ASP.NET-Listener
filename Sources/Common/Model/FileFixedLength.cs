//-----------------------------------------------------------------------
// <copyright file="FileFixedLength.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System.Collections.Generic;

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

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "At least protected modifier is required for nHibernate.")]
        public virtual IList<FileFieldFixedLength> Fields { get; protected set; }
    }
}