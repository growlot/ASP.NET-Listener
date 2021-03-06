﻿//-----------------------------------------------------------------------
// <copyright file="Entity{TId}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System;

    /// <summary>
    /// Base entity implementation that ensures identifier field is implemented
    /// and that entities can be compared by using this identifier field
    /// </summary>
    /// <typeparam name="TId">The type of identity</typeparam>
    public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        public virtual TId Id { get; protected set; }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Entity<TId> entity = obj as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            // If non persisted instance, use base hash code calculation
            if (this.Id.Equals(default(TId)))
            {
                return base.GetHashCode();
            }

            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            // If both are the same instance, return true.
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If both are different non persisted instances, return false
            if (this.Id.Equals(default(TId)) && other.Id.Equals(default(TId)))
            {
                return false;
            }

            // For persisted instances only compare identities
            return this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected abstract void SetMemento(IMemento memento);
    }
}
