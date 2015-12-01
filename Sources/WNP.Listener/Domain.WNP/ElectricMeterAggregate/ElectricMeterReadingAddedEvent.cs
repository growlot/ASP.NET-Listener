// <copyright file="ElectricMeterReadingAddedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.ElectricMeterAggregate
{
    using System;

    /// <summary>
    /// The event that adds electric meter reading.
    /// </summary>
    public class ElectricMeterReadingAddedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeterReadingAddedEvent" /> class.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="annunciator">The annunciator.</param>
        /// <param name="occasion">The occasion.</param>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        /// <param name="date">The date.</param>
        /// <param name="user">The user.</param>
        public ElectricMeterReadingAddedEvent(
            string equipmentNumber,
            string annunciator,
            char? occasion,
            string label,
            string value,
            string source,
            DateTime date,
            string user)
        {
            this.EquipmentNumber = equipmentNumber;
            this.Annunciator = annunciator;
            this.Occasion = occasion;
            this.Label = label;
            this.Value = value;
            this.Source = source;
            this.Date = date;
            this.User = user;
        }

        /// <summary>
        /// Gets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; private set; }

        /// <summary>
        /// Gets the annunciator.
        /// </summary>
        /// <value>
        /// The annunciator.
        /// </value>
        public string Annunciator { get; private set; }

        /// <summary>
        /// Gets the occasion.
        /// </summary>
        /// <value>
        /// The occasion.
        /// </value>
        public char? Occasion { get; private set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; private set; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; private set; }
    }
}
