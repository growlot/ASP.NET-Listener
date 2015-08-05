//-----------------------------------------------------------------------
// <copyright file="ExternalSystem.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data model class representing ExternalSystem
    /// </summary>
    public class ExternalSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalSystem"/> class.
        /// </summary>
        public ExternalSystem()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalSystem"/> class.
        /// </summary>
        /// <param name="id">The transaction data identifier.</param>
        public ExternalSystem(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the external system identifier.
        /// </summary>
        /// <value>
        /// The external system identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the external system name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the external system description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}