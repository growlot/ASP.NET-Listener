// <copyright file="EquipmentUninstalledFromCircuitEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild
{
    using System;

    /// <summary>
    /// Event generated after any type of equipment is uninstalled from the circuit.
    /// </summary>
    public class EquipmentUninstalledFromCircuitEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentUninstalledFromCircuitEvent" /> class.
        /// </summary>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="uninstallDate">The uninstall date.</param>
        /// <param name="uninstallUser">The uninstall user.</param>
        /// <param name="uninstallReason">The uninstall reason.</param>
        /// <param name="orderIssued">The order issued.</param>
        /// <param name="orderCompleted">The order completed.</param>
        public EquipmentUninstalledFromCircuitEvent(
            string equipmentType,
            string equipmentNumber,
            DateTime uninstallDate,
            string uninstallUser,
            string uninstallReason,
            DateTime? orderIssued,
            DateTime? orderCompleted)
        {
            this.EquipmentType = equipmentType;
            this.EquipmentNumber = equipmentNumber;
            this.UninstallDate = uninstallDate;
            this.UninstallUser = uninstallUser;
            this.UninstallReason = uninstallReason;
            this.UninstallOrderIssued = orderIssued;
            this.UninstallOrderCompleted = orderCompleted;
        }

        /// <summary>
        /// Gets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; private set; }

        /// <summary>
        /// Gets the meter equipment number.
        /// </summary>
        /// <value>
        /// The meter equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the uninstall date.
        /// </summary>
        /// <value>
        /// The uninstall date.
        /// </value>
        public DateTime UninstallDate { get; private set; }

        /// <summary>
        /// Gets the uninstall user.
        /// </summary>
        /// <value>
        /// The uninstall user.
        /// </value>
        public string UninstallUser { get; private set; }

        /// <summary>
        /// Gets the uninstall reason.
        /// </summary>
        /// <value>
        /// The uninstall reason.
        /// </value>
        public string UninstallReason { get; private set; }

        /// <summary>
        /// Gets the uninstall order issued.
        /// </summary>
        /// <value>
        /// The uninstall order issued.
        /// </value>
        public DateTime? UninstallOrderIssued { get; private set; }

        /// <summary>
        /// Gets the uninstall order completed.
        /// </summary>
        /// <value>
        /// The uninstall order completed.
        /// </value>
        public DateTime? UninstallOrderCompleted { get; private set; }
    }
}
