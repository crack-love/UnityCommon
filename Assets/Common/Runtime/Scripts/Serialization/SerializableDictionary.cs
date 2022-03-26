
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
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        // Serializeds
        [SerializeField] TKey[] m_serializedKeys;
        [SerializeField] TValue[] m_serializedValues;

#if UNITY_EDITOR
        [SerializeField] bool m_adding;
        [SerializeField] TKey m_addingKey;
        [SerializeField] TValue m_addingValue;
#endif

        Dictionary<TKey, TValue> m_dic;

        public TValue this[TKey key]
        {
            get
            {
                return m_dic[key];
            }
            set
            {
                m_dic[key] = value;
            }
        }

        public int Count
        {
            get => m_dic.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get => m_dic.Keys;
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get => m_dic.Values;
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get => m_dic.Keys;
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get => m_dic.Values;
        }

        public SerializableDictionary()
        {
            m_dic = new Dictionary<TKey, TValue>();
        }

        public void OnAfterDeserialize()
        {
            m_dic.Clear();

            var size = m_serializedKeys.Length;
            for (int i = 0; i < size; ++i)
            {
                m_dic.Add(m_serializedKeys[i], m_serializedValues[i]);
            }

#if UNITY_EDITOR
            if (m_adding)
            {
                m_adding = false;
                m_dic.Add(m_addingKey, m_addingValue);
            }
#endif
        }

        public void OnBeforeSerialize()
        {
            var count = m_dic.Count;
            var length = m_serializedKeys?.Length ?? 0;

            // lenght is less than count or too many empty space
            if (length != count)
            {
                m_serializedKeys = new TKey[count];
                m_serializedValues = new TValue[count];
            }

            int i = 0;
            foreach (var pair in m_dic)
            {
                m_serializedKeys[i] = pair.Key;
                m_serializedValues[i++] = pair.Value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            m_dic.Add(key, value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (!m_dic.ContainsKey(key))
            {
                m_dic.Add(key, value);

                return true;
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            return m_dic.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_dic.TryGetValue(key, out value);
        }

        public bool ContainsKey(TKey key)
        {
            return m_dic.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return m_dic.ContainsValue(value);
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return m_dic.GetEnumerator();
        }

        public void Clear()
        {
            m_dic.Clear();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)m_dic).Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)m_dic).Remove(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)m_dic).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)m_dic).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_dic.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return m_dic.GetEnumerator();
        }
    }
}
  