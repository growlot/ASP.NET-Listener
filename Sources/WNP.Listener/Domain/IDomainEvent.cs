﻿//-----------------------------------------------------------------------
// <copyright file="IDomainEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Marker interface for all domain events.
    /// </summary>
    /// <remarks>
    /// A simple message that represents a change to the state of an aggregate.
    /// Just data with no behavior and often named in the past tense.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is a marker interface.")]
    public interface IDomainEvent
    {
    }
}