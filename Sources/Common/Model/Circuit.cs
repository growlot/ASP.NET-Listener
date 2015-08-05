//-----------------------------------------------------------------------
// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Data model class representing Circuit entity
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
            this.Devices = new List<Device>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the index of the circuit.
        /// </summary>
        /// <value>
        /// The index of the circuit.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description of the circuit.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the site of this circuit.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public Site Site { get; set; }

        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<Device> Devices { get; set; }
    }
}