//-----------------------------------------------------------------------
// <copyright file="ServiceType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing ServiceType
    /// </summary>
    public class ServiceType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceType"/> class.
        /// </summary>
        public ServiceType()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceType"/> class.
        /// </summary>
        /// <param name="id">The service type identifier.</param>
        public ServiceType(int id)
        {
            this.Id = id;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the service type identifier.
        /// </summary>
        /// <value>
        /// The service type identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the external code.
        /// </summary>
        /// <value>
        /// The external code.
        /// </value>
        public string ExternalCode { get; set; }

        /// <summary>
        /// Gets or sets the internal code.
        /// </summary>
        /// <value>
        /// The internal code.
        /// </value>
        public string InternalCode { get; set; }

        /// <summary>
        /// Gets or sets the service type description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}