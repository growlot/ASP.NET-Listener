// <copyright file="CircuitMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Memento class for circuit entity
    /// </summary>
    public class CircuitMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitMemento" /> class.
        /// </summary>
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
        /// <param name="meters">The meters.</param>
        /// <param name="currentTransformers">The current transformers.</param>
        /// <param name="potentialTransformers">The potential transformers.</param>
        public CircuitMemento(
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
            DateTime? installDate,
            IEnumerable<IMemento> meters,
            IEnumerable<IMemento> currentTransformers,
            IEnumerable<IMemento> potentialTransformers)
        {
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
            this.Meters = meters;
            this.CurrentTransformers = currentTransformers;
            this.PotentialTransformers = potentialTransformers;
        }

        /// <summary>
        /// Gets the meters.
        /// </summary>
        /// <value>
        /// The meters.
        /// </value>
        internal IEnumerable<IMemento> Meters { get; private set; }

        /// <summary>
        /// Gets the current transformers.
        /// </summary>
        /// <value>
        /// The current transformers.
        /// </value>
        internal IEnumerable<IMemento> CurrentTransformers { get; private set; }

        /// <summary>
        /// Gets the potential transformers.
        /// </summary>
        /// <value>
        /// The potential transformers.
        /// </value>
        internal IEnumerable<IMemento> PotentialTransformers { get; private set; }

        /// <summary>
        /// Gets the circuit identifier.
        /// </summary>
        /// <value>
        /// The circiut identifier.
        /// </value>
        internal int Id { get; private set; }

        /// <summary>
        /// Gets the circuit description.
        /// </summary>
        /// <value>
        /// The circuit description.
        /// </value>
        internal string Description { get; private set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        internal decimal? Longitude { get; private set; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        internal decimal? Latitude { get; private set; }

        /// <summary>
        /// Gets the service location.
        /// </summary>
        /// <value>
        /// The service location.
        /// </value>
        internal string ServiceLocation { get; private set; }

        /// <summary>
        /// Gets the service voltage.
        /// </summary>
        /// <value>
        /// The service voltage.
        /// </value>
        internal decimal? ServiceVoltage { get; private set; }

        /// <summary>
        /// Gets the service amperage.
        /// </summary>
        /// <value>
        /// The service amperage.
        /// </value>
        internal decimal? ServiceAmperage { get; private set; }

        /// <summary>
        /// Gets the service phases.
        /// </summary>
        /// <value>
        /// The service phases.
        /// </value>
        internal int? ServicePhases { get; private set; }

        /// <summary>
        /// Gets the service wires.
        /// </summary>
        /// <value>
        /// The service wires.
        /// </value>
        internal int? ServiceWires { get; private set; }

        /// <summary>
        /// Gets the wire location.
        /// </summary>
        /// <value>
        /// The wire location.
        /// </value>
        internal string WireLocation { get; private set; }

        /// <summary>
        /// Gets the size of the wire.
        /// </summary>
        /// <value>
        /// The size of the wire.
        /// </value>
        internal string WireSize { get; private set; }

        /// <summary>
        /// Gets the type of the wire.
        /// </summary>
        /// <value>
        /// The type of the wire.
        /// </value>
        internal string WireType { get; private set; }

        /// <summary>
        /// Gets the number of conductors per phase.
        /// </summary>
        /// <value>
        /// The number of conductors per phase.
        /// </value>
        internal int? NumberOfConductorsPerPhase { get; private set; }

        /// <summary>
        /// Gets the type of the enclosure.
        /// </summary>
        /// <value>
        /// The type of the enclosure.
        /// </value>
        internal string EnclosureType { get; private set; }

        /// <summary>
        /// Gets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        internal DateTime? InstallDate { get; private set; }
    }
}
