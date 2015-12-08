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
        /// <param name="clearBox">if set to <c>true</c> [clear box].</param>
        /// <param name="clearPallet">if set to <c>true</c> [clear pallet].</param>
        /// <param name="clearShelf">if set to <c>true</c> [clear shelf].</param>
        /// <param name="clearIssuedTo">if set to <c>true</c> [clear issued to].</param>
        /// <param name="clearVehicleNumber">if set to <c>true</c> [clear vehicle number].</param>
        public BusinessActionMemento(
            string actionName,
            string currentWorkflow,
            string newWorkflow,
            string newEquipmentStatus,
            string newDetailedStatus,
            IMemento newLocation,
            string newLocationType,
            bool incrementCycle,
            bool clearBox,
            bool clearPallet,
            bool clearShelf,
            bool clearIssuedTo,
            bool clearVehicleNumber)
        {
            this.ActionName = actionName;
            this.CurrentWorkflow = currentWorkflow;
            this.NewWorkflow = newWorkflow;
            this.NewEquipmentStatus = newEquipmentStatus;
            this.NewDetailedStatus = newDetailedStatus;
            this.NewLocation = newLocation;
            this.NewLocationType = newLocationType;
            this.IncrementCycle = incrementCycle;
            this.ClearBox = clearBox;
            this.ClearPallet = clearPallet;
            this.ClearShelf = clearShelf;
            this.ClearIssuedTo = clearIssuedTo;
            this.ClearVehicleNumber = clearVehicleNumber;
        }

        internal string ActionName { get; private set; }

        internal string CurrentWorkflow { get; private set; }

        internal string NewWorkflow { get; private set; }

        internal string NewEquipmentStatus { get; private set; }

        internal string NewDetailedStatus { get; private set; }

        internal IMemento NewLocation { get; private set; }

        internal string NewLocationType { get; private set; }

        internal bool IncrementCycle { get; private set; }

        internal bool ClearBox { get; private set; }

        internal bool ClearPallet { get; private set; }

        internal bool ClearShelf { get; private set; }

        internal bool ClearIssuedTo { get; private set; }

        internal bool ClearVehicleNumber { get; private set; }
    }
}
