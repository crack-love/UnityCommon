using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-09-22 화 오후 8:13:31, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Loop Permanently
    /// </summary>
    class LoopableAction : ArrayedPoolItem<LoopableAction>, ILoopable, IEquatable<LoopableAction>
    {
        Action m_action;

        public Action Action
        {
            get => m_action;
            set => m_action = value;
        }

        public static LoopableAction Create(Action action)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new LoopableAction();
            }

            res.m_action = action;

            return res;
        }

        public bool MoveNext()
        {
            try
            {
                m_action?.Invoke();

                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);

                return false;
            }
        }

        public override void Clear()
        {
            m_action = null;
        }

        public bool Equals(LoopableAction other)
        {
            return other.m_action == m_action;
        }
    }
}