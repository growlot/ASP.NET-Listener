//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Advanced Metering Services LLC">
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
    /// <typeparam name="T">The type of identity</typeparam>
    public abstract class Entity<T> : IEntity<T>, IEquatable<Entity<T>>
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        public virtual T Id { get; protected set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Entity<T> entity = obj as Entity<T>;
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
            if (this.Id.Equals(default(T)))
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
        public bool Equals(Entity<T> other)
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
            if (this.Id.Equals(default(T)) && other.Id.Equals(default(T)))
            {
                return false;
            }

            // For persisted instances only compare identities
            return this.Id.Equals(other.Id);
        }
    }
}
