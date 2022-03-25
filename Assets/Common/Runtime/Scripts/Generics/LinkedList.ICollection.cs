using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Assertions;

/// <summary>
/// 2021-05-25 화 오후 7:58:07, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public partial class LinkedList<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
    {
        public int Count => m_count;
        object ICollection.SyncRoot => throw new Exception("Not Syncronized");
        bool ICollection.IsSynchronized => false;
        bool ICollection<T>.IsReadOnly => false;

        public bool Contains(T value)
        {
            return FindFirstInternal(value) != null;
        }

        public void CopyTo(T[] array, int index)
        {
            Assert.IsNotNull(array);

            var node = m_first.Next;

            for (int i = 0; i < m_count; ++i)
            {
                array[index + i] = node.Value;

                node = node.Next;
            }
        }

        public void CopyTo(Array array, int index)
        {
            Assert.IsNotNull(array);

            var node = m_first.Next;

            for (int i = 0; i < m_count; ++i)
            {
                array.SetValue(node.Value, index + i);

                node = node.Next;
            }
        }

        /// <summary>
        /// Remove first value node
        /// </summary>
        public bool Remove(T value)
        {
            // find node
            var node = FindFirstInternal(value);

            if (node != null)
            {
                RemoveInternal(node);

                return true;
            }

            return false;
        }

        public void Clear()
        {
            var curr = m_first.Next;

            while (curr != m_last)
            {
                var next = curr.Next;
                DestroyNode(curr);
                curr = next;
            }
            m_count = 0;

            // reset first last
            m_first.Next = m_last;
            m_last.Prev = m_first;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetValueEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetValueEnumerable().GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }
    }
}