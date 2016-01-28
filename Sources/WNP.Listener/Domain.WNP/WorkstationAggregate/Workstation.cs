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
        /// <param name="location">The location.</param>
        /// <exception cref="ArgumentNullException">Can not perform business action, because equipment is not specified.</exception>
        /// <exception cref="ArgumentException">Can not perform business action, becasue there is no action with name {0} defined in workstation {1}.FormatWith(actionName, this.Id)</exception>
        /// <exception cref="InvalidOperationException">Can not perform business action, becasue equipment state doesn't allow it: {0}.FormatWith(rule.Message)
        /// or
        /// Can not perform business action, because equipment state doesn't allow it.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Review Me!")]
        public void PerformBusinessAction(
            EquipmentState equipment,
            string actionName,
            string boxNumber,
            string palletNumber,
            string shelfId,
            string issuedTo,
            string vehicleId,
            Location location)
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

            // validate values for boxNumber field
            this.ValidateFieldAction(actionName, action.ActionBox, nameof(boxNumber), boxNumber);

            // validate values for palletNumber field
            this.ValidateFieldAction(actionName, action.ActionPallet, nameof(palletNumber), palletNumber);

            // validate values for shelfId field
            this.ValidateFieldAction(actionName, action.ActionShelf, nameof(shelfId), shelfId);

            // validate values for issuedTo field
            this.ValidateFieldAction(actionName, action.ActionReceivedBy, nameof(issuedTo), issuedTo);

            // validate values for vehicleId field
            this.ValidateFieldAction(actionName, action.ActionVehicleNumber, nameof(vehicleId), vehicleId);

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
                        newVehicleNumber: vehicleId,
                        newLocation: location);
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
                Location location = null;
                if (incomingRuleMemento.Location != null)
                {
                    location = new Location();
                    ((IOriginator)location).SetMemento(incomingRuleMemento.Location);
                }

                var incomingRule = new IncomingRule(
                    workflow: incomingRuleMemento.Workflow,
                    isAllowed: incomingRuleMemento.IsAllowed,
                    equipmentStatus: incomingRuleMemento.EquipmentStatus,
                    detailedStatus: incomingRuleMemento.DetailedStatus,
                    location: location,
                    locationType: incomingRuleMemento.LocationType,
                    message: incomingRuleMemento.Message);
                this.incomingRules.Add(incomingRule);
            }

            foreach (var item in workstationMemento.BusinessActions)
            {
                var businessActionMemento = (BusinessActionMemento)item;
                Location location = null;
                if (businessActionMemento.NewLocation != null)
                {
                    location = new Location();
                    ((IOriginator)location).SetMemento(businessActionMemento.NewLocation);
                }

                var businessAction = new BusinessAction(
                    actionName: businessActionMemento.ActionName,
                    currentWorkflow: businessActionMemento.CurrentWorkflow,
                    newWorkflow: businessActionMemento.NewWorkflow,
                    newEquipmentStatus: businessActionMemento.NewEquipmentStatus,
                    newDetailedStatus: businessActionMemento.NewDetailedStatus,
                    newLocation: location,
                    newLocationType: businessActionMemento.NewLocationType,
                    incrementCycle: businessActionMemento.IncrementCycle,
                    actionBox: new ActionValue(businessActionMemento.ActionBox),
                    actionPallet: new ActionValue(businessActionMemento.ActionPallet),
                    actionShelf: new ActionValue(businessActionMemento.ActionShelf),
                    actionReceivedBy: new ActionValue(businessActionMemento.ActionReceivedBy),
                    actionVehicleNumber: new ActionValue(businessActionMemento.ActionVehicleNumber));
                this.businessActions.Add(businessAction);
            }
        }

        /// <summary>
        /// Validates the value for action fields.
        /// </summary>
        /// <param name="actionName">Name of business action.</param>
        /// <param name="actionFlag">Action set for the business action.</param>
        /// <param name="fieldName">Name of field to be validated for the action.</param>
        /// <param name="fieldValue">Value of field to be validated for the business action.</param>
        private void ValidateFieldAction(string actionName, ActionValue actionFlag, string fieldName, string fieldValue)
        {
            if (actionFlag == null)
            {
                return;
            }

            // do not perform business action if action for the field is set to Clear but value is provided
            if (actionFlag == ActionValue.Clear && !string.IsNullOrWhiteSpace(fieldValue))
            {
                throw new ArgumentException("Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear".FormatWith(actionName, this.Id, fieldName));
            }

            // do not perform business action if action for the field is set to Disabled but value is provided
            if (actionFlag == ActionValue.Disabled && !string.IsNullOrWhiteSpace(fieldValue))
            {
                throw new ArgumentException("Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled".FormatWith(actionName, this.Id, fieldName));
            }

            // do not perform business action if action for the field is set to Required but value is missing
            if (actionFlag == ActionValue.Required && string.IsNullOrWhiteSpace(fieldValue))
            {
                throw new ArgumentException("Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'".FormatWith(actionName, this.Id, fieldName));
            }
        }
    }
}
