// <copyright file="CircuitDetailsUpdatedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Event generated after new circuit is created.
    /// </summary>
    public class CircuitDetailsUpdatedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitDetailsUpdatedEvent" /> class.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="meterPoint">The meter point.</param>
        /// <param name="servicePoint">The service point.</param>
        /// <param name="hasBracket">Indicator to determine if circuit has a bracket.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        public CircuitDetailsUpdatedEvent(
            int siteId,
            int circuitId,
            string description,
            string meterPoint,
            string servicePoint,
            bool hasBracket,
            string enclosureType,
            DateTime? installDate)
        {
            this.SiteId = siteId;
            this.CircuitId = circuitId;
            this.Description = description;
            this.MeterPoint = meterPoint;
            this.ServicePoint = servicePoint;
            this.HasBracket = hasBracket;
            this.EnclosureType = enclosureType;
            this.InstallDate = installDate;
        }

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
        public int CircuitId { get; private set; }

        /// <summary>
        /// Gets the circuit description.
        /// </summary>
        /// <value>
        /// The circuit description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the meter point.
        /// </summary>
        /// <value>
        /// The meter point.
        /// </value>
        public string MeterPoint { get; private set; }

        /// <summary>
        /// Gets the service point.
        /// </summary>
        /// <value>
        /// The service point.
        /// </value>
        public string ServicePoint { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this circuit has bracket.
        /// </summary>
        /// <value>
        /// <c>true</c> if this circuit has bracket; otherwise, <c>false</c>.
        /// </value>
        public bool HasBracket { get; private set; }

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
