using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
    {
        [SerializeField] T[] m_serialValues;
        [SerializeField] bool m_deSerialFail;

        //public SerializableHashSet() : base()
        //{

        //}

        public void OnBeforeSerialize()
        {
            if (m_deSerialFail) return;

            if (m_serialValues == null || m_serialValues.Length != Count)
            {
                m_serialValues = new T[Count];
            }

            int i = 0;
            foreach(var value in m_serialValues)
            {
                m_serialValues[i++] = value;
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            m_deSerialFail = false;

            // add items
            int size = m_serialValues.Length;
            for (int i = 0; i < size; ++i)
            {
                var v = m_serialValues[i];

                if (!Add(v))
                {
                    m_deSerialFail = true;
                }
            }
        }
    }
}