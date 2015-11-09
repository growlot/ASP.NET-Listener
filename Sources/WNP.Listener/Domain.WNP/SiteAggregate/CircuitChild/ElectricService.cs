//-----------------------------------------------------------------------
// <copyright file="ElectricService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Geographical location coordinates.
    /// </summary>
    public sealed class ElectricService : ValueObject<ElectricService>
    {
        private readonly string location;
        private readonly decimal? voltage;
        private readonly decimal? amperage;
        private readonly int? numberOfPhases;
        private readonly int? numberOfWires;
        private readonly ServiceWiring wiringInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricService" /> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="voltage">The voltage.</param>
        /// <param name="amperage">The amperage.</param>
        /// <param name="numberOfPhases">The number of phases.</param>
        /// <param name="numberOfWires">The number of wires.</param>
        /// <param name="wiringInfo">The wiring information.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Only values 1 for single-phase or 3 for three-phase are supported for number of phases.
        /// or
        /// Only values 2, 3 and 4 are supported for number of wires.
        /// </exception>
        public ElectricService(
            string location,
            decimal? voltage,
            decimal? amperage,
            int? numberOfPhases,
            int? numberOfWires,
            ServiceWiring wiringInfo)
        {
            if (numberOfPhases != null && (numberOfPhases.Value != 1 || numberOfPhases.Value != 3))
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfPhases), "Only values 1 for single-phase or 3 for three-phase are supported for number of phases.");
            }

            if (numberOfWires != null && (numberOfWires.Value < 2 || numberOfWires.Value > 4))
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfWires), "Only values 2, 3 and 4 are supported for number of wires.");
            }

            this.location = location;
            this.voltage = voltage;
            this.amperage = amperage;
            this.numberOfPhases = numberOfPhases;
            this.numberOfWires = numberOfWires;
            this.wiringInfo = wiringInfo;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Gets the voltage.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public decimal? Voltage
        {
            get
            {
                return this.voltage;
            }
        }

        /// <summary>
        /// Gets the amperage.
        /// </summary>
        /// <value>
        /// The amperage.
        /// </value>
        public decimal? Amperage
        {
            get
            {
                return this.amperage;
            }
        }

        /// <summary>
        /// Gets the number of phases.
        /// </summary>
        /// <value>
        /// The number of phases.
        /// </value>
        public int? NumberOfPhases
        {
            get
            {
                return this.numberOfPhases;
            }
        }

        /// <summary>
        /// Gets the number of wires.
        /// </summary>
        /// <value>
        /// The number of wires.
        /// </value>
        public int? NumberOfWires
        {
            get
            {
                return this.numberOfWires;
            }
        }

        /// <summary>
        /// Gets the wiring information.
        /// </summary>
        /// <value>
        /// The wiring information.
        /// </value>
        public ServiceWiring WiringInfo
        {
            get
            {
                return this.wiringInfo;
            }
        }
    }
}
