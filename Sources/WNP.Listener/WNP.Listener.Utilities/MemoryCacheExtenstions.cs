using System;
using System.Runtime.Caching;

namespace AMSLLC.Listener.Utilities
{
    public static class MemoryCacheExtenstions
    {
        public static T GetOrAddExisting<T>(this MemoryCache cache, string key, Func<T> valueFactory, DateTime? expiration = null)
        {
            var newValue = new Lazy<T>(valueFactory);
            var oldValue =
                cache.AddOrGetExisting(key, newValue,
                    new CacheItemPolicy() {AbsoluteExpiration = expiration ?? ObjectCache.InfiniteAbsoluteExpiration})
                    as Lazy<T>;

            try
            {
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