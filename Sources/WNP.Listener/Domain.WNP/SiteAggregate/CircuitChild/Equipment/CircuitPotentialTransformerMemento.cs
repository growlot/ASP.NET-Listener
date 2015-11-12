// <copyright file="CircuitPotentialTransformerMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Memento class for potential transformer connected to circuit
    /// </summary>
    public class CircuitPotentialTransformerMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitPotentialTransformerMemento" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="selectedRatio">The selected ratio.</param>
        public CircuitPotentialTransformerMemento(
            string id,
            decimal? selectedRatio)
        {
            this.Id = id;
            this.SelectedRatio = selectedRatio;
        }

        /// <summary>
        /// Gets the current transformers identifier.
        /// </summary>
        /// <value>
        /// The current transformers identifier.
        /// </value>
        internal string Id { get; private set; }

        /// <summary>
        /// Gets the selected ratio.
        /// </summary>
        /// <value>
        /// The selected ratio.
        /// </value>
        internal decimal? SelectedRatio { get; private set; }
    }
}
