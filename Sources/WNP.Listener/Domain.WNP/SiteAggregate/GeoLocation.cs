//-----------------------------------------------------------------------
// <copyright file="GeoLocation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Geographical location coordinates.
    /// </summary>
    public sealed class GeoLocation : ValueObject<GeoLocation>
    {
        /// <summary>
        /// The longitude
        /// </summary>
        private readonly decimal longitude;

        /// <summary>
        /// The latitude
        /// </summary>
        private readonly decimal latitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation" /> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public GeoLocation(decimal longitude, decimal latitude)
        {
            if (longitude > 180 || longitude < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");
            }

            if (latitude > 90 || latitude < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");
            }

            this.longitude = longitude;
            this.latitude = latitude;
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal Longitude
        {
            get
            {
                return this.longitude;
            }
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal Latitude
        {
            get
            {
                return this.latitude;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string result = string.Format(CultureInfo.InvariantCulture, "Location (longitude = {0}, latitude = {1})", this.longitude, this.latitude);
            return result;
        }
    }
}
