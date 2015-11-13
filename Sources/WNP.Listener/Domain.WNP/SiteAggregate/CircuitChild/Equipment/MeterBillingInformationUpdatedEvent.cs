// <copyright file="MeterBillingInformationUpdatedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Event generated after billing multiplier changes for the meter.
    /// </summary>
    public class MeterBillingInformationUpdatedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterBillingInformationUpdatedEvent" /> class.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="billingMultiplier">The billing multiplier.</param>
        /// <param name="billingKh">The billing kh.</param>
        public MeterBillingInformationUpdatedEvent(
            string equipmentNumber,
            decimal? billingMultiplier,
            decimal? billingKh)
        {
            this.EquipmentNumber = equipmentNumber;
            this.BillingMultiplier = billingMultiplier;
            this.BillingKh = billingKh;
        }

        /// <summary>
        /// Gets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the billing multiplier.
        /// </summary>
        /// <value>
        /// The billing multiplier.
        /// </value>
        public decimal? BillingMultiplier { get; private set; }

        /// <summary>
        /// Gets the number of Watt-Hours consumed by client per revolution on meter.
        /// </summary>
        /// <value>
        /// The number of Watt-Hours consumed by client per revolution on meter.
        /// </value>
        public decimal? BillingKh { get; private set; }
    }
}
