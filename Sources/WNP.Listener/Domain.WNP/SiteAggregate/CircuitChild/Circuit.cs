// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents single circuit installed in a site.
    /// </summary>
    public class Circuit : Entity<int>
    {
        private IList<IDomainEvent> events;
        private string description;
        private GeoLocation location;
        private ElectricService service;
        private int? numberOfConductorsPerPhase;
        private string enclosureType;
        private DateTime? installDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit" /> class.
        /// </summary>
        /// <param name="events">The events list used by parent agregate root.</param>
        public Circuit(IList<IDomainEvent> events)
        {
            this.events = events;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="numberOfConductorsPerPhase">The number of conductors per phase.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        private Circuit(
            int owner,
            int siteId,
            int id,
            string description,
            GeoLocation location,
            ElectricService service,
            int? numberOfConductorsPerPhase,
            string enclosureType,
            DateTime? installDate)
        {
            this.Id = id;
            this.description = description;
            this.location = location;
            this.service = service;
            this.numberOfConductorsPerPhase = numberOfConductorsPerPhase;
            this.enclosureType = enclosureType;
            this.installDate = installDate;

            this.events.Add(
                new CircuitCreatedEvent(
                    ownerId: owner,
                    siteId: siteId,
                    id: this.Id,
                    description: this.description,
                    longitude: this.location?.Longitude,
                    latitude: this.location?.Latitude,
                    serviceLocation: this.service?.Location,
                    serviceVoltage: this.service?.Voltage,
                    serviceAmperage: this.service?.Amperage,
                    servicePhases: this.service?.NumberOfPhases,
                    serviceWires: this.service?.NumberOfWires,
                    wireLocation: this.service?.WiringInfo?.WireLocation,
                    wireSize: this.service?.WiringInfo?.WireSize,
                    wireType: this.service?.WiringInfo?.WireType,
                    numberOfConductorsPerPhase: this.numberOfConductorsPerPhase,
                    enclosureType: this.enclosureType,
                    installDate: this.installDate));
        }

        /// <summary>
        /// Creates the circuit.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="numberOfConductorsPerPhase">The number of conductors per phase.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        /// <returns>
        /// The new circuit
        /// </returns>
        public static Circuit CreateCircuit(
            int owner,
            int siteId,
            int id,
            string description,
            GeoLocation location,
            ElectricService service,
            int? numberOfConductorsPerPhase,
            string enclosureType,
            DateTime? installDate)
        {
            return new Circuit(
                owner: owner,
                siteId: siteId,
                id: id,
                description: description,
                location: location,
                service: service,
                numberOfConductorsPerPhase: numberOfConductorsPerPhase,
                enclosureType: enclosureType,
                installDate: installDate);
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var circuitMemento = (CircuitMemento)memento;
            this.Id = circuitMemento.Id;
            this.description = circuitMemento.Description;
            this.numberOfConductorsPerPhase = circuitMemento.NumberOfConductorsPerPhase;
            this.enclosureType = circuitMemento.EnclosureType;
            this.installDate = circuitMemento.InstallDate;

            if (circuitMemento.Latitude.HasValue && circuitMemento.Longitude.HasValue)
            {
                this.location = new GeoLocation(circuitMemento.Longitude.Value, circuitMemento.Latitude.Value);
            }

            var wiringInfo = new ServiceWiring(
                circuitMemento.WireLocation,
                circuitMemento.WireSize,
                circuitMemento.WireType);

            this.service = new ElectricService(
                circuitMemento.ServiceLocation,
                circuitMemento.ServiceVoltage,
                circuitMemento.ServiceAmperage,
                circuitMemento.ServicePhases,
                circuitMemento.ServiceWires,
                wiringInfo);
        }
    }
}
