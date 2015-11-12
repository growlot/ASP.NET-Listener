// <copyright file="EquipmentInstalledInCircuitEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Event generated after any type of equipment is installed in the circuit.
    /// </summary>
    public class EquipmentInstalledInCircuitEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentInstalledInCircuitEvent" /> class.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="installDate">The install date.</param>
        /// <param name="installUser">The install user.</param>
        /// <param name="orderIssued">The order issued.</param>
        /// <param name="orderCompleted">The order completed.</param>
        public EquipmentInstalledInCircuitEvent(
            int siteId,
            int circuitId,
            string equipmentNumber,
            DateTime installDate,
            string installUser,
            DateTime? orderIssued,
            DateTime? orderCompleted)
        {
            this.SiteId = siteId;
            this.CircuitId = circuitId;
            this.EquipmentNumber = equipmentNumber;
            this.InstallDate = installDate;
            this.InstallUser = installUser;
            this.InstallOrderIssued = orderIssued;
            this.InstallOrderCompleted = orderCompleted;
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
        /// Gets the meter equipment number.
        /// </summary>
        /// <value>
        /// The meter equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        public DateTime InstallDate { get; private set; }

        /// <summary>
        /// Gets the install user.
        /// </summary>
        /// <value>
        /// The install user.
        /// </value>
        public string InstallUser { get; private set; }

        /// <summary>
        /// Gets the install order issued.
        /// </summary>
        /// <value>
        /// The install order issued.
        /// </value>
        public DateTime? InstallOrderIssued { get; private set; }

        /// <summary>
        /// Gets the install order completed.
        /// </summary>
        /// <value>
        /// The install order completed.
        /// </value>
        public DateTime? InstallOrderCompleted { get; private set; }
    }
}
