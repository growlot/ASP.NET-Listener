// <copyright file="InMemoryBus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApplicationService;
    using Domain;
    using Utilities;

    /// <summary>
    /// Implements <see cref="IDomainEventBus"/> and <see cref="ICommandBus"/> in memory.
    /// </summary>
    public sealed class InMemoryBus : IDomainEventBus, ICommandBus
    {
        /// <summary>
        /// The event handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, List<Action<IDomainEvent>>> DomainEventHandlers =
            new Dictionary<Type, List<Action<IDomainEvent>>>();

        /// <summary>
        /// The async event handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, List<Func<IDomainEvent, Task>>> DomainEventAsyncHandlers =
            new Dictionary<Type, List<Func<IDomainEvent, Task>>>();

        /// <summary>
        /// The async command handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, Func<ICommand, Task>> CommandAsyncHandlers =
            new Dictionary<Type, Func<ICommand, Task>>();

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            DomainEventHandlers.Clear();
            DomainEventAsyncHandlers.Clear();
            CommandAsyncHandlers.Clear();
        }

        void IDomainEventBus.Publish<TEvent>(TEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent), "Domain event must be specified in order to publish it.");
            }

            if (DomainEventHandlers.ContainsKey(domainEvent.GetType()))
            {
                foreach (var handler in DomainEventHandlers[domainEvent.GetType()])
                {
                    handler(domainEvent);
                }
            }
        }

        Task IDomainEventBus.PublishAsync<TEvent>(TEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent), "Domain event must be specified in order to publish it.");
            }

            if (DomainEventAsyncHandlers.ContainsKey(domainEvent.GetType()))
            {
                var handlers = DomainEventAsyncHandlers[domainEvent.GetType()];
                Task[] returnValue = new Task[handlers.Count];
                for (int i = 0; i < handlers.Count; i++)
                {
                    returnValue[i] = handlers[i](domainEvent);
                }

                return Task.WhenAll(returnValue);
            }

            return Task.CompletedTask;
        }

        Task ICommandBus.PublishAsync<TCommand>(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command must be specified in order to publish it.");
            }

            if (CommandAsyncHandlers.ContainsKey(command.GetType()))
            {
                return CommandAsyncHandlers[command.GetType()](command);
            }

            return Task.CompletedTask;
        }

        void IDomainEventBus.Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (!DomainEventHandlers.ContainsKey(typeof(TEvent)))
            {
                DomainEventHandlers.Add(typeof(TEvent), new List<Action<IDomainEvent>>());
            }

            if (!DomainEventHandlers[typeof(TEvent)].Contains(x => handler((TEvent)x)))
            {
                DomainEventHandlers[typeof(TEvent)].Add(x => handler((TEvent)x));
            }
        }

        void IDomainEventBus.SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
        {
            if (!DomainEventAsyncHandlers.ContainsKey(typeof(TEvent)))
            {
                DomainEventAsyncHandlers.Add(typeof(TEvent), new List<Func<IDomainEvent, Task>>());
            }

            if (!DomainEventAsyncHandlers[typeof(TEvent)].Contains(x => handler((TEvent)x)))
            {
                DomainEventAsyncHandlers[typeof(TEvent)].Add(x => handler((TEvent)x));
            }
        }

        void ICommandBus.Subscribe<TCommand>(Func<TCommand, Task> handler)
        {
            if (CommandAsyncHandlers.ContainsKey(typeof(TCommand)))
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Handler for command type {typeof(TCommand)} is alrady registered. Command can have only one handler."));
            }

            CommandAsyncHandlers.Add(typeof(TCommand), x => handler((TCommand)x));
        }

        /// <summary>
        /// Publishes the specified domain events in a parallel manner.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="domainEvents">The domain events.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">Domain events must be specified in order to publish it.</exception>
        Task IDomainEventBus.PublishBulk<TEvent>(ICollection<TEvent> domainEvents)
        {
            if (domainEvents == null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events must be specified in order to publish it.");
            }

            return Task.WhenAll(domainEvents.Select(d => ((IDomainEventBus)this).PublishAsync(d)));
        }
    }
}
