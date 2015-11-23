// <copyright file="BusinessAction.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Business action defines how equipment state changes.
    /// </summary>
    public class BusinessAction : ValueObject<BusinessAction>
    {
        private readonly string actionName;
        private readonly string currentWorkflow;
        private readonly string newWorkflow;
        private readonly string newEquipmentStatus;
        private readonly string newDetailedStatus;
        private readonly string newLocation;
        private readonly string locationType;
        private readonly bool incrementCycle;
        private readonly bool clearBox;
        private readonly bool clearPallet;
        private readonly bool clearShelf;
        private readonly bool clearIssuedTo;
        private readonly bool clearVehicleNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessAction" /> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="currentWorkflow">The current workflow.</param>
        /// <param name="newWorkflow">The new workflow.</param>
        /// <param name="newEquipmentStatus">The new equipment status.</param>
        /// <param name="newDetailedStatus">The new detailed status.</param>
        /// <param name="newLocation">The new location.</param>
        /// <param name="locationType">Type of the location.</param>
        /// <param name="incrementCycle">if set to <c>true</c> [increment cycle].</param>
        /// <param name="clearBox">if set to <c>true</c> [clear box].</param>
        /// <param name="clearPallet">if set to <c>true</c> [clear pallet].</param>
        /// <param name="clearShelf">if set to <c>true</c> [clear shelf].</param>
        /// <param name="clearIssuedTo">if set to <c>true</c> [clear issued to].</param>
        /// <param name="clearVehicleNumber">if set to <c>true</c> [clear vehicle number].</param>
        public BusinessAction(
            string actionName,
            string currentWorkflow,
            string newWorkflow,
            string newEquipmentStatus,
            string newDetailedStatus,
            string newLocation,
            string locationType,
            bool incrementCycle,
            bool clearBox,
            bool clearPallet,
            bool clearShelf,
            bool clearIssuedTo,
            bool clearVehicleNumber)
        {
            this.actionName = actionName;
            this.currentWorkflow = currentWorkflow;
            this.newWorkflow = newWorkflow;
            this.newEquipmentStatus = newEquipmentStatus;
            this.newDetailedStatus = newDetailedStatus;
            this.newLocation = newLocation;
            this.locationType = locationType;
            this.incrementCycle = incrementCycle;
            this.clearBox = clearBox;
            this.clearPallet = clearPallet;
            this.clearShelf = clearShelf;
            this.clearIssuedTo = clearIssuedTo;
            this.clearVehicleNumber = clearVehicleNumber;
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName
        {
            get
            {
                return this.actionName;
            }
        }

        /// <summary>
        /// Gets the current workflow.
        /// </summary>
        /// <value>
        /// The current test program.
        /// </value>
        public string CurrentWorkflow
        {
            get
            {
                return this.currentWorkflow;
            }
        }

        /// <summary>
        /// Gets the workflow.
        /// </summary>
        /// <value>
        /// The new workflow.
        /// </value>
        public string NewWorkflow
        {
            get
            {
                return this.newWorkflow;
            }
        }

        /// <summary>
        /// Gets the new equipment status.
        /// </summary>
        /// <value>
        /// The new equipment status.
        /// </value>
        public string NewEquipmentStatus
        {
            get
            {
                return this.newEquipmentStatus;
            }
        }

        /// <summary>
        /// Gets the new detailed status.
        /// </summary>
        /// <value>
        /// The new detailed status.
        /// </value>
        public string NewDetailedStatus
        {
            get
            {
                return this.newDetailedStatus;
            }
        }

        /// <summary>
        /// Gets the new location.
        /// </summary>
        /// <value>
        /// The new location.
        /// </value>
        public string NewLocation
        {
            get
            {
                return this.newLocation;
            }
        }

        /// <summary>
        /// Gets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        public string LocationType
        {
            get
            {
                return this.locationType;
            }
        }

        /// <summary>
        /// Gets a value indicating whether shop cycle should be incremented after this action is executed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if shop cycle should be incremented after this action is executed; otherwise, <c>false</c>.
        /// </value>
        public bool IncrementCycle
        {
            get
            {
                return this.incrementCycle;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment assignment to box (container) should be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if equipment assignment to box (container) should be removed; otherwise, <c>false</c>.
        /// </value>
        public bool ClearBox
        {
            get
            {
                return this.clearBox;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment assignment to pallet (container) should be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if equipment assignment to pallet (container) should be removed; otherwise, <c>false</c>.
        /// </value>
        public bool ClearPallet
        {
            get
            {
                return this.clearPallet;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment shelf location should be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if equipment shelf location should be removed; otherwise, <c>false</c>.
        /// </value>
        public bool ClearShelf
        {
            get
            {
                return this.clearShelf;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment assignment to user should be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if equipment assignment to user should be removed; otherwise, <c>false</c>.
        /// </value>
        public bool ClearIssuedTo
        {
            get
            {
                return this.clearIssuedTo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment assignment to vehicle should be removed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if equipment assignment to vehicle should be removed; otherwise, <c>false</c>.
        /// </value>
        public bool ClearVehicleNumber
        {
            get
            {
                return this.clearVehicleNumber;
            }
        }
    }
}