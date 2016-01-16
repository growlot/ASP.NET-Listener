// <copyright file="DomainUtilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Domain utilities collection
    /// </summary>
    public static class DomainUtilities
    {
        /// <summary>
        /// Add the range to the collection.
        /// </summary>
        /// <typeparam name="TRecord">The type of the collection entry.</typeparam>
        /// <param name="collection">The target collection.</param>
        /// <param name="source">The source collection.</param>
        public static void AddRange<TRecord>(
            this ICollection<TRecord> collection,
            IEnumerable<TRecord> source)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (source != null)
            {
                foreach (TRecord record in source)
                {
                    collection.Add(record);
                }
            }
        }
    }
}