// <copyright file="IncomingRuleMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Memento class for incomming rule value object.
    /// </summary>
    public class IncomingRuleMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncomingRuleMemento" /> class.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <param name="isAllowed">Specifies if this incomming rule is allowed.</param>
        /// <param name="equipmentStatus">The equipment status.</param>
        /// <param name="detailedStatus">The detailed status.</param>
        /// <param name="location">The location.</param>
        /// <param name="locationType">Type of the location.</param>
        /// <param name="message">The message.</param>
        public IncomingRuleMemento(
            string workflow,
            bool isAllowed,
            string equipmentStatus,
            string detailedStatus,
            IMemento location,
            string locationType,
            string message)
        {
            this.Workflow = workflow;
            this.IsAllowed = isAllowed;
            this.EquipmentStatus = equipmentStatus;
            this.DetailedStatus = detailedStatus;
            this.Location = location;
            this.LocationType = locationType;
            this.Message = message;
        }

        internal string Workflow { get; private set; }

        internal bool IsAllowed { get; private set; }

        internal string EquipmentStatus { get; private set; }

        internal string DetailedStatus { get; private set; }

        internal IMemento Location { get; private set; }

        internal string LocationType { get; private set; }

        internal string Message { get; private set; }
    }
}
