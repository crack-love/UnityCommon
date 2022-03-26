
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UnityCommon
{
    [Serializable]
    public class SerializableHashSet<T> : ISet<T>, IReadOnlyCollection<T>, ISerializationCallbackReceiver
    {
        [SerializeField] List<T> m_values;

        HashSet<T> m_hashSet;

        public SerializableHashSet()
        {
            m_hashSet = new HashSet<T>();
        }

        public int Count
        {
            get => m_values.Count;
        }

        public IEqualityComparer<T> Comparer
        {
            get => m_hashSet.Comparer;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public void SetComparer(IEqualityComparer<T> comp)
        {
            // new set
            HashSet<T> set = new HashSet<T>(m_hashSet, comp);

            m_hashSet = set;
        }

        public bool Add(T item)
        {
            return m_hashSet.Add(item);
        }

        void ICollection<T>.Add(T item)
        {
            m_hashSet.Add(item);
        }

        public void Clear()
        {
            m_hashSet.Clear();
        }

        public bool Contains(T item)
        {
            return m_hashSet.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            CopyTo(array, 0, m_hashSet.Count);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, m_hashSet.Count);
        }

        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            foreach (var i in m_hashSet)
            {
                if (count-- <= 0)
                {
                    break;
                }

                array[arrayIndex++] = i;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            foreach(var i in other)
            {
                m_hashSet.Remove(i);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_hashSet.GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            m_values.Clear();

            foreach (var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    m_values.Add(i); // intersect item
                }
            }

            m_hashSet.Clear();

            foreach(var i in m_values)
            {
                m_hashSet.Add(i);
            }

            m_values.Clear();
        }

        /// <summary>
        /// proper set is kind of like > and has extra elements that the second set doesn't have.
        /// </summary>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            int subCount = 0;

            foreach(var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    ++subCount;
                }
            }

            return subCount > m_hashSet.Count;
        }

        /// <summary>
        /// proper set is kind of like > and has extra elements that the second set doesn't have.
        /// </summary>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            int otherCount = 0;
            int subCount = 0;

            foreach (var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    ++subCount;
                }

                ++otherCount;
            }

            return subCount > otherCount;
        }

        /// <summary>
        /// SuperSet/Subset is doing something like >=, so your set could have exactly the same elements that are in the set you're comparing to
        /// </summary>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            int subCount = 0;

            foreach (var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    ++subCount;
                }
            }

            return subCount >= m_hashSet.Count;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            int otherCount = 0;
            int subCount = 0;

            foreach (var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    ++subCount;
                }

                ++otherCount;
            }

            return subCount >= otherCount;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            foreach(var i in other)
            {
                if (m_hashSet.Contains(i))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(T item)
        {
            return m_hashSet.Remove(item);
        }

        public int RemoveWhere(Predicate<T> match)
        {
            int cnt = 0;

            foreach(var i in m_hashSet)
            {
                if (match(i))
                {
                    m_hashSet.Remove(i);
                    ++cnt;
                }
            }

            return cnt;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            foreach(var i in other)
            {
                if (!m_hashSet.Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            HashSet<T> res = new HashSet<T>(m_hashSet, m_hashSet.Comparer);

            foreach(var i in other)
            {
                if (!res.Remove(i))
                {
                    res.Add(i);
                }
            }

            m_hashSet = res;
        }

        public void TrimExcess()
        {
            m_hashSet.TrimExcess();
            m_values.TrimExcess();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach(var i in other)
            {
                m_hashSet.Add(i);
            }
        }

        public void OnBeforeSerialize()
        {
            m_values.Clear();

            foreach(var i in m_hashSet)
            {
                m_values.Add(i);
            }
        }

        public void OnAfterDeserialize()
        {
            int size = m_values.Count;
            for(int i = 0; i < size; ++i)
            {
                m_hashSet.Add(m_values[i]);
            }

            m_values.Clear();
        }
    }
}
  