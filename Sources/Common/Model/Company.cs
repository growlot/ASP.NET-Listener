//-----------------------------------------------------------------------
// <copyright file="Company.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing Company 
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        public Company()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        /// <param name="id">The company identifier.</param>
        public Company(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
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
        /// Gets or sets the company name.
        /// </summary>
        /// <value>
        /// The company name.
        /// </value>
        public string Name { get; set; }
    }
}