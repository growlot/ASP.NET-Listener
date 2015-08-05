//-----------------------------------------------------------------------
// <copyright file="Owner.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing owner
    /// </summary>
    [Serializable]
    public class Owner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Owner"/> class.
        /// </summary>
        public Owner()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Owner"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Owner(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Owner typedObject = obj as Owner;
            if (typedObject == null)
            {
                return false;
            }

            if (this.Id == typedObject.Id && this.Description == typedObject.Description)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
