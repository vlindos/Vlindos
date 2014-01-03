using System;
using System.Collections.Generic;

namespace Vlindos.Common.Extensions.IEnumerable
{
    public static class EnumerableDoUntil
    {
        public static void DoUntil<T>(this IEnumerable<T> enumerable, Func<T, bool> action)
        {
            foreach (var o in enumerable)
            {
                if (action(o) == false)
                {
                    break;
                }
            }
        }
    }
}
