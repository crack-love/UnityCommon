#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;
using System.Collections;
using System.Collections.Generic;
//using IEnumerator = System.Collections.IEnumerator;
//using IEnumerable = System.Collections.IEnumerable;

/// <summary>
/// 2021-04-06 화 오후 2:08:19, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    [Serializable]
    public class SerializableStack<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        const float ExtendCapacityMulti = 2;

        [SerializeField] T[] m_stack;
        [SerializeField] int m_last;

        object m_syncRoot;

        public int Count
        {
            get => m_last + 1;
        }

        public bool IsSynchronized => false;

        public object SyncRoot => m_syncRoot;

        public bool IsReadOnly => false;

        public SerializableStack()
        {
            m_last = -1;

            m_syncRoot = new object();

            m_stack = new T[1];
        }

        public void Clear()
        {
            for (int i = 0; i <= m_last; ++i)
            {
                m_stack[i] = default;
            }

            m_last = -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            //Check.Null(array);
            //Check.When(array.Length - arrayIndex < m_last + 1, "SerializableStack.CopyTo array length overflow");

            for (int i = 0; i <= m_last; ++i)
            {
                array[arrayIndex + i] = m_stack[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(m_stack, m_last);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(m_stack, m_last);
        }

        public T Peek()
        {
            //Check.When(m_last < 0, "SerializableStack.Peek OutOfIndex");

            return m_stack[m_last];
        }

        public T Pop()
        {
           // Check.When(m_last < 0, "SerializableStack.Pop OutOfIndex");

            var res = m_stack[m_last];

            m_stack[m_last] = default;
            m_last -= 1;

            return res;
        }

        public void Push(T item)
        {
            if (m_last + 1 >= m_stack.Length)
            {
                Resize(Mathf.CeilToInt(m_stack.Length * ExtendCapacityMulti));
            }

            m_stack[++m_last] = item;
        }

        void ICollection<T>.Add(T item)
        {
            Push(item);
        }

        public void TrimExcess()
        {
            var cnt = m_last + 1;

            if (cnt < m_stack.Length * 0.9f)
            {
                Resize(Math.Max(1, cnt));
            }
        }

        int IndexOf(T item)
        {
            if (item is IEquatable<T> convert)
            {
                for (int i = 0; i <= m_last; ++i)
                {
                    if (convert.Equals(m_stack[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }
            else if (typeof(T).IsClass)
            {
                for (int i = 0; i <= m_last; ++i)
                {
                    if (item.Equals(m_stack[i]))
                    {
                        return i;
                    }
                }

                return -1;
            }
            // calling equals struct boxing
            else
            {
               // Check.Throw(new Exception("Struct type is not equatable with self type : " + typeof(T).Name));

                return -1;
            }
        }


        void Resize(int newSize)
        {
            // Minimum size 1
            newSize = Mathf.Max(1, newSize);

            T[] newStack = new T[newSize];

            for (int i = 0; i <= m_last && i < newSize; ++i)
            {
                newStack[i] = m_stack[i];
            }

            m_stack = newStack;
        }

        public bool Remove(T item)
        {
            var idx = IndexOf(item);

            if (idx >= 0)
            {
                m_stack[idx] = default;

                for (int i = idx; i < m_last; ++i)
                {
                    m_stack[i] = m_stack[i + 1];
                }

                --m_last;

                return true;
            }

            return false;
        }

        public class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            T[] m_stack;
            int m_last;
            int m_curr;

            public Enumerator(T[] stack, int last)
            {
                m_stack = stack;
                m_last = last;
                m_curr = last + 1;

                Reset();
            }

            public T Current
            {
                get => m_stack[m_curr];
            }

            object IEnumerator.Current
            {
                get => m_stack[m_curr];
            }

            public void Dispose()
            {
                m_stack = null;
            }

            public bool MoveNext()
            {
                if (--m_curr >= 0)
                {
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                m_curr = m_last + 1;
            }
        }
    }

    [Serializable]
    public class IntStack : SerializableStack<int>
    {

    }

    [Serializable]
    public class FloatStack : SerializableStack<int>
    {

    }
}