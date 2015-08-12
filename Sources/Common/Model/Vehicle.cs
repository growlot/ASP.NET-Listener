//-----------------------------------------------------------------------
// <copyright file="Vehicle.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing Vehicle
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        public Vehicle(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        /// <value>
        /// The vehicle identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets company who owns the vehicle.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the vehicle number.
        /// </summary>
        /// <value>
        /// The vehicle number.
        /// </value>
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
