// //-----------------------------------------------------------------------
// // <copyright file="IEnumerableExtensions.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Core
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void Map<T>(this IEnumerable<T> series, Action<T> action)
        {
            foreach (T obj in series)
                action(obj);
        }
    }
}