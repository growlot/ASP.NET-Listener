//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Interface that ensures that all entities have the identity.
    /// </summary>
    /// <typeparam name="T">The type of identity.</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        T Id { get; }
    }
}
