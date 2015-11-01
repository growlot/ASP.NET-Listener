//-----------------------------------------------------------------------
// <copyright file="IDomainEventHandler{TEvent}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for all domain event handlers.
    /// </summary>
    /// <typeparam name="TEvent">The type of the domain event.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Suffix has an unambiguous meaning in the application")]
    public interface IDomainEventHandler<TEvent>
            where TEvent : IDomainEvent
        {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>
        /// The empty task.
        /// </returns>
        Task HandleAsync(TEvent domainEvent);
    }
}
