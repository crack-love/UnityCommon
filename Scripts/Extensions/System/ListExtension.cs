using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 2021-01-12 화 오후 4:51:37, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class ListExtension
    {
        /// <summary>
        /// Set Count to size. O(n)
        /// </summary>
        public static void SetSize<T>(this List<T> list, int size)
        {
            SetSize(list, size, default);
        }

        public static void SetSize<T>(this List<T> list, int size, T value)
        {
            while (list.Count < size)
            {
                list.Add(value);
            }
            while (list.Count > size)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        /// <summary>
        /// Sorts an IList<T> in place through comparison
        /// </summary>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            // Adapter does not copy the contents of IList. Instead,
            // it only creates an ArrayList wrapper around IList;
            // therefore, changes to the IList also affect the ArrayList.
            // The ArrayList class provides generic Reverse, BinarySearch and Sort methods.

            ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }

        /// <summary>
        /// Sorts in IList<T> in place through comparer
        /// </summary>
        public static void Sort<T>(this IList<T> list, Comparer<T> comparer)
        {
            ArrayList.Adapter((IList)list).Sort(comparer);
        }
    }
}