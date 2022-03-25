#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;

/// <summary>
/// 2021-04-19 월 오후 6:37:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Initial state is setted outside of pool
    /// </summary>
    class ListPoolItem<T> : List<T>
    {
        bool m_isInsidePool = false;

        ~ListPoolItem()
        {
            if (!m_isInsidePool)
            {
                ListPool<T>.Return(this);

                GC.ReRegisterForFinalize(this);
            }
        }

        public void OnEnpool()
        {
            Clear();

            m_isInsidePool = true;
        }

        public void OnDepool()
        {
            m_isInsidePool = false;
        }

        public void Destroy()
        {
            m_isInsidePool = true;
        }
    }
}