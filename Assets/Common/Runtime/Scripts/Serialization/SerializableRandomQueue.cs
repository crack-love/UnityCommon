using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;

/// <summary>
/// 2020-07-08 수 오후 7:00:16, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Auto Resize.
    /// Item order is not guaranteed.
    /// <typeparamref name="T"/> need to be Serializable
    /// </summary>
    [Serializable]
    public class SerializableRandomQueue<T>
    {
        const int s_InitialCapacity = 8;

        [SerializeField] T[] m_array;
        [SerializeField] int m_firstIndex;
        [SerializeField] int m_nextIndex;
        [SerializeField] int m_maxSize;
        [NonSerialized] object m_locker;

        public bool IsEmpty => m_firstIndex == m_nextIndex;
        public int Count => m_nextIndex - m_firstIndex;
        public int Length => m_array.Length;
        public int Space => m_firstIndex + (m_array.Length - m_nextIndex);
        public int MaxSize { get => m_maxSize; set => m_maxSize = value; }

        public SerializableRandomQueue()
        {
            if (m_array == null)
            {
                m_array = new T[s_InitialCapacity];
                m_firstIndex = 0;
                m_nextIndex = 0;
                m_maxSize = 1000;
            }

            m_locker = new object();
        }

        public void Enqueue(T v)
        {
            lock (m_locker)
            {
                ValidateSpace();

                if (Space > 0)
                {
                    if (m_firstIndex > 0)
                    {
                        m_array[m_firstIndex - 1] = v;
                        m_firstIndex -= 1;
                    }
                    else
                    {
                        m_array[m_nextIndex] = v;
                        m_nextIndex += 1;
                    }
                }
            }
        }

        public T Dequeue()
        {
            lock (m_locker)
            {
                if (Count > 0)
                {
                    var res = m_array[m_nextIndex - 1];
                    m_nextIndex -= 1;

                    return res;
                }

                return default;
            }
        }

        public bool TryEnqueue(T v)
        {
            lock (m_locker)
            {
                ValidateSpace();

                if (Space > 0)
                {
                    if (m_firstIndex > 0)
                    {
                        m_array[m_firstIndex - 1] = v;
                        m_firstIndex -= 1;
                    }
                    else
                    {
                        m_array[m_nextIndex] = v;
                        m_nextIndex += 1;
                    }

                    return true;
                }

                return false;
            }
        }

        public bool TryDequeue(out T v)
        {
            lock (m_locker)
            {
                if (Count > 0)
                {
                    v = m_array[m_nextIndex - 1];
                    m_nextIndex -= 1;

                    return true;
                }

                v = default;
                return false;
            }
        }

        void ValidateSpace()
        {
            var length = m_array.Length;
            var space = m_firstIndex + (length - m_nextIndex);

            // new size
            if (space <= 0 && length != m_maxSize)
            {
                int newSize = Mathf.Clamp(m_array.Length, m_maxSize, m_array.Length * 2);
                var newArray = new T[newSize];

                // copy
                Array.Copy(m_array, newArray, length);

                m_array = newArray;
            }
        }
    }
}