// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;
    using System.Collections.Generic;
    using Equipment;

    /// <summary>
    /// Represents single circuit installed in a site.
    /// </summary>
    public class Circuit : Entity<int>
    {
        private IList<IDomainEvent> events;
        private string description;
        private GeoLocation location;
        private ElectricService service;
        private string enclosureType;
        private DateTime? installDate;
        private decimal circuitMultiplier;
        private IList<CircuitMeter> meters = new List<CircuitMeter>();
        private IList<CircuitCurrentTransformer> currentTransformers = new List<CircuitCurrentTransformer>();
        private IList<CircuitPotentialTransformer> potentialTransformers = new List<CircuitPotentialTransformer>();

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
        /// <param name="events">The events.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        private Circuit(
            IList<IDomainEvent> events,
            int siteId,
            int id,
            string description,
            GeoLocation location,
            ElectricService service,
            string enclosureType,
            DateTime? installDate)
        {
            this.events = events;
            this.Id = id;
            this.description = description;
            this.location = location;
            this.service = service;
            this.enclosureType = enclosureType;
            this.installDate = installDate;

            this.events.Add(
                new CircuitCreatedEvent(
                    siteId: siteId,
                    circuitId: this.Id,
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
                    numberOfConductorsPerPhase: this.service?.WiringInfo?.NumberOfConductorsPerPhase,
                    enclosureType: this.enclosureType,
                    installDate: this.installDate));
        }

        /// <summary>
        /// Creates the circuit.
        /// </summary>
        /// <param name="events">The list of domain events.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        /// <returns>
        /// The new circuit
        /// </returns>
        public static Circuit CreateCircuit(
            IList<IDomainEvent> events,
            int siteId,
            int id,
            string description,
            GeoLocation location,
            ElectricService service,
            string enclosureType,
            DateTime? installDate)
        {
            return new Circuit(
                events: events,
                siteId: siteId,
                id: id,
                description: description,
                location: location,
                service: service,
                enclosureType: enclosureType,
                installDate: installDate);
        }

        /// <summary>
        /// Checks if specified description is same as current description.
        /// </summary>
        /// <param name="newDescription">The description.</param>
        /// <returns>True if description match, false otherwise</returns>
        public bool SameDescription(string newDescription)
        {
            return this.description.Equals(newDescription, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Installs the meter in circuit.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="meterInstallDate">The install date.</param>
        /// <param name="installUser">The install user.</param>
        /// <param name="installServiceOrder">The service order information.</param>
        public void InstallMeter(
            int siteId,
            CircuitMeter meter,
            DateTime meterInstallDate,
            string installUser,
            ServiceOrder installServiceOrder)
        {
            if (installServiceOrder == null)
            {
                throw new ArgumentNullException(nameof(installServiceOrder), "Service order must be specified (even if all it's fields will be null)");
            }

            if (meter == null)
            {
                throw new ArgumentNullException(nameof(meter), "Can not install meter in circuit, because it is not specified.");
            }

            this.meters.Add(meter);
            meter.ActivateEvents(this.events);
            var meterInstalledEvent = new EquipmentInstalledInCircuitEvent(
                siteId,
                this.Id,
                meter.Id,
                meterInstallDate,
                installUser,
                installServiceOrder.OrderIssued,
                installServiceOrder.OrderCompleted);

            this.events.Add(meterInstalledEvent);
            meter.UpdateBillingInformation(this.circuitMultiplier);
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var circuitMemento = (CircuitMemento)memento;
            this.Id = circuitMemento.Id;
            this.description = circuitMemento.Description;
            this.enclosureType = circuitMemento.EnclosureType;
            this.installDate = circuitMemento.InstallDate;

            if (circuitMemento.Latitude.HasValue && circuitMemento.Longitude.HasValue)
            {
                this.location = new GeoLocation(circuitMemento.Longitude.Value, circuitMemento.Latitude.Value);
            }

            var wiringInfo = new ServiceWiring(
                circuitMemento.WireLocation,
                circuitMemento.WireSize,
                circuitMemento.WireType,
                circuitMemento.NumberOfConductorsPerPhase);

            this.service = new ElectricService(
                circuitMemento.ServiceLocation,
                circuitMemento.ServiceVoltage,
                circuitMemento.ServiceAmperage,
                circuitMemento.ServicePhases,
                circuitMemento.ServiceWires,
                wiringInfo);

            foreach (var meterMemento in circuitMemento.Meters)
            {
                var circuitMeter = new CircuitMeter(this.events);
                ((IOriginator)circuitMeter).SetMemento(meterMemento);
                this.meters.Add(circuitMeter);
            }

            foreach (var currentTransformerMemento in circuitMemento.CurrentTransformers)
            {
                var circuitCurrentTransformer = new CircuitCurrentTransformer(this.events);
                ((IOriginator)circuitCurrentTransformer).SetMemento(currentTransformerMemento);
                this.currentTransformers.Add(circuitCurrentTransformer);
            }

            foreach (var potentialTransformerMemento in circuitMemento.PotentialTransformers)
            {
                var circuitPotentialTransformer = new CircuitPotentialTransformer(this.events);
                ((IOriginator)circuitPotentialTransformer).SetMemento(potentialTransformerMemento);
                this.potentialTransformers.Add(circuitPotentialTransformer);
            }

            this.SetCalculatedValues();
        }

        /// <summary>
        /// Sets the calculated values after .
        /// </summary>
        private void SetCalculatedValues()
        {
            this.circuitMultiplier = 1;
            if (this.currentTransformers.Count > 0)
            {
                this.circuitMultiplier *= this.currentTransformers[0].GetMultiplier();
            }

            if (this.potentialTransformers.Count > 0)
            {
                this.circuitMultiplier *= this.potentialTransformers[0].GetMultiplier();
            }
        }
    }
}
