// <copyright file="ElectricMeter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.ElectricMeterAggregate
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Electric meter is an electic device that measures electricity consumption. It must be tested regularly to meet meterology requirements.
    /// </summary>
    public class ElectricMeter : AggregateRoot<string>
    {
        private IList<ElectricMeterReading> readings = new List<ElectricMeterReading>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeter"/> class.
        /// </summary>
        public ElectricMeter()
        {
        }

        /// <summary>
        /// Adds the new reading for electric meter.
        /// </summary>
        /// <param name="newReading">The new reading.</param>
        /// <exception cref="System.InvalidOperationException">Can not add same reading to the meter twice.</exception>
        public void AddReading(ElectricMeterReading newReading)
        {
            if (newReading == null)
            {
                throw new ArgumentNullException(nameof(newReading), "Can not add reading for electric meter, beceause it is not specified.");
            }

            if (this.readings.Contains(newReading))
            {
                throw new InvalidOperationException("Can not add same reading to the meter twice.");
            }

            this.readings.Add(newReading);
            var newReadingAddedEvent = new ElectricMeterReadingAddedEvent(
                equipmentNumber: this.Id,
                annunciator: newReading.Annunciator,
                occasion: newReading.Occasion,
                label: newReading.Label,
                value: newReading.Value,
                source: newReading.Source,
                date: newReading.Date,
                user: newReading.User);
            this.Events.Add(newReadingAddedEvent);
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var electricMeterMemento = (ElectricMeterMemento)memento;
            this.Id = electricMeterMemento.EquipmentNumber;

            foreach (var item in electricMeterMemento.Readings)
            {
                var readingMemento = (ElectricMeterReadingMemento)item;
                var reading = new ElectricMeterReading(
                    annunciator: readingMemento.Annunciator,
                    occasion: readingMemento.Occasion,
                    label: readingMemento.Label,
                    value: readingMemento.Value,
                    source: readingMemento.Source,
                    date: readingMemento.Date,
                    user: readingMemento.User);
                this.readings.Add(reading);
            }
        }
    }
}
