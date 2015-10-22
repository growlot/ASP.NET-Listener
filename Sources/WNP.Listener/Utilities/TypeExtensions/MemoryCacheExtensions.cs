// <copyright file="MemoryCacheExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System.Runtime.Caching
{
    using System;

    /// <summary>
    /// Defines custom extensions for <see cref="MemoryCache"/> type
    /// </summary>
    public static class MemoryCacheExtensions
    {
        /// <summary>
        /// Gets existing item from cache or retrieves value from valueFactor and stores it in cache.
        /// </summary>
        /// <typeparam name="T">The type of cache item.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key of cache item.</param>
        /// <param name="valueFactory">The value factory used to retrieve item if it's not found in cache.</param>
        /// <returns>The item.</returns>
        public static T GetOrAddExisting<T>(this MemoryCache cache, string key, Func<T> valueFactory)
        {
            return cache.GetOrAddExisting(key, valueFactory, null);
        }

       /// <summary>
       /// Gets existing item from cache or retrieves value from valueFactor and stores it in cache.
       /// </summary>
       /// <typeparam name="T">The type of cache item.</typeparam>
       /// <param name="cache">The cache.</param>
       /// <param name="key">The key of cache item.</param>
       /// <param name="valueFactory">The value factory used to retrieve item if it's not found in cache.</param>
       /// <param name="expiration">The cache item expiration time.</param>
       /// <returns>The item.</returns>
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "oldValue is validated to be not null")]
        public static T GetOrAddExisting<T>(this MemoryCache cache, string key, Func<T> valueFactory, DateTime? expiration)
        {
            // newValue will be calculated only if needed
            var newValue = new Lazy<T>(valueFactory);

            // insert will be done ony if needed. Is it really so???
            var oldValue =
                cache.AddOrGetExisting(
                    key,
                    newValue,
                    new CacheItemPolicy() { AbsoluteExpiration = expiration ?? ObjectCache.InfiniteAbsoluteExpiration }) as Lazy<T>;

            try
            {
                // initiates newValue calculation only if oldValue is null
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                cache.Remove(key);
                throw;
            }
        }
    }
}