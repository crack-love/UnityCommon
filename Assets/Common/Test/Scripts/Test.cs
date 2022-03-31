
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CommonEditor;
using Common;

namespace Test
{
    class Test : MonoBehaviour
    {
        [SerializeField] SerializableDictionary<int, float> dic;
        [SerializeField] SerializableHashSet<float> hash;

        private void OnEnable()
        {
            HashSet<float> s = new HashSet<float>();
            s.Add(0);
        }
    }
}