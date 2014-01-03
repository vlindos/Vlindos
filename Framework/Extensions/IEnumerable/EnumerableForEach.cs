using System;
using System.Collections.Generic;

namespace Vlindos.Common.Extensions.IEnumerable
{
    public static class EnumerableForEach
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var o in enumerable)
            {
                action(o);
            }
        }
    }
}