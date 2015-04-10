//-----------------------------------------------------------------------
// <copyright file="Address.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System;

    /// <summary>
    /// Data model class representing Address entity
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address (first line).
        /// </summary>
        /// <value>
        /// The address (first line).
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address (second line).
        /// </summary>
        /// <value>
        /// The address (second line).
        /// </value>
        public string Address2 { get; set; }
    }
}