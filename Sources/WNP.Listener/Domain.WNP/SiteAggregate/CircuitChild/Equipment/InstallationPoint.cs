//-----------------------------------------------------------------------
// <copyright file="InstallationPoint.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    using System;

    /// <summary>
    /// Installation place of equipment.
    /// </summary>
    public sealed class InstallationPoint : ValueObject<InstallationPoint>
    {
        private readonly int? siteId;
        private readonly int? circuitId;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallationPoint" /> class.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <exception cref="System.ArgumentException">
        /// Circuit must be specified if site set.
        /// or
        /// Circuit can not be specified if site is not set.
        /// </exception>
        public InstallationPoint(int? siteId, int? circuitId)
        {
            if (siteId.HasValue && !circuitId.HasValue)
            {
                throw new ArgumentException("Circuit must be specified if site set.", nameof(circuitId));
            }

            if (!siteId.HasValue && circuitId.HasValue)
            {
                throw new ArgumentException("Circuit can not be specified if site is not set.", nameof(circuitId));
            }

            this.siteId = siteId;
            this.circuitId = circuitId;
        }

        /// <summary>
        /// Gets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int? SiteId
        {
            get
            {
                return this.siteId;
            }
        }

        /// <summary>
        /// Gets the circuit identifier.
        /// </summary>
        /// <value>
        /// The circuit identifier.
        /// </value>
        public int? CircuitId
        {
            get
            {
                return this.circuitId;
            }
        }
    }
}
