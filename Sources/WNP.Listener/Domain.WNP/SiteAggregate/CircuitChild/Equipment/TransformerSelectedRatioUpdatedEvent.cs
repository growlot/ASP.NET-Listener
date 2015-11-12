// <copyright file="TransformerSelectedRatioUpdatedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Event generated after transformer selected ratio changes.
    /// </summary>
    public class TransformerSelectedRatioUpdatedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransformerSelectedRatioUpdatedEvent" /> class.
        /// </summary>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="selectedRatio">The selected ratio.</param>
        public TransformerSelectedRatioUpdatedEvent(
            string equipmentType,
            string equipmentNumber,
            decimal? selectedRatio)
        {
            this.EquipmentType = equipmentType;
            this.EquipmentNumber = equipmentNumber;
            this.SelectedRatio = selectedRatio;
        }

        /// <summary>
        /// Gets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; private set; }

        /// <summary>
        /// Gets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the selected ratio for transformer.
        /// </summary>
        /// <value>
        /// The circiut identifier.
        /// </value>
        public decimal? SelectedRatio { get; private set; }
    }
}
