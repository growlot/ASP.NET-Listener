// <copyright file="EquipmentStateChangedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Event generated when business action is performed on equipment.
    /// </summary>
    public class EquipmentStateChangedEvent : IDomainEvent
    {
        internal EquipmentStateChangedEvent(
            string equipmentNumber,
            string equipmentType,
            string workstation,
            string workflow,
            string location,
            string equipmentStatus,
            string detailedStatus,
            int shopCycle,
            string boxNumber,
            string palletNumber,
            string shelfId,
            string issuedTo,
            string vehicle)
        {
            this.EquipmentNumber = equipmentNumber;
            this.EquipmentType = equipmentType;
            this.Workstation = workstation;
            this.Workflow = workflow;
            this.Location = location;
            this.EquipmentStatus = equipmentStatus;
            this.DetailedStatus = detailedStatus;
            this.ShopCycle = shopCycle;
            this.BoxNumber = boxNumber;
            this.PalletNumber = palletNumber;
            this.ShelfId = shelfId;
            this.IssuedTo = issuedTo;
            this.Vehicle = vehicle;
        }

        /// <summary>
        /// Gets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; private set; }

        /// <summary>
        /// Gets the workstation.
        /// </summary>
        /// <value>
        /// The workstation.
        /// </value>
        public string Workstation { get; private set; }

        /// <summary>
        /// Gets the workflow.
        /// </summary>
        /// <value>
        /// The workflow.
        /// </value>
        public string Workflow { get; private set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; private set; }

        /// <summary>
        /// Gets the equipment status.
        /// </summary>
        /// <value>
        /// The equipment status.
        /// </value>
        public string EquipmentStatus { get; private set; }

        /// <summary>
        /// Gets the detailed status.
        /// </summary>
        /// <value>
        /// The detailed status.
        /// </value>
        public string DetailedStatus { get; private set; }

        /// <summary>
        /// Gets the shop cycle.
        /// </summary>
        /// <value>
        /// The shop cycle.
        /// </value>
        public int ShopCycle { get; private set; }

        /// <summary>
        /// Gets the box number.
        /// </summary>
        /// <value>
        /// The box number.
        /// </value>
        public string BoxNumber { get; private set; }

        /// <summary>
        /// Gets the pallet number.
        /// </summary>
        /// <value>
        /// The pallet number.
        /// </value>
        public string PalletNumber { get; private set; }

        /// <summary>
        /// Gets the shelf identifier.
        /// </summary>
        /// <value>
        /// The shelf identifier.
        /// </value>
        public string ShelfId { get; private set; }

        /// <summary>
        /// Gets the issued to.
        /// </summary>
        /// <value>
        /// The issued to.
        /// </value>
        public string IssuedTo { get; private set; }

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        /// <value>
        /// The vehicle.
        /// </value>
        public string Vehicle { get; private set; }
    }
}
