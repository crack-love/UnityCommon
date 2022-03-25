#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2021-05-24 월 오후 10:54:18, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class ArrayedPool<T> : Singletone<ArrayedPool<T>>, IPool<T>
    {
        const int InitialSize = 4;

        T[] m_array;
        int m_count;
        int m_maxCapacity;

        public int Count => m_count;

        public int MaxCapacity
        {
            get => m_maxCapacity;
            set
            {
                value = value.Clamp(1, CommonPoolVariables.MaxCapacity);
                m_maxCapacity = value;
            }
        }

        public ArrayedPool()
        {
            m_mutex = new object();
            m_array = new T[InitialSize];
            m_maxCapacity = 512;
        }

        public bool TryGet(out T value)
        {
            lock (m_mutex)
            {
                // empty
                if (m_count <= 0)
                {
                    value = default;

                    return false;
                }
                else
                {
                    // pop item
                    var idx = --m_count;
                    value = m_array[idx];

                    // clean
                    m_array[idx] = default;
                }
            }

            return true;
        }

        public bool TryReturn(in T value)
        {
            lock (m_mutex)
            {
                // overflow
                if (m_count >= m_maxCapacity - 1)
                {
                    return false;
                }

                // increase size
                if (m_count == m_array.Length)
                {
                    Resize(m_array.Length * 2);
                }

                m_array[m_count++] = value;
            }

            return true;
        }

        void Resize(int newSize)
        {
            // min max clamp
            newSize = Mathf.Clamp(newSize, 1, m_maxCapacity);

            if (newSize != m_array.Length)
            {
                // alloc
                T[] m_newArray = new T[newSize];

                // copy
                if (m_array != null)
                {
                    Array.Copy(m_array, m_newArray, Mathf.Clamp(m_array.Length, 0, newSize));
                }

                m_array = m_newArray;
            }
        }
    }
}