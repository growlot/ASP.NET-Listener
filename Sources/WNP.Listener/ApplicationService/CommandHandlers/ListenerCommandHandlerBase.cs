﻿// <copyright file="ListenerCommandHandlerBase.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Base class for listener command handlers
    /// </summary>
    public abstract class ListenerCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerCommandHandlerBase" /> class.
        /// </summary>
        /// <param name="domainEventBus">The domain event bus.</param>
        protected ListenerCommandHandlerBase(IDomainEventBus domainEventBus)
        {
            this.DomainEventBus = domainEventBus;
        }

        /// <summary>
        /// Gets the domain event bus used by this command.
        /// </summary>
        /// <value>
        /// The domain event bus.
        /// </value>
        protected IDomainEventBus DomainEventBus { get; private set; }

        /// <summary>
        /// Publishes the events generated by agregate root.
        /// </summary>
        /// <param name="aggregateRoot">The aggreagate root.</param>
        /// <returns>The task with all published events.</returns>
        protected Task PublishEvents(IAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot), "Can not publish events if aggregate is not specified.");
            }

            var taskList = new List<Task>();
            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                taskList.Add(this.DomainEventBus.PublishAsync(domainEvent));
            }

            return Task.WhenAll(taskList);
        }
    }
}