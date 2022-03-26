using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 2020-07-08 수 오후 7:00:16, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// Item index is fixed after remove
    /// </summary>
    [Serializable]
    public class FixedList<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        const int InitialSize = 8;
        const float ExtendMultiply = 2f;
        const string k_invalidAccess = "Accesing Empty Item";

        [SerializeField] ItemAndBool[] m_items;
        [SerializeField] IntStack m_emties;

        public FixedList()
        {
            m_items = new ItemAndBool[0];
            m_emties = new IntStack();
        }

        /// <summary>
        /// Actual Item Count
        /// </summary>
        public int Count => m_items.Length - m_emties.Count;

        public int Capacity => m_items.Length;

        public T this[int idx]
        {
            get
            {
                var item = m_items[idx];

                Assert.IsTrue(item.IsFilled, k_invalidAccess);

                return item.Value;
            }
            set
            {
                var item = m_items[idx];

                Assert.IsTrue(item.IsFilled, k_invalidAccess);

                item.Value = value;
            }
        }

        /// <returns>Index of item</returns>
        public int Add(T item)
        {
            int idx;

            if (m_emties.Count > 0)
            {
                idx = m_emties.Pop();
            }
            else
            {
                ExtendItemSize();

                idx = m_emties.Pop();
            }

            m_items[idx] = new ItemAndBool(item, true);

            return idx;
        }

        /// <returns>Removed Index</returns>
        public int Remove(T item)
        {
            var idx = Find(item);

            if (idx >= 0)
            {
                RemoveAt(idx);
            }

            return idx;
        }

        public void RemoveAt(int idx)
        {
            m_items[idx] = default;
            m_emties.Push(idx);
        }

        /// <summary>
        /// return -1 if not found
        /// </summary>
        public int Find(T src)
        {
            int size = m_items.Length;
            int remainCount = Count;

            for (int i = 0; i < size && remainCount > 0; ++i)
            {
                var item = m_items[i];

                if (item.IsFilled)
                {
                    if (item.Equals(src))
                    {
                        return i;
                    }
                    --remainCount;
                }
            }

            return -1;
        }

        public void Clear()
        {
            var size = m_items.Length;
            int remainCount = Count;

            for (int i = 0; i < size && remainCount > 0; ++i)
            {
                if (m_items[i].IsFilled)
                {
                    RemoveAt(i);
                    --remainCount;
                }
            }
        }

        public bool Contains(T item)
        {
            return Find(item) >= 0;
        }

        public IEnumerable<T> GetEnumerable()
        {
            int size = m_items.Length;
            for (int i = 0; i < size; ++i)
            {
                if (m_items[i].IsFilled)
                {
                    yield return m_items[i].Value;
                }
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int size = m_items.Length;
            int count = Count;

            for (int i = 0; i < size && count > 0; ++i)
            {
                if (m_items[i].IsFilled)
                {
                    array[arrayIndex++] = m_items[i].Value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item) >= 0;
        }

        void ExtendItemSize()
        {
            int oldSize = m_items.Length;
            int extSize = Mathf.CeilToInt(oldSize * ExtendMultiply);
            int newSize = Math.Max(extSize, InitialSize);

            ItemAndBool[] newItems = new ItemAndBool[newSize];

            for (int i = 0; i < oldSize; ++i)
            {
                newItems[i] = m_items[i];
            }
            for (int i = newSize - 1; i >= oldSize; --i)
            {
                m_emties.Push(i);
            }

            m_items = newItems;
        }

        /// <summary>
        /// Item Value and Bool flag
        /// </summary>
        [Serializable]
        struct ItemAndBool
        {
            public bool IsFilled;
            public T Value;

            public ItemAndBool(T value, bool isFilled)
            {
                Value = value;
                IsFilled = isFilled;
            }
        }
    }
}