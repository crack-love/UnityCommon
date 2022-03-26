#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;
using System.Collections;

/// <summary>
/// 2021-04-19 월 오후 10:20:40, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    // TrimExcess before Serialization
    [Serializable]
    public class SerializableList<T> : IList<T>, IReadOnlyList<T>, ISerializationCallbackReceiver
    {
        public const int MaxCapacity = 2048;

        [SerializeField] int m_count;
        [SerializeField] T[] m_array;

        public T this[int index]
        {
            get => m_array[index];
            set => m_array[index] = value;
        }

        public int Count => m_count;

        public bool IsReadOnly => false;

        public SerializableList()
        {
            m_array = new T[0];
            m_count = 0;
        }

        public void Add(T item)
        {
            // expand
            if (m_count >= m_array.Length)
            {
                ReSize(m_array.Length * 2);
            }

            m_array[m_count++] = item;
        }

        public void Clear()
        {
            for (int i = 0; i < m_count; ++i)
            {
                m_array[i] = default;
            }

            m_count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < m_count; ++i)
            {
                if (item.Equals(m_array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            index = Mathf.Min(index, m_count); // clamp to last index

            // expand
            if (m_count >= m_array.Length)
            {
                ReSize(m_array.Length * 2);
            }

            for (int i = index; i < m_count; ++i)
            {
                m_array[i + 1] = m_array[i];
            }

            m_array[index] = item;
            ++m_count;
        }

        public bool Remove(T item)
        {
            var idx = IndexOf(item);

            if (idx >= 0)
            {
                RemoveAt(idx);
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            int size = m_count - 1;

            m_array[index] = default;

            for (int i = index; i < size; ++i)
            {
                m_array[i] = m_array[i + 1];
            }
        }

        public void OnBeforeSerialize()
        {
            TrimExcess();
        }

        public void OnAfterDeserialize()
        {
            
        }

        public void TrimExcess()
        {
            if (m_count >= m_array.Length * 0.9)
            {
                ReSize(m_count);
            }
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < m_count; ++i)
            {
                array[arrayIndex + i] = m_array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        void ReSize(int newSize)
        {
            newSize = Mathf.Clamp(newSize, 1, MaxCapacity);
            if (newSize == m_array.Length) return;

            var newArray = new T[newSize];
            var copyCount = Mathf.Min(m_count, newSize);

            for (int i = 0; i < copyCount; ++i)
            {
                newArray[i] = m_array[i];
            }

            m_array = newArray;
        }

        public class Enumerator : IEnumerator<T>
        {
            SerializableList<T> m_src;
            int m_currentIndex;

            public T Current => m_src[m_currentIndex];

            object IEnumerator.Current => m_src[m_currentIndex];

            public Enumerator(SerializableList<T> src)
            {
                m_src = src;

                Reset();
            }

            public void Dispose()
            {
                m_src = null;
            }

            public bool MoveNext()
            {
                if (++m_currentIndex < m_src.m_count)
                {
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                m_currentIndex = -1;
            }
        }
    }
}