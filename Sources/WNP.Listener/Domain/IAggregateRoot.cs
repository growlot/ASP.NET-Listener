//-----------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Marker interface to separate agregate roots
    /// </summary>
    /// <remarks>
    /// A persistent domain object that turns method calls into domain events.
    /// May contain references to other domain objects as part of a parent/child relationship.
    /// Stateful and behaviorful and guaranteed to be internally consistent.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is just a marker interface")]
    public interface IAggregateRoot
    {
    }
}
