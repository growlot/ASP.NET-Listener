// <copyright file="DeviceBatchProcessing.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.DeviceBatch
{
    /// <summary>
    /// Device batch processing
    /// </summary>
    public class DeviceBatchProcessing : AggregateRoot<int>
    {
        /// <summary>
        /// Split batch into separate transactions
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Empty while developing.")]
        public void Split()
        {
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void SetMemento(IMemento memento)
        {
            throw new System.NotImplementedException();
        }
    }
}