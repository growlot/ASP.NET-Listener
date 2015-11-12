// <copyright file="CircuitCurrentTransformerMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Memento class for electric meter connected to circuit
    /// </summary>
    public class CircuitCurrentTransformerMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCurrentTransformerMemento" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="selectedRatio">The selected ratio.</param>
        /// <param name="loopCount">The loop count.</param>
        public CircuitCurrentTransformerMemento(
            string id,
            decimal? selectedRatio,
            decimal? loopCount)
        {
            this.Id = id;
            this.SelectedRatio = selectedRatio;
            this.LoopCount = loopCount;
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

        /// <summary>
        /// Gets the loop count
        /// </summary>
        /// <value>
        /// The loop count.
        /// </value>
        internal decimal? LoopCount { get; private set; }
    }
}
