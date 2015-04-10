//-----------------------------------------------------------------------
// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing circuit.
    /// </summary>
    public class Circuit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        public Circuit()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Circuit(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
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
        /// Gets or sets the site.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public Site Site { get; set; }

        /// <summary>
        /// Gets or sets the circuit number.
        /// </summary>
        /// <value>
        /// The circuit number.
        /// </value>
        public int CircuitIndex { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal Latitude { get; set; }
    }
}
