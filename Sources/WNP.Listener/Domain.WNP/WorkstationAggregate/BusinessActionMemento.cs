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
        public BusinessActionMemento(
            string actionName,
            string currentWorkflow,
            string newWorkflow,
            string newEquipmentStatus,
            string newDetailedStatus,
            IMemento newLocation,
            string newLocationType,
            bool incrementCycle,
            string actionBox,
            string actionPallet,
            string actionShelf,
            string actionReceivedBy,
            string actionVehicleNumber)
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

        internal string ActionBox { get; private set; }

        internal string ActionPallet { get; private set; }

        internal string ActionShelf { get; private set; }

        internal string ActionReceivedBy { get; private set; }

        internal string ActionVehicleNumber { get; private set; }
    }
}
