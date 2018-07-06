using System.Linq;
using System.Collections.Generic;

namespace PingDong.Linq
{
    public static class LinqExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
