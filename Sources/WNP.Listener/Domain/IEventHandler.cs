// //-----------------------------------------------------------------------
// // <copyright file="IEventHandler.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for complex domain event handlers
    /// </summary>
    public interface IDomainHandler
    {
        /// <summary>
        /// Handles the specified event data.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <returns>Task.</returns>
        Task Handle(object eventData);
    }
}