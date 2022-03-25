using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-20
/// </summary>
namespace UnityCommon
{
    public class LinkedListNode<T> : LinkedPoolItemGC<LinkedListNode<T>>
    {
        LinkedList<T> m_list;
        LinkedListNode<T> m_next;
        LinkedListNode<T> m_prev;
        T m_value;

        public LinkedList<T> List
        {
            get => m_list;
            internal set => m_list = value;
        }

        public LinkedListNode<T> Next
        {
            get => m_next;
            internal set => m_next = value;
        }

        public LinkedListNode<T> Previous
        {
            get => m_prev;
            internal set => m_prev = value;
        }

        public T Value
        {
            get => m_value;
            set => m_value = value;
        }

        public override LinkedListNode<T> NextPoolItem
        {
            get => m_next;
            set => m_next = value;
        }

        public static LinkedListNode<T> Create(T value)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new LinkedListNode<T>();
            }

            res.m_list = null;
            res.m_value = value;
            res.m_next = res.m_prev = default;

            return res;
        }

        LinkedListNode()
        {

        }

        public override void Clear()
        {
            m_list = null;
            m_next = m_prev = null;
            m_value = default;
        }
    }
}