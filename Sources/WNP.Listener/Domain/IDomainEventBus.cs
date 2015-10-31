// <copyright file="IDomainEventBus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
namespace AMSLLC.Listener.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for domain event bus.
    /// </summary>
    /// <remarks>
    /// A mechanism for publishing domain events and subscribing handlers to these events.
    /// The only behavior is publishing events and subscribing event handlers and saga handlers to events;
    /// the only state is what's required to track which handler have subscribed to which events.
    /// </remarks>
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

        /// <summary>
        /// Publishes the specified domain events in a parallel manner.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="domainEvents">The domain events.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">Domain events must be specified in order to publish it.</exception>
        Task PublishBulk<TEvent>(ICollection<TEvent> domainEvents)
            where TEvent : IDomainEvent;
    }
}
