// <copyright file="ElectricMeterReadingMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.ElectricMeterAggregate
{
    using System;

    /// <summary>
    /// Memento class for electric meter reading value object.
    /// </summary>
    public class ElectricMeterReadingMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeterReadingMemento" /> class.
        /// </summary>
        /// <param name="annunciator">The annunciator.</param>
        /// <param name="occasion">The occasion.</param>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        /// <param name="date">The date.</param>
        /// <param name="user">The user.</param>
        public ElectricMeterReadingMemento(
            string annunciator,
            char? occasion,
            string label,
            string value,
            string source,
            DateTime date,
            string user)
        {
            this.Annunciator = annunciator;
            this.Occasion = occasion;
            this.Label = label;
            this.Value = value;
            this.Source = source;
            this.Date = date;
            this.User = user;
        }

        internal string Annunciator { get; private set; }

        internal char? Occasion { get; private set; }

        internal string Label { get; private set; }

        internal string Value { get; private set; }

        internal string Source { get; private set; }

        internal DateTime Date { get; private set; }

        internal string User { get; private set; }
    }
}
