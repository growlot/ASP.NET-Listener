// //-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace System.Collections.Generic
{
    using System;
    using Diagnostics.Contracts;

    /// <summary>
    /// Defines custom extensions for <see cref="IEnumerable{T}"/> type
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Maps the enumerable series to specified action.
        /// </summary>
        /// <typeparam name="T">Type of elements in series.</typeparam>
        /// <param name="series">The series of elements.</param>
        /// <param name="action">The action to perform for each series element.</param>
        public static void Map<T>(this IEnumerable<T> series, Action<T> action)
        {
            Contract.Requires<ArgumentNullException>(series != null);
            Contract.Requires<ArgumentNullException>(action != null);

            foreach (T obj in series)
            {
                action(obj);
            }
        }
    }
}