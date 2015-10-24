// <copyright file="IDomainEventBus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
namespace AMSLLC.Listener.Domain
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for domain event bus.
    /// </summary>
    public interface IDomainEventBus
    {
        /// <summary>
        /// Subscribes the handler to specific domain event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="handler">The handler.</param>
        void Subscribe<TEvent>(Action<TEvent> handler)
                    where TEvent : IDomainEvent;

        /// <summary>
        /// Subscribes the asynchronous handler to specific domain event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="handler">The handler.</param>
        void SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
            where TEvent : IDomainEvent;

        /// <summary>
        /// Publishes the specified domain event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        void Publish<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent;

        /// <summary>
        /// Publishes the specified domain event asynchronously.
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>The empty task.</returns>
        Task[] PublishAsync<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent;
    }
}
