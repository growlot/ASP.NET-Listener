// <copyright file="IncomingRule.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System;

    /// <summary>
    /// Incoming rule defines what state should equipment be in, in order for it to be loaded in this workstation.
    /// </summary>
    public class IncomingRule : ValueObject<IncomingRule>
    {
        private readonly string workflow;
        private readonly bool isAllowed;
        private readonly string equipmentStatus;
        private readonly string detailedStatus;
        private readonly string location;
        private readonly string locationType;
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="IncomingRule"/> class.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <param name="isAllowed">Specifies if this incomming rule is allowed.</param>
        /// <param name="equipmentStatus">The equipment status.</param>
        /// <param name="detailedStatus">The equipment detailed status.</param>
        /// <param name="location">The location.</param>
        /// <param name="locationType">Type of the location.</param>
        /// <param name="message">The message that is displayed in UI when equipment is loaded using this rule.</param>
        public IncomingRule(
            string workflow,
            bool isAllowed,
            string equipmentStatus,
            string detailedStatus,
            string location,
            string locationType,
            string message)
        {
            this.workflow = workflow;
            this.isAllowed = isAllowed;
            this.equipmentStatus = equipmentStatus;
            this.detailedStatus = detailedStatus;
            this.location = location;
            this.locationType = locationType;
            this.message = message;
        }

        /// <summary>
        /// Gets the workflow.
        /// </summary>
        /// <value>
        /// The workflow.
        /// </value>
        public string Workflow
        {
            get
            {
                return this.workflow;
            }
        }

        /// <summary>
        /// Gets a value indicating whether equipment matching this rule can be processed in this workstation.
        /// </summary>
        /// <value>
        /// <c>true</c> if equipment matching this rule can be processed in this workstation; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllowed
        {
            get
            {
                return this.isAllowed;
            }
        }

        /// <summary>
        /// Gets the equipment status.
        /// </summary>
        /// <value>
        /// The equipment status.
        /// </value>
        public string EquipmentStatus
        {
            get
            {
                return this.equipmentStatus;
            }
        }

        /// <summary>
        /// Gets the detailed equipment status.
        /// </summary>
        /// <value>
        /// The detailed equipment status.
        /// </value>
        public string DetailedStatus
        {
            get
            {
                return this.detailedStatus;
            }
        }

        /// <summary>
        /// Gets the equipment location.
        /// </summary>
        /// <value>
        /// The equpiment location.
        /// </value>
        public string Location
        {
            get
            {
                return this.location;
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
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
