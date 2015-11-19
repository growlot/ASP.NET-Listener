// <copyright file="UninstallMeterCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Install meter command
    /// </summary>
    public class UninstallMeterCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the circuit identifier.
        /// </summary>
        /// <value>
        /// The circuit identifier.
        /// </value>
        public int CircuitId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the uninstall date.
        /// </summary>
        /// <value>
        /// The uninstall date.
        /// </value>
        public DateTime UninstallDate { get; set; }

        /// <summary>
        /// Gets or sets the uninstall user.
        /// </summary>
        /// <value>
        /// The uninstall user.
        /// </value>
        public string UninstallUser { get; set; }

        /// <summary>
        /// Gets or sets the uninstall service order started.
        /// </summary>
        /// <value>
        /// The uninstall service order started.
        /// </value>
        public DateTime? UninstallServiceOrderStarted { get; set; }

        /// <summary>
        /// Gets or sets the uninstall service order completed.
        /// </summary>
        /// <value>
        /// The uninstall service order completed.
        /// </value>
        public DateTime? UninstallServiceOrderCompleted { get; set; }

        /// <summary>
        /// Gets or sets the uninstall reason.
        /// </summary>
        /// <value>
        /// The uninstall reason.
        /// </value>
        public string UninstallReason { get; set; }
    }
}
