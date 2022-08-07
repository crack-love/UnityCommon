/*#if UNITY_EDITOR
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
    /// Generic Pool that returning on GC
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class QueuePool<T>
    {
        static int m_trimExcessCount = 1024;
        static int m_maxCount = 128;
        static Stack<QueuePoolItem<T>> m_stack;

        /// <summary>
        /// TrimExcess Condition
        /// </summary>
        public static int TrimExcessCount
        {
            get => m_trimExcessCount;
            set => m_trimExcessCount = Mathf.Max(value, 1);
        }

        /// <summary>
        /// Max Holding Count
        /// </summary>
        public static int MaxCount
        {
            get => m_maxCount;
            set => m_maxCount = Mathf.Max(value, 1);
        }

        static QueuePool()
        {
            m_stack = new Stack<QueuePoolItem<T>>();
        }

        public static Queue<T> GetQueue()
        {
            QueuePoolItem<T> res;

            if (m_stack.Count > 0)
            {
                res = m_stack.Pop();
            }
            else
            {
                res = new QueuePoolItem<T>();
            }

            res.OnDepool();

            return res;
        }

        internal static void Return(QueuePoolItem<T> src)
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

            if (src.Count >= m_trimExcessCount)
            {
                src.TrimExcess();
            }

            m_stack.Push(src);

            src.OnEnpool();
        }

        internal static void Return(Queue<T> poolItemSrc)
        {
            if (poolItemSrc is QueuePoolItem<T> convert)
            {
                Return(convert);
            }
        }
    }

}*/