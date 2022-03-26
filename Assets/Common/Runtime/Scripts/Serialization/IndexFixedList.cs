using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;
using System.Collections;

/// <summary>
/// 2020-07-08 수 오후 7:00:16, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Item index is fixed on removing other items
    /// </summary>
    [Serializable]
    public class IndexFixedList<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        const int MinimumCapacity = 8;
        const float ExtendMultiply = 2f;

        [SerializeField] ItemBool[] m_items;
        [SerializeField] IntStack m_emptyIndexes;

        /// <summary>
        /// Count is not present last item index
        /// </summary>
        public int Count
        {
            get
            {
                return m_items.Length - m_emptyIndexes.Count;
            }
        }

        public int Length
        {
            get
            {
                return m_items.Length;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int idx]
        {
            get
            {
                var item = m_items[idx];

                Check.When(!item.IsFilled, "Accesing Empty Item");

                return item.Value;
            }

            set
            {
                var item = m_items[idx];

                Check.When(!item.IsFilled, "Accesing Empty Item");

                item.Value = value;
            }
        }

        // ctor
        public IndexFixedList()
        {
            m_items = new ItemBool[0];

            m_emptyIndexes = new IntStack();
        }

        public bool IsEmpty(int idx)
        {
            return !m_items[idx].IsFilled;
        }

        public bool IsFilled(int idx)
        {
            return m_items[idx].IsFilled;
        }

        public int Add(T item)
        {
            int idx;

            if (m_emptyIndexes.Count > 0)
            {
                idx = m_emptyIndexes.Pop();
            }
            else
            {
                ExtendItemSize();

                idx = m_emptyIndexes.Pop();
            }

            m_items[idx] = new ItemBool(item, true);

            return idx;
        }

        public int Remove(T item)
        {
            var idx = IndexOf(item);

            if (idx >= 0)
            {
                RemoveAt(idx);
            }

            return idx;
        }

        public void RemoveAt(int idx)
        {
            m_items[idx] = default;

            m_emptyIndexes.Push(idx);
        }

        public int IndexOf(T src)
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
            return IndexOf(item) >= 0;
        }

        public int CopyTo(T[] array, int arrayIndex)
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

            return count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(m_items);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item) >= 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(m_items);
        }

        void ExtendItemSize()
        {
            int oldSize = m_items.Length;
            int newSize = Mathf.CeilToInt(Math.Max(oldSize * ExtendMultiply, MinimumCapacity));

            ItemBool[] newItems = new ItemBool[newSize];

            for (int i = 0; i < oldSize; ++i)
            {
                newItems[i] = m_items[i];
            }

            for (int i = newSize - 1; i >= oldSize; --i)
            {
                m_emptyIndexes.Push(i);
            }

            m_items = newItems;
        }

        class Enumerator : IEnumerator<T>
        {
            ItemBool[] m_items;
            int m_currIdx;

            public Enumerator(ItemBool[] items)
            {
                m_items = items;
            }

            public T Current => m_items[m_currIdx].Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                m_items = null;
            }

            public bool MoveNext()
            {
                while (++m_currIdx < m_items.Length)
                {
                    var item = m_items[m_currIdx];

                    if (item.IsFilled)
                    {
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                m_currIdx = -1;
            }
        }

        /// <summary>
        /// Item Value and Bool isFilled flag
        /// </summary>
        [Serializable]
        struct ItemBool
        {
            public T Value;
            public bool IsFilled;

            public ItemBool(T value, bool isFilled)
            {
                Value = value;
                IsFilled = isFilled;
            }
        }

        [Serializable]
        class IntStack : SerializableStack<int>
        {

        }
    }
}