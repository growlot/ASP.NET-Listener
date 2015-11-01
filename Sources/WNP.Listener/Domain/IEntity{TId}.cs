//-----------------------------------------------------------------------
// <copyright file="IEntity{TId}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Interface that ensures that all entities have the identity.
    /// </summary>
    /// <typeparam name="TId">The type of identity.</typeparam>
    public interface IEntity<TId> : IOriginator
    {
        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        TId Id { get; }
    }
}
