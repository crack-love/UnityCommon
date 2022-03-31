using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-09-22 화 오후 8:13:31, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    class LoopableFunction : ArrayedPoolItem<LoopableFunction>, ILoopable, IEquatable<LoopableFunction>
    {
        Func<bool> m_func;

        public Func<bool> Func
        {
            get => m_func;
            set => m_func = value;
        }

        public static LoopableFunction Create(Func<bool> func)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new LoopableFunction();
            }

            res.m_func = func;

            return res;
        }

        public bool MoveNext()
        {
            try
            {
                return Func.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);

                return false;
            }
        }

        public override void Clear()
        {
            m_func = null;
        }

        public bool Equals(LoopableFunction other)
        {
            return other.m_func == m_func;
        }
    }
}