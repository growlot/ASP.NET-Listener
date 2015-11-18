//-----------------------------------------------------------------------
// <copyright file="ServiceWiring.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Defines wiring information in electric service circuit.
    /// </summary>
    public sealed class ServiceWiring : ValueObject<ServiceWiring>
    {
        private readonly string wireLocation;
        private readonly string wireSize;
        private readonly string wireType;
        private readonly int? numberOfConductorsPerPhase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceWiring" /> class.
        /// </summary>
        /// <param name="wireLocation">The location of wires.</param>
        /// <param name="wireSize">Size of the wire.</param>
        /// <param name="wireType">Type of the wire.</param>
        /// <param name="numberOfConductorsPerPhase">The number of conductors per phase.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// There can be only 1, 2 or 3 conductors per phase.
        /// or
        /// Wire location can only be Underground (U) or Overhead (O).
        /// or
        /// Wire type can only be Copper (C) or Aluminium (A).
        /// </exception>
        public ServiceWiring(
            string wireLocation,
            string wireSize,
            string wireType,
            int? numberOfConductorsPerPhase)
        {
            if (numberOfConductorsPerPhase.HasValue && (numberOfConductorsPerPhase > 3 || numberOfConductorsPerPhase < 1))
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfConductorsPerPhase), "There can be only 1, 2 or 3 conductors per phase.");
            }

            if (!string.IsNullOrWhiteSpace(wireLocation) && wireLocation != "U" && wireLocation != "O")
            {
                throw new ArgumentOutOfRangeException(nameof(wireLocation), "Wire location can only be Underground (U) or Overhead (O).");
            }

            if (!string.IsNullOrWhiteSpace(wireType) && wireType != "C" && wireType != "A")
            {
                throw new ArgumentOutOfRangeException(nameof(wireType), "Wire type can only be Copper (C) or Aluminium (A).");
            }

            this.numberOfConductorsPerPhase = numberOfConductorsPerPhase;
            this.wireLocation = wireLocation;
            this.wireSize = wireSize;
            this.wireType = wireType;
        }

        /// <summary>
        /// Gets the wire location.
        /// </summary>
        /// <value>
        /// The wire location.
        /// </value>
        public string WireLocation
        {
            get
            {
                return this.wireLocation;
            }
        }

        /// <summary>
        /// Gets the size of the wire.
        /// </summary>
        /// <value>
        /// The size of the wire.
        /// </value>
        public string WireSize
        {
            get
            {
                return this.wireSize;
            }
        }

        /// <summary>
        /// Gets the type of the wire.
        /// </summary>
        /// <value>
        /// The type of the wire.
        /// </value>
        public string WireType
        {
            get
            {
                return this.wireType;
            }
        }

        /// <summary>
        /// Gets the number of conductors per phase.
        /// </summary>
        /// <value>
        /// The number of conductors per phase.
        /// </value>
        public int? NumberOfConductorsPerPhase
        {
            get
            {
                return this.numberOfConductorsPerPhase;
            }
        }
    }
}
