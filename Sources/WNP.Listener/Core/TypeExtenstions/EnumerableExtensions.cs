// //-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace System.Collections.Generic
{
    using System;

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
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static void Map<T>(this IEnumerable<T> series, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Can not perform action on elements if action is not specified.");
            }

            foreach (T obj in series)
            {
                action(obj);
            }
        }
    }
}