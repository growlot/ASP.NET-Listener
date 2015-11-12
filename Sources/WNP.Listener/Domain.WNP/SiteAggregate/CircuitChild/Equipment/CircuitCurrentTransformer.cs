// <copyright file="CircuitCurrentTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Current transformer component in the circuit.
    /// </summary>
    public class CircuitCurrentTransformer : Entity<string>
    {
        private IList<IDomainEvent> events;

        /// <summary>
        /// When transformer has multiple taps, or windings, different ratios can be selected during installation.
        /// </summary>
        private decimal? selectedRatio;

        /// <summary>
        /// Current transformers ratio can be modified by looping wire through it's hole multiple times.
        /// </summary>
        private decimal? loopCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCurrentTransformer"/> class.
        /// </summary>
        public CircuitCurrentTransformer()
        {
            this.events = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCurrentTransformer"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public CircuitCurrentTransformer(IList<IDomainEvent> events)
        {
            this.events = events;
        }

        /// <summary>
        /// Activates the related entity by giving acces to aggregate root event list.
        /// </summary>
        /// <param name="aggregateRootEvents">The aggregate root events.</param>
        public void ActivateEvents(IList<IDomainEvent> aggregateRootEvents)
        {
            this.events = aggregateRootEvents;
        }

        /// <summary>
        /// Gets the current transformer multiplier.
        /// </summary>
        /// <returns>The current transformer multiplier</returns>
        /// <exception cref="InvalidOperationException">
        /// Can not calculate current transformer multiplier, because loop count is not set.
        /// or
        /// Can not calculate current transformer multiplier, because transformer ratio is not set.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a method becaues it can throw exceptions.")]
        public decimal GetMultiplier()
        {
            if (!this.loopCount.HasValue)
            {
                throw new InvalidOperationException("Can not calculate current transformer multiplier, because loop count is not set.");
            }

            if (!this.selectedRatio.HasValue)
            {
                throw new InvalidOperationException("Can not calculate current transformer multiplier, because transformer ratio is not set.");
            }

            return this.selectedRatio.Value * this.loopCount.Value;
        }

        /// <summary>
        /// Sets the selected ratio for current transformer.
        /// </summary>
        /// <param name="newSelectedRatio">The ratio selected during current transformer installation.</param>
        /// <exception cref="System.InvalidOperationException">Can not update set selected ratio, because entity doesn't belong to any aggregate root.</exception>
        public void SetSelectedRatio(decimal newSelectedRatio)
        {
            if (this.events == null)
            {
                throw new InvalidOperationException("Can not update set selected ratio, because entity doesn't belong to any aggregate root.");
            }

            if (newSelectedRatio != this.selectedRatio)
            {
                this.selectedRatio = newSelectedRatio;
                var selectedRatioUpdated = new TransformerSelectedRatioUpdatedEvent(EquipmentType.CurrentTransformer.Code, this.Id, this.selectedRatio);
                this.events.Add(selectedRatioUpdated);
            }
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var currentTransformerMemento = (CircuitCurrentTransformerMemento)memento;
            this.Id = currentTransformerMemento.Id;
            this.selectedRatio = currentTransformerMemento.SelectedRatio;
            this.loopCount = currentTransformerMemento.LoopCount;
        }
    }
}
