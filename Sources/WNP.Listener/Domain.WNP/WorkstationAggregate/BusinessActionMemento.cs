// <copyright file="BusinessActionMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Memento class for business action value object
    /// </summary>
    public class BusinessActionMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessActionMemento" /> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="currentWorkflow">The current workflow.</param>
        /// <param name="newWorkflow">The new workflow.</param>
        /// <param name="newEquipmentStatus">The new equipment status.</param>
        /// <param name="newDetailedStatus">The new detailed status.</param>
        /// <param name="newLocation">The new location.</param>
        /// <param name="newLocationType">The type of new location.</param>
        /// <param name="incrementCycle">if set to <c>true</c> [increment cycle].</param>
        /// <param name="actionBox">The action to be taken for box.</param>
        /// <param name="actionPallet">The action to be taken for Pallet.</param>
        /// <param name="actionShelf">The action to be taken for Shelf.</param>
        /// <param name="actionReceivedBy">The action to be taken for Issued To.</param>
        /// <param name="actionVehicleNumber">The action to be taken for Vehicle Number.</param>
        public BusinessActionMemento(
            string actionName,
            string currentWorkflow,
            string newWorkflow,
            string newEquipmentStatus,
            string newDetailedStatus,
            IMemento newLocation,
            string newLocationType,
            bool incrementCycle,
            ActionFlag actionBox,
            ActionFlag actionPallet,
            ActionFlag actionShelf,
            ActionFlag actionReceivedBy,
            ActionFlag actionVehicleNumber)
        {
            this.ActionName = actionName;
            this.CurrentWorkflow = currentWorkflow;
            this.NewWorkflow = newWorkflow;
            this.NewEquipmentStatus = newEquipmentStatus;
            this.NewDetailedStatus = newDetailedStatus;
            this.NewLocation = newLocation;
            this.NewLocationType = newLocationType;
            this.IncrementCycle = incrementCycle;
            this.ActionBox = actionBox;
            this.ActionPallet = actionPallet;
            this.ActionShelf = actionShelf;
            this.ActionReceivedBy = actionReceivedBy;
            this.ActionVehicleNumber = actionVehicleNumber;
        }

        internal string ActionName { get; private set; }

        internal string CurrentWorkflow { get; private set; }

        internal string NewWorkflow { get; private set; }

        internal string NewEquipmentStatus { get; private set; }

        internal string NewDetailedStatus { get; private set; }

        internal IMemento NewLocation { get; private set; }

        internal string NewLocationType { get; private set; }

        internal bool IncrementCycle { get; private set; }

        internal ActionFlag ActionBox { get; private set; }

        internal ActionFlag ActionPallet { get; private set; }

        internal ActionFlag ActionShelf { get; private set; }

        internal ActionFlag ActionReceivedBy { get; private set; }

        internal ActionFlag ActionVehicleNumber { get; private set; }
    }
}
