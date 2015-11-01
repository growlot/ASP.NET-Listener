﻿//-----------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Marker interface to separate agregate roots
    /// </summary>
    /// <remarks>
    /// A persistent domain object that turns method calls into domain events.
    /// May contain references to other domain objects as part of a parent/child relationship.
    /// Stateful and behaviorful and guaranteed to be internally consistent.
    /// </remarks>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets the domain events generated by this AgregateRoot.
        /// </summary>
        /// <value>
        /// The domain events.
        /// </value>
        ReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
