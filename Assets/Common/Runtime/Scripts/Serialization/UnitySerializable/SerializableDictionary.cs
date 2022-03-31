using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class SerializableDictionary<TK, TV> : Dictionary<TK, TV>, ISerializationCallbackReceiver
    {
        [Serializable]
        struct KeyValuePair
        {
            public TK Key;
            public TV Value;
        }

        [SerializeField] KeyValuePair[] m_serializedPairs;
        [SerializeField] bool m_deserializationFail;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (m_deserializationFail) return;

            var count = Count;

            // reallocate
            if (m_serializedPairs == null ||
                m_serializedPairs.Length != count)
            {
                m_serializedPairs = new KeyValuePair[count];
            }

            // set serializeds
            int idx = 0;
            foreach (var pair in this)
            {
                m_serializedPairs[idx].Key = pair.Key;
                m_serializedPairs[idx++].Value = pair.Value;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            m_deserializationFail = false;

            // add items
            int size = m_serializedPairs.Length;
            for (int i = 0; i < size; ++i)
            {
                if (ContainsKey(m_serializedPairs[i].Key))
                {
                    m_deserializationFail = true;
                }
                else
                {
                    Add(m_serializedPairs[i].Key, m_serializedPairs[i].Value);
                }
            }
        }
    }
}
