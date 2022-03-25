#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;

/// <summary>
/// 2021-04-19 월 오후 6:35:40, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Generic List's Pool that returning on GC
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ListPool<T>
    {
        static int m_listTrimCondition = 1024;
        static int m_maxCount = 128;
        static Stack<ListPoolItem<T>> m_stack;

        /// <summary>
        /// TrimExcess Condition max capacity
        /// </summary>
        public static int TrimCondition
        {
            get => m_listTrimCondition;
            set => m_listTrimCondition = Mathf.Max(value, 1);
        }

        /// <summary>
        /// Max Holding Count
        /// </summary>
        public static int MaxCount
        {
            get => m_maxCount;
            set => m_maxCount = Mathf.Max(value, 1);
        }

        static ListPool()
        {
            m_stack = new Stack<ListPoolItem<T>>();
        }

        public static List<T> GetList()
        {
            ListPoolItem<T> res;

            if (m_stack.Count > 0)
            {
                res = m_stack.Pop();
            }
            else
            {
                res = new ListPoolItem<T>();
            }

            res.OnDepool();

            return res;
        }

        internal static void Return(ListPoolItem<T> src)
        {
            if (src == null)
            {
                return;
            }

            if (m_stack.Count >= m_maxCount)
            {
                src.Destroy();

                return;
            }

            if (src.Capacity > m_listTrimCondition)
            {
                src.TrimExcess();
            }

            m_stack.Push(src);

            src.OnEnpool();
        }

        internal static void Return(List<T> listPoolItemSrc)
        {
            if (listPoolItemSrc is ListPoolItem<T> convert)
            {
                Return(convert);
            }
        }
    }

}