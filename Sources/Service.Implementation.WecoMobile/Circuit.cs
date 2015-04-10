//-----------------------------------------------------------------------
// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System;

    /// <summary>
    /// Data model class representing Circuit entity
    /// </summary>
    public class Circuit
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The Circuit description.
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
    }
}
