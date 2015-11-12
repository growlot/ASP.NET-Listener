// <copyright file="CircuitMeterMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Memento class for electric meter connected to circuit
    /// </summary>
    public class CircuitMeterMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitMeterMemento" /> class.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="internalMultiplier">The internal multiplier.</param>
        /// <param name="billingMultiplier">The billing multiplier.</param>
        /// <param name="kh">The kh.</param>
        /// <param name="billingKh">The billing kh.</param>
        public CircuitMeterMemento(
            string equipmentNumber,
            decimal? internalMultiplier,
            decimal? billingMultiplier,
            decimal kh,
            decimal? billingKh)
        {
            this.EquipmentNumber = equipmentNumber;
            this.InternalMultiplier = internalMultiplier;
            this.BillingMultiplier = billingMultiplier;
            this.Kh = kh;
            this.BillingKh = billingKh;
        }

        /// <summary>
        /// Gets the meters equipment number.
        /// </summary>
        /// <value>
        /// The meters equipment number.
        /// </value>
        internal string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the meters internal multiplier.
        /// </summary>
        /// <value>
        /// The meters internal multiplier.
        /// </value>
        internal decimal? InternalMultiplier { get; private set; }

        /// <summary>
        /// Gets the billing multiplier.
        /// </summary>
        /// <value>
        /// The billing multiplier.
        /// </value>
        internal decimal? BillingMultiplier { get; private set; }

        /// <summary>
        /// Gets the number of Watt-Hours per revolution on meter.
        /// </summary>
        /// <value>
        /// The the number of Watt-Hours per revolution on meter.
        /// </value>
        internal decimal Kh { get; private set; }

        /// <summary>
        /// Gets the number of Watt-Hours consumed by client per revolution on meter.
        /// </summary>
        /// <value>
        /// The number of Watt-Hours consumed by client per revolution on meter.
        /// </value>
        internal decimal? BillingKh { get; private set; }
    }
}
