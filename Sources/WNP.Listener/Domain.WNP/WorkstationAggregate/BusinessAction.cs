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
        private readonly Location newLocation;
        private readonly string newLocationType;
        private readonly bool incrementCycle;
        private readonly string actionBox;
        private readonly string actionPallet;
        private readonly string actionShelf;
        private readonly string actionReceivedBy;
        private readonly string actionVehicleNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessAction" /> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="currentWorkflow">The current workflow.</param>
        /// <param name="newWorkflow">The new workflow.</param>
        /// <param name="newEquipmentStatus">The new equipment status.</param>
        /// <param name="newDetailedStatus">The new detailed status.</param>
        /// <param name="newLocation">New location.</param>
        /// <param name="newLocationType">Type of new location.</param>
        /// <param name="incrementCycle">if set to <c>true</c> [increment cycle].</param>
        /// <param name="actionBox">Represents the action field [action box]. Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        /// <param name="actionPallet">Represents the action field [action pallet]. Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        /// <param name="actionShelf">Represents the action field [action shelf]. Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        /// <param name="actionReceivedBy">Represents the action field [action received by]. Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        /// <param name="actionVehicleNumber">Represents the action field [action vehicle number]. Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        public BusinessAction(
            string actionName,
            string currentWorkflow,
            string newWorkflow,
            string newEquipmentStatus,
            string newDetailedStatus,
            Location newLocation,
            string newLocationType,
            bool incrementCycle,
            string actionBox,
            string actionPallet,
            string actionShelf,
            string actionReceivedBy,
            string actionVehicleNumber)
        {
            this.actionName = actionName;
            this.currentWorkflow = currentWorkflow;
            this.newWorkflow = newWorkflow;
            this.newEquipmentStatus = newEquipmentStatus;
            this.newDetailedStatus = newDetailedStatus;
            this.newLocation = newLocation;
            this.newLocationType = newLocationType;
            this.incrementCycle = incrementCycle;
            this.actionBox = actionBox;
            this.actionPallet = actionPallet;
            this.actionShelf = actionShelf;
            this.actionReceivedBy = actionReceivedBy;
            this.actionVehicleNumber = actionVehicleNumber;
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
        public Location NewLocation
        {
            get
            {
                return this.newLocation;
            }
        }

        /// <summary>
        /// Gets the type of new location.
        /// </summary>
        /// <value>
        /// The type of new location.
        /// </value>
        public string NewLocationType
        {
            get
            {
                return this.newLocationType;
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
        /// Gets a value indicating if box information should be set or cleared during this action.
        /// </summary>
        /// <value>
        ///   'D' if rule is disabled.
        /// </value>
        /// <value>
        ///   'E' if rule is enabled.
        /// </value>
        /// <value>
        ///   'R' if value is required for Box.
        /// </value>
        /// <value>
        ///   'C' if value needs to be clear for Box.
        /// </value>
        public string ActionBox
        {
            get
            {
                return this.actionBox;
            }
        }

        /// <summary>
        /// Gets a value indicating if pallet information should be set or cleared during this action.
        /// </summary>
        /// <value>
        ///   'D' if rule is disabled.
        /// </value>
        /// <value>
        ///   'E' if rule is enabled.
        /// </value>
        /// <value>
        ///   'R' if value is required for Pallet.
        /// </value>
        /// <value>
        ///   'C' if value needs to be clear for Pallet.
        /// </value>
        public string ActionPallet
        {
            get
            {
                return this.actionPallet;
            }
        }

        /// <summary>
        /// Gets a value indicating if shelf information should be set or cleared during this action.
        /// </summary>
        /// <value>
        ///   'D' if rule is disabled.
        /// </value>
        /// <value>
        ///   'E' if rule is enabled.
        /// </value>
        /// <value>
        ///   'R' if value is required for Shelf Id.
        /// </value>
        /// <value>
        ///   'C' if value needs to be clear for Shelf Id.
        /// </value>
        public string ActionShelf
        {
            get
            {
                return this.actionShelf;
            }
        }

        /// <summary>
        /// Gets a value indicating whether if received by information should be set or cleared during this action.
        /// </summary>
        /// <value>
        ///   'D' if rule is disabled.
        /// </value>
        /// <value>
        ///   'E' if rule is enabled.
        /// </value>
        /// <value>
        ///   'R' if value is required for received by field.
        /// </value>
        /// <value>
        ///   'C' if value needs to be clear for received by feild.
        /// </value>
        public string ActionReceivedBy
        {
            get
            {
                return this.actionReceivedBy;
            }
        }

        /// <summary>
        /// Gets a value indicating whether if vehicle number information should be set or cleared during this action.
        /// </summary>
        /// <value>
        ///   'D' if rule is disabled.
        /// </value>
        /// <value>
        ///   'E' if rule is enabled.
        /// </value>
        /// <value>
        ///   'R' if value is required for vehicle number.
        /// </value>
        /// <value>
        ///   'C' if value needs to be clear for vehicle number.
        /// </value>
        public string ActionVehicleNumber
        {
            get
            {
                return this.actionVehicleNumber;
            }
        }
    }
}