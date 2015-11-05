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

    /// <summary>
    /// bla
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
        /// The command handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, List<Action<ICommand>>> CommandHandlers =
            new Dictionary<Type, List<Action<ICommand>>>();

        /// <summary>
        /// The async command handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, List<Func<ICommand, Task>>> CommandAsyncHandlers =
            new Dictionary<Type, List<Func<ICommand, Task>>>();

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

        void ICommandBus.Publish<TCommand>(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command must be specified in order to publish it.");
            }

            if (CommandHandlers.ContainsKey(command.GetType()))
            {
                foreach (var handler in CommandHandlers[command.GetType()])
                {
                    handler(command);
                }
            }
        }

        async Task IDomainEventBus.PublishAsync<TEvent>(TEvent domainEvent)
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

                await Task.WhenAll(returnValue);
            }
        }

        Task ICommandBus.PublishAsync<TCommand>(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command must be specified in order to publish it.");
            }

            if (CommandAsyncHandlers.ContainsKey(command.GetType()))
            {
                var handlers = CommandAsyncHandlers[command.GetType()];
                Task[] returnValue = new Task[handlers.Count];
                for (int i = 0; i < handlers.Count; i++)
                {
                    returnValue[i] = handlers[i](command);
                }

                return Task.WhenAll(returnValue);
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

        void ICommandBus.Subscribe<TCommand>(Action<TCommand> handler)
        {
            if (!CommandHandlers.ContainsKey(typeof(TCommand)))
            {
                CommandHandlers.Add(typeof(TCommand), new List<Action<ICommand>>());
            }

            if (!CommandHandlers[typeof(TCommand)].Contains(x => handler((TCommand)x)))
            {
                CommandHandlers[typeof(TCommand)].Add(x => handler((TCommand)x));
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

        void ICommandBus.SubscribeAsync<TCommand>(Func<TCommand, Task> handler)
        {
            if (!CommandAsyncHandlers.ContainsKey(typeof(TCommand)))
            {
                CommandAsyncHandlers.Add(typeof(TCommand), new List<Func<ICommand, Task>>());
            }

            if (!CommandAsyncHandlers[typeof(TCommand)].Contains(x => handler((TCommand)x)))
            {
                CommandAsyncHandlers[typeof(TCommand)].Add(x => handler((TCommand)x));
            }
        }

        /// <summary>
        /// Publishes the specified domain events in a parallel manner.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="domainEvents">The domain events.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">Domain events must be specified in order to publish it.</exception>
        async Task IDomainEventBus.PublishBulk<TEvent>(ICollection<TEvent> domainEvents)
        {
            if (domainEvents == null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events must be specified in order to publish it.");
            }

            /*Parallel.ForEach(domainEvents, new ParallelOptions { MaxDegreeOfParallelism = parallelDegree }, @event =>
            {
                if (DomainEventHandlers.ContainsKey(@event.GetType()))
                {
                    foreach (var handler in DomainEventHandlers[@event.GetType()])
                    {
                        handler(@event);
                    }
                }
            });*/

            await Task.WhenAll(domainEvents.Select(d => ((IDomainEventBus)this).PublishAsync(d)));
        }
    }
}
