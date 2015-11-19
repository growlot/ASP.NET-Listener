// <copyright file="Circuit.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Equipment;

    /// <summary>
    /// Represents single circuit installed in a site.
    /// </summary>
    public class Circuit : Entity<int>
    {
        private IList<IDomainEvent> events;
        private string description;
        private string meterPoint;
        private string servicePoint;
        private bool hasBracket;
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
        /// <param name="meterPoint">The meter point.</param>
        /// <param name="servicePoint">The service point.</param>
        /// <param name="hasBracket">Indicator to determine if circuit has a bracket.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        private Circuit(
            IList<IDomainEvent> events,
            int siteId,
            int id,
            string description,
            string meterPoint,
            string servicePoint,
            bool hasBracket,
            GeoLocation location,
            ElectricService service,
            string enclosureType,
            DateTime? installDate)
        {
            this.events = events;
            this.Id = id;
            this.description = description;
            this.meterPoint = meterPoint;
            this.servicePoint = servicePoint;
            this.hasBracket = hasBracket;
            this.location = location;
            this.service = service;
            this.enclosureType = enclosureType;
            this.installDate = installDate;

            this.events.Add(
                new CircuitCreatedEvent(
                    siteId: siteId,
                    circuitId: this.Id,
                    description: this.description,
                    meterPoint: this.meterPoint,
                    servicePoint: this.servicePoint,
                    hasBracket: this.hasBracket,
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
        /// <param name="meterPoint">The meter point.</param>
        /// <param name="servicePoint">The service point.</param>
        /// <param name="hasBracket">Indicator to determine if circuit has a bracket.</param>
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
            string meterPoint,
            string servicePoint,
            bool hasBracket,
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
                meterPoint: meterPoint,
                servicePoint: servicePoint,
                hasBracket: hasBracket,
                location: location,
                service: service,
                enclosureType: enclosureType,
                installDate: installDate);
        }

        /// <summary>
        /// Updates the circuit details.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="newDescription">The description.</param>
        /// <param name="newEnclosureType">Type of the enclosure.</param>
        /// <param name="newHasBracket">The has bracket.</param>
        /// <param name="newInstallDate">The install date.</param>
        /// <param name="newMeterPoint">The meter point.</param>
        /// <param name="newServicePoint">The service point.</param>
        internal void UpdateDetails(int siteId, string newDescription, string newEnclosureType, bool newHasBracket, DateTime? newInstallDate, string newMeterPoint, string newServicePoint)
        {
            this.description = newDescription;
            this.enclosureType = newEnclosureType;
            this.hasBracket = newHasBracket;
            this.installDate = newInstallDate;
            this.meterPoint = newMeterPoint;
            this.servicePoint = newServicePoint;

            this.events.Add(
                new CircuitDetailsUpdatedEvent(
                    siteId: siteId,
                    circuitId: this.Id,
                    description: this.description,
                    hasBracket: this.hasBracket,
                    meterPoint: this.meterPoint,
                    servicePoint: this.servicePoint,
                    enclosureType: this.enclosureType,
                    installDate: this.installDate));
        }

        /// <summary>
        /// Checks if specified values are not colliding with current circuit values.
        /// </summary>
        /// <param name="newDescription">The description.</param>
        /// <param name="newMeterPoint">The meter point.</param>
        internal void EnsureUniqueness(string newDescription, string newMeterPoint)
        {
            if (this.description.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Circuit with same description already exists in this site.");
            }

            if (this.meterPoint != null && this.meterPoint.Equals(newMeterPoint, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Circuit with same meter point already exists in this site.");
            }
        }

        /// <summary>
        /// Installs the meter in circuit.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="meterInstallDate">The install date.</param>
        /// <param name="installUser">The install user.</param>
        /// <param name="installServiceOrder">The service order information.</param>
        internal void InstallMeter(
            int siteId,
            CircuitMeter meter,
            DateTime meterInstallDate,
            string installUser,
            ServiceOrder installServiceOrder)
        {
            if (meter == null)
            {
                throw new ArgumentNullException(nameof(meter), "Can not install meter in circuit, because it is not specified.");
            }

            if (installServiceOrder == null)
            {
                throw new ArgumentNullException(nameof(installServiceOrder), "Service order must be specified (even if all it's fields will be null)");
            }

            if (this.meters.Contains(meter))
            {
                return;
            }

            string reason;
            if (!meter.CanBeInstalled(out reason))
            {
                throw new InvalidOperationException(reason);
            }

            this.meters.Add(meter);
            meter.ActivateEvents(this.events);
            var meterInstalledEvent = new EquipmentInstalledInCircuitEvent(
                siteId,
                this.Id,
                EquipmentType.ElectricMeter.Code,
                meter.Id,
                meterInstallDate,
                installUser,
                installServiceOrder.OrderIssued,
                installServiceOrder.OrderCompleted);

            this.events.Add(meterInstalledEvent);
            meter.UpdateBillingInformation(this.circuitMultiplier);
        }

        /// <summary>
        /// Uninstalls the meter.
        /// </summary>
        /// <param name="meterId">The meter identifier.</param>
        /// <param name="uninstallDate">The uninstall date.</param>
        /// <param name="uninstallReason">The uninstall reason.</param>
        /// <param name="uninstallUser">The uninstall user.</param>
        /// <param name="uninstallServiceOrder">The uninstall service order.</param>
        internal void UninstallMeter(
            string meterId,
            DateTime uninstallDate,
            string uninstallReason,
            string uninstallUser,
            ServiceOrder uninstallServiceOrder)
        {
            var meter = this.meters.FirstOrDefault(item => item.Id == meterId);
            if (meter == null)
            {
                throw new InvalidOperationException("Can not uninstall the meter, because meter with equipment number {0} is not found in circuit {1} (ID: {2}).".FormatWith(meterId, this.description, this.Id));
            }

            this.meters.Remove(meter);
            var meterRemovedEvent = new EquipmentUninstalledFromCircuitEvent(
                equipmentType: EquipmentType.ElectricMeter.Code,
                equipmentNumber: meter.Id,
                uninstallDate: uninstallDate,
                uninstallUser: uninstallUser,
                uninstallReason: uninstallReason,
                orderIssued: uninstallServiceOrder.OrderIssued,
                orderCompleted: uninstallServiceOrder.OrderCompleted);

            this.events.Add(meterRemovedEvent);
            meter.RemoveBillingInformation();
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var circuitMemento = (CircuitMemento)memento;
            this.Id = circuitMemento.Id;
            this.description = circuitMemento.Description;
            this.enclosureType = circuitMemento.EnclosureType;
            this.installDate = circuitMemento.InstallDate;
            this.meterPoint = circuitMemento.MeterPoint;
            this.servicePoint = circuitMemento.ServicePoint;
            this.hasBracket = circuitMemento.HasBracket;

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
