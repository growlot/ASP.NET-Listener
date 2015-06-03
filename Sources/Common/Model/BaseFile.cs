//-----------------------------------------------------------------------
// <copyright file="BaseFile.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing common fields for all flat file definition entities
    /// </summary>
    public class BaseFile
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the external system for file format definition.
        /// </summary>
        /// <value>
        /// The external system.
        /// </value>
        public virtual ExternalSystem ExternalSystem { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this file definition is provided with the system or created by user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if provided with system; otherwise, <c>false</c>.
        /// </value>
        public virtual bool System { get; set; }
    }
}