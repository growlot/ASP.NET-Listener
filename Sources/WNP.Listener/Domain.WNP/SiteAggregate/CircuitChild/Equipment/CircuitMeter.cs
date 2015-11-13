// <copyright file="CircuitMeter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Electric meter component in the circuit.
    /// </summary>
    public class CircuitMeter : Entity<string>
    {
        private InstallationPoint installationPoint;
        private EquipmentStatus status;
        private IList<IDomainEvent> events;
        private decimal internalMultiplier;
        private decimal? billingMultiplier;
        private decimal kh;
        private decimal? billingKh;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitMeter"/> class.
        /// </summary>
        public CircuitMeter()
        {
            this.events = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitMeter"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public CircuitMeter(IList<IDomainEvent> events)
        {
            this.events = events;
        }

        /// <summary>
        /// Activates the related entity by giving acces to aggregate root event list.
        /// </summary>
        /// <param name="aggregateRootEvents">The aggregate root events.</param>
        public void ActivateEvents(IList<IDomainEvent> aggregateRootEvents)
        {
            this.events = aggregateRootEvents;
        }

        /// <summary>
        /// Updates the billing information for this meter.
        /// </summary>
        /// <param name="circuitMultiplier">The circuit multiplier.</param>
        /// <exception cref="System.InvalidOperationException">Can not update billing information, because entity doesn't belong to any aggregate root.</exception>
        public void UpdateBillingInformation(decimal circuitMultiplier)
        {
            if (this.events == null)
            {
                throw new InvalidOperationException("Can not update billing information, because entity doesn't belong to any aggregate root.");
            }

            var newBillingMultiplier = circuitMultiplier * this.internalMultiplier;
            var newBillingKh = newBillingMultiplier * this.kh;

            if (newBillingMultiplier != this.billingMultiplier || newBillingKh != this.billingKh)
            {
                this.billingMultiplier = newBillingMultiplier;
                this.billingKh = newBillingKh;

                var multiplierUpdatedEvent = new MeterBillingInformationUpdatedEvent(this.Id, this.billingMultiplier, this.billingKh);
                this.events.Add(multiplierUpdatedEvent);
            }
        }

        /// <summary>
        /// Determines whether this meter can be installed. If not provides the reason why.
        /// </summary>
        /// <param name="reason">The reason why meter can't be installed. Null if it can be installed.</param>
        /// <returns>True if meter can be installed, false otherwise.</returns>
        public bool CanBeInstalled(out string reason)
        {
            reason = null;
            if (this.status.IsRetired())
            {
                reason = "Meter can not be installed, because it is retired.";
                return false;
            }

            if (this.installationPoint.SiteId != null)
            {
                reason = "Meter can not be installed, because it is already installed in another location.";
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var meterMemento = (CircuitMeterMemento)memento;
            this.Id = meterMemento.EquipmentNumber;
            this.installationPoint = new InstallationPoint(meterMemento.SiteId, meterMemento.CircuitId);
            this.status = new EquipmentStatus(meterMemento.Status);
            this.billingMultiplier = meterMemento.BillingMultiplier;
            this.billingKh = meterMemento.BillingKh;
            this.kh = meterMemento.Kh;

            this.internalMultiplier = 1;
            if (meterMemento.InternalMultiplier.HasValue)
            {
                this.internalMultiplier = meterMemento.InternalMultiplier.Value;
            }
        }
    }
}
