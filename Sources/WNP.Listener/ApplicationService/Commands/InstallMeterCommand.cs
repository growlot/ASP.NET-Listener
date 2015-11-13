// <copyright file="InstallMeterCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Install meter command
    /// </summary>
    public class InstallMeterCommand : ICommand
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
        /// Gets or sets the equipment type.
        /// </summary>
        /// <value>
        /// The equipment type.
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
        /// Gets or sets the install date.
        /// </summary>
        /// <value>
        /// The install date.
        /// </value>
        public DateTime InstallDate { get; set; }

        /// <summary>
        /// Gets or sets the install user.
        /// </summary>
        /// <value>
        /// The install user.
        /// </value>
        public string InstallUser { get; set; }

        /// <summary>
        /// Gets or sets the install service order started.
        /// </summary>
        /// <value>
        /// The install service order started.
        /// </value>
        public DateTime? InstallServiceOrderStarted { get; set; }

        /// <summary>
        /// Gets or sets the install service order completed.
        /// </summary>
        /// <value>
        /// The install service order completed.
        /// </value>
        public DateTime? InstallServiceOrderCompleted { get; set; }
    }
}
