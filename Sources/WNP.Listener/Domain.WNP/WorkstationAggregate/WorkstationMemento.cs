// <copyright file="WorkstationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System.Collections.Generic;

    /// <summary>
    /// Memento class for workstation aggregate root
    /// </summary>
    public class WorkstationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkstationMemento" /> class.
        /// </summary>
        /// <param name="name">The workstation name.</param>
        /// <param name="businessActions">The business actions.</param>
        /// <param name="incomingRules">The incoming rules.</param>
        public WorkstationMemento(
            string name,
            IEnumerable<IMemento> businessActions,
            IEnumerable<IMemento> incomingRules)
        {
            this.Name = name;
            this.BusinessActions = businessActions == null ? new List<BusinessActionMemento>() : businessActions;
            this.IncomingRules = incomingRules == null ? new List<IncomingRuleMemento>() : incomingRules;
        }

        internal string Name { get; private set; }

        internal IEnumerable<IMemento> BusinessActions { get; private set; }

        internal IEnumerable<IMemento> IncomingRules { get; private set; }
    }
}
