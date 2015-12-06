//-----------------------------------------------------------------------
// <copyright file="Location.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing location.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationName"/> class.
        /// </summary>
        public Location()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationName"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Location(int id)
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
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public string Area { get; set; }
    }
}
