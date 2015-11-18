// <copyright file="UpdateCircuitDetailsCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Update circuit details command
    /// </summary>
    public class UpdateCircuitDetailsCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the circuit identifier.
        /// </summary>
        /// <value>
        /// The circuit identifier.
        /// </value>
        public int CircuitId { get; set; }

        /// <summary>
        /// Gets or sets the circuit description.
        /// </summary>
        /// <value>
        /// The circuit description.
        /// </value>
        public string CircuitDescription { get; set; }

        /// <summary>
        /// Gets or sets the type of the enclosure.
        /// </summary>
        /// <value>
        /// The type of the enclosure.
        /// </value>
        public string EnclosureType { get; set; }

        /// <summary>
        /// Gets or sets the has bracket.
        /// </summary>
        /// <value>
        /// The has bracket.
        /// </value>
        public bool HasBracket { get; set; }

        /// <summary>
        /// Gets or sets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        public DateTime? InstallDate { get; set; }

        /// <summary>
        /// Gets or sets the meter point.
        /// </summary>
        /// <value>
        /// The meter point.
        /// </value>
        public string MeterPoint { get; set; }

        /// <summary>
        /// Gets or sets the service point.
        /// </summary>
        /// <value>
        /// The service point.
        /// </value>
        public string ServicePoint { get; set; }
    }
}