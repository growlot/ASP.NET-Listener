//-----------------------------------------------------------------------
// <copyright file="ServiceWiring.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
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
        public ServiceWiring(
            string wireLocation,
            string wireSize,
            string wireType,
            int? numberOfConductorsPerPhase)
        {
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
