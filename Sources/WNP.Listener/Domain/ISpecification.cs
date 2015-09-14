//-----------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Defines the inerface for declaring specifications
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity identity.</typeparam>
    public interface ISpecification<TEntity, TId> where TEntity : IEntity<TId>
    {
        /// <summary>
        /// Evaluates the specification against an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if specification is satisfied for entity</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}
