// <copyright file="ElectricMeterReading.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.ElectricMeterAggregate
{
    using System;

    /// <summary>
    /// Electric meter reading represents measurement taken by electric meter at some specific point in time.
    /// </summary>
    public class ElectricMeterReading : ValueObject<ElectricMeterReading>
    {
        private readonly string annunciator;
        private readonly char? eventFlag;
        private readonly string label;
        private readonly string value;
        private readonly string source;
        private readonly DateTime date;
        private readonly string user;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeterReading" /> class.
        /// </summary>
        /// <param name="annunciator">The annunciator.</param>
        /// <param name="occasion">The occasion.</param>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        /// <param name="date">The date.</param>
        /// <param name="user">The user.</param>
        public ElectricMeterReading(
            string annunciator,
            char? occasion,
            string label,
            string value,
            string source,
            DateTime date,
            string user)
        {
            if (occasion != null && occasion != 'F' && occasion != 'L' && occasion != 'I' && occasion != 'O')
            {
                throw new ArgumentOutOfRangeException(nameof(occasion), "Only F (As Found), L (As Left), I (In/Installed) or O (Out/Uninstalled) values are allowed.");
            }

            this.annunciator = annunciator;
            this.eventFlag = occasion;
            this.label = label;
            this.value = value;
            this.source = source;
            this.date = date;
            this.user = user;
        }

        /// <summary>
        /// Gets the annunciator.
        /// </summary>
        /// <value>
        /// The annunciator.
        /// </value>
        public string Annunciator
        {
            get
            {
                return this.annunciator;
            }
        }

        /// <summary>
        /// Gets occasion when reading was taken.
        /// </summary>
        /// <value>
        /// The ocation.
        /// </value>
        public char? Occasion
        {
            get
            {
                return this.eventFlag;
            }
        }

        /// <summary>
        /// Gets the label of the reading.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get
            {
                return this.label;
            }
        }

        /// <summary>
        /// Gets the value of the reading.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Gets the source from where reading came (usually applicatin window or some external system).
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get
            {
                return this.source;
            }
        }

        /// <summary>
        /// Gets the date when reading was recorded.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }

        /// <summary>
        /// Gets the user who recorded the reading.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User
        {
            get
            {
                return this.user;
            }
        }
    }
}
