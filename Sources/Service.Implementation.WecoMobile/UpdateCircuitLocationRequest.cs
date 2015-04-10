//-----------------------------------------------------------------------
// <copyright file="UpdateCircuitLocationRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    /// <summary>
    /// Circuit location update request message for web service
    /// </summary>
    public class UpdateCircuitLocationRequest
    {
        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the circuit.
        /// </summary>
        /// <value>
        /// The circuit.
        /// </value>
        public int CircuitIndex { get; set; }

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
