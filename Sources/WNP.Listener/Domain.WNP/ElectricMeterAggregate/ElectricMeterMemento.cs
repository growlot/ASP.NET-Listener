// <copyright file="ElectricMeterMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.ElectricMeterAggregate
{
    using System.Collections.Generic;

    /// <summary>
    /// Memento class for electric meter aggregate root
    /// </summary>
    public class ElectricMeterMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeterMemento" /> class.
        /// </summary>
        /// <param name="equipmentNumber">The workstation name.</param>
        /// <param name="readings">The list of readings.</param>
        public ElectricMeterMemento(
            string equipmentNumber,
            IEnumerable<IMemento> readings)
        {
            this.EquipmentNumber = equipmentNumber;
            this.Readings = readings == null ? new List<ElectricMeterReadingMemento>() : readings;
        }

        internal string EquipmentNumber { get; private set; }

        internal IEnumerable<IMemento> Readings { get; private set; }
    }
}
