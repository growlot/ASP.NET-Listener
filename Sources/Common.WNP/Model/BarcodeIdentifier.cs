//-----------------------------------------------------------------------
// <copyright file="BarcodeIdentifier.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Represents composite key for barcode table
    /// </summary>
    [Serializable]
    public class BarcodeIdentifier
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the barcode identifier.
        /// </summary>
        /// <value>
        /// The barcode identifier.
        /// </value>
        public string LookupCode { get; set; }

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

            BarcodeIdentifier typedObject = obj as BarcodeIdentifier;
            if (typedObject == null)
            {
                return false;
            }

            if (this.Owner.Equals(typedObject.Owner) && this.LookupCode == typedObject.LookupCode)
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
            string description = (this.Owner == null) ? string.Empty : this.Owner.Description;

            return (description + "|" + this.LookupCode).GetHashCode();
        }
    }
}
