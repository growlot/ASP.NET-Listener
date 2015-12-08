// <copyright file="EquipmentStateMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Memento class for equipment state entity
    /// </summary>
    public class EquipmentStateMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentStateMemento" /> class.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="workflow">The workflow.</param>
        /// <param name="location">The location.</param>
        /// <param name="equipmentStatus">The equipment status.</param>
        /// <param name="detailedStatus">The detailed status.</param>
        /// <param name="shopCycle">The shop cycle.</param>
        /// <param name="boxNumber">The box number.</param>
        /// <param name="palletNumber">The pallet number.</param>
        /// <param name="shelfId">The shelf identifier.</param>
        /// <param name="issuedTo">The issued to.</param>
        /// <param name="vehicleNumber">The vehicle number.</param>
        public EquipmentStateMemento(
            string equipmentNumber,
            string equipmentType,
            string workflow,
            IMemento location,
            string equipmentStatus,
            string detailedStatus,
            int shopCycle,
            string boxNumber,
            string palletNumber,
            string shelfId,
            string issuedTo,
            string vehicleNumber)
        {
            this.EquipmentNumber = equipmentNumber;
            this.EquipmentType = equipmentType;
            this.Workflow = workflow;
            this.Location = location;
            this.EquipmentStatus = equipmentStatus;
            this.DetailedStatus = detailedStatus;
            this.ShopCycle = shopCycle;
            this.BoxNumber = boxNumber;
            this.PalletNumber = palletNumber;
            this.ShelfId = shelfId;
            this.IssuedTo = issuedTo;
            this.VehicleNumber = vehicleNumber;
        }

        internal string EquipmentNumber { get; private set; }

        internal string EquipmentType { get; private set; }

        internal string Workflow { get; private set; }

        internal IMemento Location { get; private set; }

        internal string EquipmentStatus { get; private set; }

        internal string DetailedStatus { get; private set; }

        internal int ShopCycle { get; private set; }

        internal string BoxNumber { get; private set; }

        internal string PalletNumber { get; private set; }

        internal string ShelfId { get; private set; }

        internal string IssuedTo { get; private set; }

        internal string VehicleNumber { get; private set; }
    }
}
