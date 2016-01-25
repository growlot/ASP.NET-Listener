// <copyright file="EquipmentState.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// All equipment types in WNP has some common information assigned to it. This information could be called it's state,
    /// because it includes such things as status and location. It is also used to decide what business actions can be
    /// performed with this piece of quipment.
    /// </summary>
    public class EquipmentState : Entity<EquipmentId>
    {
        private const string LeaveAsIs = "*LEAVE_AS_IS*";

        private IList<IDomainEvent> events;
        private string workflow;
        private Location location;
        private string equipmentStatus;
        private string detailedStatus;
        private int shopCycle;
        private string boxNumber;
        private string palletNumber;
        private string shelfId;
        private string issuedTo;
        private string vehicleNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentState"/> class.
        /// </summary>
        public EquipmentState()
        {
        }

        internal string Workflow
        {
            get
            {
                return this.workflow;
            }
        }

        /// <summary>
        /// Activates the related entity by giving acces to aggregate root event list.
        /// </summary>
        /// <param name="aggregateRootEvents">The aggregate root events.</param>
        internal void ActivateEvents(IList<IDomainEvent> aggregateRootEvents)
        {
            this.events = aggregateRootEvents;
        }

        /// <summary>
        /// Checks if current equipment status match provided incoming rule
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <returns><c>true</c> if rule match, <c>false</c> otherwise.</returns>
        internal bool MatchIncomingRule(IncomingRule rule)
        {
            if (rule.Workflow != this.workflow
                || (rule.LocationType != null && !this.location.BelongsTo(rule.LocationType))
                || (rule.Location != null && !rule.Location.Equals(this.location))
                || rule.EquipmentStatus != this.equipmentStatus
                || rule.DetailedStatus != this.detailedStatus)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates equipment state as specified in business action.
        /// </summary>
        /// <param name="workstationId">The workstation identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="newBoxNumber">The box number.</param>
        /// <param name="newPalletNumber">The pallet number.</param>
        /// <param name="newShelfId">The shelf identifier.</param>
        /// <param name="newIssuedTo">The issued to.</param>
        /// <param name="newVehicleNumber">The vehicle number.</param>
        /// <param name="newLocation">The new location. Only used if action defines location type instead of specific location.</param>
        /// <exception cref="System.InvalidOperationException">Can not update equipment state, because entity doesn't belong to any aggregate root.</exception>
        internal void UpdateState(
            string workstationId,
            BusinessAction action,
            string newBoxNumber,
            string newPalletNumber,
            string newShelfId,
            string newIssuedTo,
            string newVehicleNumber,
            Location newLocation)
        {
            if (this.events == null)
            {
                throw new InvalidOperationException("Can not update equipment state, because entity doesn't belong to any aggregate root.");
            }

            // if action didn't have specific location configured, then use location specified by user
            if (action.NewLocation != null)
            {
                // leave as is can be configured only for specific locations
                this.location = action.NewLocation.Id == LeaveAsIs ? this.location : action.NewLocation;
            }
            else
            {
                this.location = newLocation;
            }

            this.workflow = action.NewWorkflow == LeaveAsIs ? this.workflow : action.NewWorkflow;
            this.equipmentStatus = action.NewEquipmentStatus == LeaveAsIs ? this.equipmentStatus : action.NewEquipmentStatus;
            this.detailedStatus = action.NewEquipmentStatus == LeaveAsIs ? this.detailedStatus : action.NewDetailedStatus;
            this.boxNumber = newBoxNumber;
            this.palletNumber = newPalletNumber;
            this.shelfId = newShelfId;
            this.issuedTo = newIssuedTo;
            this.vehicleNumber = newVehicleNumber;

            this.shopCycle = action.IncrementCycle ? this.shopCycle++ : this.shopCycle;
            this.boxNumber = (!string.IsNullOrWhiteSpace(action.ActionBox) && action.ActionBox == "C") ? null : this.boxNumber;
            this.issuedTo = (!string.IsNullOrWhiteSpace(action.ActionReceivedBy) && action.ActionReceivedBy == "C") ? null : this.issuedTo;
            this.palletNumber = (!string.IsNullOrWhiteSpace(action.ActionPallet) && action.ActionPallet == "C") ? null : this.palletNumber;
            this.shelfId = (!string.IsNullOrWhiteSpace(action.ActionShelf) && action.ActionShelf == "C") ? null : this.shelfId;
            this.vehicleNumber = (!string.IsNullOrWhiteSpace(action.ActionVehicleNumber) && action.ActionVehicleNumber == "C") ? null : this.vehicleNumber;

            var equipmentStateChangedEvent = new EquipmentStateChangedEvent(
                equipmentNumber: this.Id.EquipmentNumber,
                equipmentType: this.Id.EquipmentType,
                workstation: workstationId,
                workflow: this.workflow,
                location: this.location.Id,
                equipmentStatus: this.equipmentStatus,
                detailedStatus: this.detailedStatus,
                shopCycle: this.shopCycle,
                boxNumber: this.boxNumber,
                palletNumber: this.palletNumber,
                shelfId: this.shelfId,
                issuedTo: this.issuedTo,
                vehicle: this.vehicleNumber);
            this.events.Add(equipmentStateChangedEvent);
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var equipmentStateMemento = (EquipmentStateMemento)memento;
            if (equipmentStateMemento.Location != null)
            {
                this.location = new Location();
                ((IOriginator)this.location).SetMemento(equipmentStateMemento.Location);
            }

            this.Id = new EquipmentId(equipmentStateMemento.EquipmentType, equipmentStateMemento.EquipmentNumber);
            this.workflow = equipmentStateMemento.Workflow;
            this.equipmentStatus = equipmentStateMemento.EquipmentStatus;
            this.detailedStatus = equipmentStateMemento.DetailedStatus;
            this.shopCycle = equipmentStateMemento.ShopCycle;
            this.boxNumber = equipmentStateMemento.BoxNumber;
            this.palletNumber = equipmentStateMemento.PalletNumber;
            this.shelfId = equipmentStateMemento.ShelfId;
            this.issuedTo = equipmentStateMemento.IssuedTo;
            this.vehicleNumber = equipmentStateMemento.VehicleNumber;
        }
    }
}
