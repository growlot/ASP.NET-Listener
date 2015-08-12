//-----------------------------------------------------------------------
// <copyright file="Vehicle.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing vehicle.
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
        /// <param name="id">The identifier.</param>
        public Vehicle(int id)
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
        /// Gets or sets the vehicle number.
        /// </summary>
        /// <value>
        /// The vehicle number.
        /// </value>
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the vehicle description.
        /// </summary>
        /// <value>
        /// The vehicle description.
        /// </value>
        public string Description { get; set; }
    }
}
