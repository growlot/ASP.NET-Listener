﻿// <copyright file="AggregateRoot{TId}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Base class for all AgregateRoots
    /// </summary>
    /// <typeparam name="TId">The type of identity.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        /// <inheritdoc/>
        public ReadOnlyCollection<IDomainEvent> DomainEvents
        {
            get
            {
                return new ReadOnlyCollection<IDomainEvent>(this.Events);
            }
        }

        /// <summary>
        /// The list of domain events generated by this agregate root.
        /// </summary>
        protected IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();
    }
}
