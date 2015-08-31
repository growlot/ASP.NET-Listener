using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNP.Listener.Utilities
{
    public static class EnumerableExtensions
    {
        public static void Map<T>(this IEnumerable<T> series, Action<T> action)
        {
            foreach (T obj in series)
                action(obj);
        }
    }
}
