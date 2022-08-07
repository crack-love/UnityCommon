using System;
using UnityEngine;

/// <summary>
/// 2020-09-20 일 오후 9:32:51, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Callback OnDepool and OnEnpool if target is IPoolItemCallback
    /// </summary>
    public class ArrayedPoolCallback<T> : ArrayedPool<T> where T : IPoolItemCallback
    {
        public new bool TryGet(out T value)
        {
            if (base.TryGet(out value))
            {
                value.OnDepool();

                return true;
            }

            return false;
        }

        public new bool TryReturn(in T value)
        {
            if (base.TryReturn(value))
            {
                value.OnEnpool();

                return true;
            }

            return false;
        }
    }
}