/*#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;

/// <summary>
/// 2021-04-09 금 오후 8:59:01, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace mvp6
{
    [Serializable]
    public class SerializableQueue<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        [SerializeField] T[] m_queue;
        [SerializeField] int m_nextEmpty;
        [SerializeField] int m_lastItem;

        public int Count
        {
            get
            {


                return m_nextEmpty
            }
        }

        public SerializableQueue()
        {

        }

        public SerializableQueue(IEnumerable<T> collection)
        {

        }

        public SerializableQueue(int capacity)
        {

        }

        public void Clear();
        public bool Contains(T item);
        public void CopyTo(T[] array, int arrayIndex);
        public T Dequeue();
        public void Enqueue(T item);
        public Enumerator GetEnumerator();
        public T Peek();
        public T[] ToArray();
        public void TrimExcess();

        public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            public T Current { get; }

            public void Dispose();
            public bool MoveNext();
        }
    }
}*/