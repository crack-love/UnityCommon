using System;
using UnityEngine;

/// <summary>
/// 2021-05-24 월 오후 10:54:18, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public class ArrayedPool<T> : IPool<T>
    {
        const int InitialSize = 16;
        const int InitialMaxCap = 512;

        T[] m_array;
        int m_count;
        int m_maxCapacity;

        public int Count => m_count;

        public int MaxCapacity
        {
            get => m_maxCapacity;
            set => m_maxCapacity = value;
        }

        public ArrayedPool()
        {
            m_array = new T[InitialSize];
            m_maxCapacity = InitialMaxCap;
        }

        public bool TryGet(out T value)
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

            return true;
        }

        public bool TryReturn(in T value)
        {
            // overflow
            if (m_count + 1 >= m_maxCapacity)
            {
                return false;
            }

            // increase size
            if (m_count == m_array.Length)
            {
                Resize(m_array.Length * 2);
            }

            m_array[m_count++] = value;

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
                    Array.Copy(m_array, m_newArray, Mathf.Min(m_array.Length, newSize));
                }

                m_array = m_newArray;
            }
        }
    }
}