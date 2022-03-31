using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 2020-12-22 화 오후 6:45:33, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// IEnumerable to List (enumerate all)
        /// </summary>
        public static List<T> ToList<T>(this IEnumerable<T> self, List<T> buffer)
        {
            buffer.Clear();

            foreach (var v in self)
            {
                buffer.Add(v);
            }

            return buffer;
        }

        /// <summary>
        /// Convenience method on IEnumerable to allow passing a
        /// Comparison delegate to the OrderBy method
        /// </summary>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }
    }
}