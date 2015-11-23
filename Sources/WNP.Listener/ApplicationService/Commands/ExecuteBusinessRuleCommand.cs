// <copyright file="ExecuteBusinessRuleCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    /// <summary>
    /// Command to execute business rule
    /// </summary>
    public class ExecuteBusinessRuleCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the workstation where business action is defined.
        /// </summary>
        /// <value>
        /// The workstation.
        /// </value>
        public string Workstation { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the box number if current business action defines equipment packaging to the box.
        /// </summary>
        /// <value>
        /// The box number.
        /// </value>
        public string BoxNumber { get; set; }

        /// <summary>
        /// Gets or sets the pallet number if current business action defines equipment packaging to the pallet.
        /// </summary>
        /// <value>
        /// The pallet number.
        /// </value>
        public string PalletNumber { get; set; }

        /// <summary>
        /// Gets or sets the shelf identifier if current business action defines equipment warehousing.
        /// </summary>
        /// <value>
        /// The shelf identifier.
        /// </value>
        public string ShelfId { get; set; }

        /// <summary>
        /// Gets or sets the user who recieved the equipment if current business action defines equipment checkout.
        /// </summary>
        /// <value>
        /// The user receiving equipment.
        /// </value>
        public string IssuedTo { get; set; }

        /// <summary>
        /// Gets or sets the vehicle number if current business action defines equipment checkout.
        /// </summary>
        /// <value>
        /// The vehicle number.
        /// </value>
        public string VehicleNumber { get; set; }
    }
}