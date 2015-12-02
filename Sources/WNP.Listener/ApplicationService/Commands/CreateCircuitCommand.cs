// <copyright file="CreateCircuitCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Information needed for circuit creation.
    /// </summary>
    public class CreateCircuitCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the circuit description.
        /// </summary>
        /// <value>
        /// The circuit description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the service location.
        /// </summary>
        /// <value>
        /// The service location.
        /// </value>
        public string ServiceLocation { get; set; }

        /// <summary>
        /// Gets or sets the service voltage.
        /// </summary>
        /// <value>
        /// The service voltage.
        /// </value>
        public decimal? ServiceVoltage { get; set; }

        /// <summary>
        /// Gets or sets the service amperage.
        /// </summary>
        /// <value>
        /// The service amperage.
        /// </value>
        public decimal? ServiceAmperage { get; set; }

        /// <summary>
        /// Gets or sets the service phases.
        /// </summary>
        /// <value>
        /// The service phases.
        /// </value>
        public int? ServicePhases { get; set; }

        /// <summary>
        /// Gets or sets the service wires.
        /// </summary>
        /// <value>
        /// The service wires.
        /// </value>
        public int? ServiceWires { get; set; }

        /// <summary>
        /// Gets or sets the wire location.
        /// </summary>
        /// <value>
        /// The wire location.
        /// </value>
        public string WireLocation { get; set; }

        /// <summary>
        /// Gets or sets the size of the wire.
        /// </summary>
        /// <value>
        /// The size of the wire.
        /// </value>
        public string WireSize { get; set; }

        /// <summary>
        /// Gets or sets the type of the wire.
        /// </summary>
        /// <value>
        /// The type of the wire.
        /// </value>
        public string WireType { get; set; }

        /// <summary>
        /// Gets or sets the number of conductors per phase.
        /// </summary>
        /// <value>
        /// The number of conductors per phase.
        /// </value>
        public int? NumberOfConductorsPerPhase { get; set; }

        /// <summary>
        /// Gets or sets the type of the enclosure.
        /// </summary>
        /// <value>
        /// The type of the enclosure.
        /// </value>
        public string EnclosureType { get; set; }

        /// <summary>
        /// Gets or sets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        public DateTime? InstallDate { get; set; }

        /// <summary>
        /// Gets or sets the service point.
        /// </summary>
        /// <value>
        /// The service point.
        /// </value>
        public string ServicePoint { get; set; }

        /// <summary>
        /// Gets or sets the meter point.
        /// </summary>
        /// <value>
        /// The meter point.
        /// </value>
        public string MeterPoint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this circuit has bracket.
        /// </summary>
        /// <value>
        /// The has bracket.
        /// </value>
        public bool HasBracket { get; set; }
    }
}
