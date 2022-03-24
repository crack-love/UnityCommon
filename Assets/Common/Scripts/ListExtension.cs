using System.Collections.Generic;

namespace Common
{
    static class ListExtension
    {
        /// <summary>
        /// Set Count to size. O(n)
        /// </summary>
        public static void SetSize<T>(this List<T> list, int size)
        {
            while (list.Count < size)
            {
                list.Add(default);
            }
            while (list.Count > size)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}