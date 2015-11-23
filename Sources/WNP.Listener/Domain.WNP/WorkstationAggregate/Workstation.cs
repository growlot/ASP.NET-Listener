// <copyright file="Workstation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// WNP works using the concept of "workstations". Workstations can be physical places where workflow processing occurs
    /// (ex. Testing, Shipping, Receiving, etc.). They may also be virtual, where data processing occurs, but a WNP application window is not used.
    /// Workstation can perform only the business actions that are assigned to it, as defined by the IN and OUT tracking rules for that workstation.
    /// </summary>
    public class Workstation : AggregateRoot<string>
    {
        private IList<BusinessAction> businessActions = new List<BusinessAction>();
        private IList<IncomingRule> incomingRules = new List<IncomingRule>();

        /// <summary>
        /// Performs the business action.
        /// </summary>
        /// <param name="equipment">The equipment.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="boxNumber">The box number.</param>
        /// <param name="palletNumber">The pallet number.</param>
        /// <param name="shelfId">The shelf identifier.</param>
        /// <param name="issuedTo">The issued to.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <exception cref="System.ArgumentNullException">Can not perform business action, because equipment is not specified.</exception>
        /// <exception cref="System.ArgumentException">Can not perform business action, becasue there is no action with name {0} defined in workstation {1}.FormatWith(actionName, this.Id)</exception>
        /// <exception cref="System.InvalidOperationException">Can not perform business action, becasue equipment state doesn't allow it: {0}.FormatWith(rule.Message)
        /// or
        /// Can not perform business action, because equipment state doesn't allow it.</exception>
        public void PerformBusinessAction(
            EquipmentState equipment,
            string actionName,
            string boxNumber,
            string palletNumber,
            string shelfId,
            string issuedTo,
            string vehicleId)
        {
            if (equipment == null)
            {
                throw new ArgumentNullException(nameof(equipment), "Can not perform business action, because equipment is not specified.");
            }

            equipment.ActivateEvents(this.Events);

            // multiple actions with same name can exist in workstation
            // in such case they can be separated by workflow
            var action = this.businessActions.FirstOrDefault(item => (item.ActionName == actionName) && (item.CurrentWorkflow == equipment.Workflow));

            if (action == null)
            {
                throw new ArgumentException("Can not perform business action, becasue there is no action with name {0} and workflow (test program) {1} defined in workstation {2}".FormatWith(actionName, equipment.Workflow, this.Id), nameof(actionName));
            }

            // if at least one allowed inocmming rule match for current equipment status, then perform the action and exit.
            foreach (var rule in this.incomingRules.Where(item => item.IsAllowed))
            {
                if (equipment.MatchIncomingRule(rule))
                {
                    equipment.UpdateState(
                        workstationId: this.Id,
                        action: action,
                        newBoxNumber: boxNumber,
                        newPalletNumber: palletNumber,
                        newShelfId: shelfId,
                        newIssuedTo: issuedTo,
                        newVehicleNumber: vehicleId);
                    return;
                }
            }

            // if there is not allowed incomming rule defined, throw exception including message from the rule.
            foreach (var rule in this.incomingRules.Where(item => !item.IsAllowed))
            {
                if (equipment.MatchIncomingRule(rule))
                {
                    throw new InvalidOperationException("Can not perform business action, becasue equipment state doesn't allow it: {0}".FormatWith(rule.Message));
                }
            }

            // if no rules matched current equipment state, throw generic exeption.
            throw new InvalidOperationException("Can not perform business action, because equipment state doesn't allow it.");
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var workstationMemento = (WorkstationMemento)memento;
            this.Id = workstationMemento.Name;

            foreach (var item in workstationMemento.IncomingRules)
            {
                var incomingRuleMemento = (IncomingRuleMemento)item;
                var incomingRule = new IncomingRule(
                    workflow: incomingRuleMemento.Workflow,
                    isAllowed: incomingRuleMemento.IsAllowed,
                    equipmentStatus: incomingRuleMemento.EquipmentStatus,
                    detailedStatus: incomingRuleMemento.DetailedStatus,
                    location: incomingRuleMemento.Location,
                    locationType: incomingRuleMemento.LocationType,
                    message: incomingRuleMemento.Message);
                this.incomingRules.Add(incomingRule);
            }

            foreach (var item in workstationMemento.BusinessActions)
            {
                var businessActionMemento = (BusinessActionMemento)item;
                var businessAction = new BusinessAction(
                    actionName: businessActionMemento.ActionName,
                    currentWorkflow: businessActionMemento.CurrentWorkflow,
                    newWorkflow: businessActionMemento.NewWorkflow,
                    newEquipmentStatus: businessActionMemento.NewEquipmentStatus,
                    newDetailedStatus: businessActionMemento.NewDetailedStatus,
                    newLocation: businessActionMemento.NewLocation,
                    locationType: businessActionMemento.LocationType,
                    incrementCycle: businessActionMemento.IncrementCycle,
                    clearBox: businessActionMemento.ClearBox,
                    clearPallet: businessActionMemento.ClearPallet,
                    clearShelf: businessActionMemento.ClearShelf,
                    clearIssuedTo: businessActionMemento.ClearIssuedTo,
                    clearVehicleNumber: businessActionMemento.ClearVehicleNumber);
                this.businessActions.Add(businessAction);
            }
        }
    }
}
