//-----------------------------------------------------------------------
// <copyright file="BarcodeMeterIdComparer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Custom EqualityComparer for <see cref="MeterBarcode"/> class.
    /// Compares meter barcodes by identifier.
    /// </summary>
    public class BarcodeMeterIdComparer : IEqualityComparer<MeterBarcode>
    {
        /// <summary>
        /// Checks if meter barcode identifiers are equal.
        /// </summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>True if specified objects are equal. False if objects are different.</returns>
        public bool Equals(MeterBarcode x, MeterBarcode y)
        {
            // Check whether the objects are the same object.
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            // Check if one of the objects is null.
            if (x == null || y == null)
            {
                return false;
            }

            if (x.Owner.Id != y.Owner.Id || x.LookupCode != y.LookupCode)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a hash code for the meter barcode.
        /// </summary>
        /// <param name="obj">The meter barcode.</param>
        /// <returns>
        /// A hash code for meter barcode, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(MeterBarcode obj)
        {
            if (obj == null)
            {
                return string.Empty.GetHashCode();
            }

            int ownerId = (obj.Owner == null) ? -1 : obj.Owner.Id;

            return (ownerId.ToString(CultureInfo.InvariantCulture) + "|" + obj.LookupCode).GetHashCode();
        }
    }
}
