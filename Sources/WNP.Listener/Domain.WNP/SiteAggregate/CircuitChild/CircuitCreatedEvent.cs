// <copyright file="CircuitCreatedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Event generated after new circuit is created.
    /// </summary>
    public class CircuitCreatedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCreatedEvent" /> class.
        /// </summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="serviceLocation">The service location.</param>
        /// <param name="serviceVoltage">The service voltage.</param>
        /// <param name="serviceAmperage">The service amperage.</param>
        /// <param name="servicePhases">The service phases.</param>
        /// <param name="serviceWires">The service wires.</param>
        /// <param name="wireLocation">The wire location.</param>
        /// <param name="wireSize">Size of the wire.</param>
        /// <param name="wireType">The wire type.</param>
        /// <param name="numberOfConductorsPerPhase">The number of conductors per phase.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        public CircuitCreatedEvent(
            int ownerId,
            int siteId,
            int id,
            string description,
            decimal? longitude,
            decimal? latitude,
            string serviceLocation,
            decimal? serviceVoltage,
            decimal? serviceAmperage,
            int? servicePhases,
            int? serviceWires,
            string wireLocation,
            string wireSize,
            string wireType,
            int? numberOfConductorsPerPhase,
            string enclosureType,
            DateTime? installDate)
        {
            this.OwnerId = ownerId;
            this.SiteId = siteId;
            this.Id = id;
            this.Description = description;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.ServiceLocation = serviceLocation;
            this.ServiceVoltage = serviceVoltage;
            this.ServiceAmperage = serviceAmperage;
            this.ServicePhases = servicePhases;
            this.ServiceWires = serviceWires;
            this.WireLocation = wireLocation;
            this.WireSize = wireSize;
            this.WireType = wireType;
            this.NumberOfConductorsPerPhase = numberOfConductorsPerPhase;
            this.EnclosureType = enclosureType;
            this.InstallDate = installDate;
        }

        /// <summary>
        /// Gets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public int OwnerId { get; private set; }

        /// <summary>
        /// Gets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int SiteId { get; private set; }

        /// <summary>
        /// Gets the circuit identifier.
        /// </summary>
        /// <value>
        /// The circiut identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the circuit description.
        /// </summary>
        /// <value>
        /// The circuit description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal? Longitude { get; private set; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal? Latitude { get; private set; }

        /// <summary>
        /// Gets the service location.
        /// </summary>
        /// <value>
        /// The service location.
        /// </value>
        public string ServiceLocation { get; private set; }

        /// <summary>
        /// Gets the service voltage.
        /// </summary>
        /// <value>
        /// The service voltage.
        /// </value>
        public decimal? ServiceVoltage { get; private set; }

        /// <summary>
        /// Gets the service amperage.
        /// </summary>
        /// <value>
        /// The service amperage.
        /// </value>
        public decimal? ServiceAmperage { get; private set; }

        /// <summary>
        /// Gets the service phases.
        /// </summary>
        /// <value>
        /// The service phases.
        /// </value>
        public int? ServicePhases { get; private set; }

        /// <summary>
        /// Gets the service wires.
        /// </summary>
        /// <value>
        /// The service wires.
        /// </value>
        public int? ServiceWires { get; private set; }

        /// <summary>
        /// Gets the wire location.
        /// </summary>
        /// <value>
        /// The wire location.
        /// </value>
        public string WireLocation { get; private set; }

        /// <summary>
        /// Gets the size of the wire.
        /// </summary>
        /// <value>
        /// The size of the wire.
        /// </value>
        public string WireSize { get; private set; }

        /// <summary>
        /// Gets the type of the wire.
        /// </summary>
        /// <value>
        /// The type of the wire.
        /// </value>
        public string WireType { get; private set; }

        /// <summary>
        /// Gets the number of conductors per phase.
        /// </summary>
        /// <value>
        /// The number of conductors per phase.
        /// </value>
        public int? NumberOfConductorsPerPhase { get; private set; }

        /// <summary>
        /// Gets the type of the enclosure.
        /// </summary>
        /// <value>
        /// The type of the enclosure.
        /// </value>
        public string EnclosureType { get; private set; }

        /// <summary>
        /// Gets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        public DateTime? InstallDate { get; private set; }
    }
}
