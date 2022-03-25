using System;
using UnityEngine;

/// <summary>
/// 2020-09-20 일 오후 9:32:51, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// Callback OnDepool and OnEnpool if target is IPoolItemCallback
    /// </summary>
    public class ArrayedPoolCallback<T> : ArrayedPool<T>
    {
        public new bool TryGet(out T value)
        {
            if (base.TryGet(out value) && value is IPoolItemCallback callback)
            {
                callback.OnDepool();

                return true;
            }

            return false;
        }

        public new bool TryReturn(in T value)
        {
            if (base.TryReturn(value) && value is IPoolItemCallback callback)
            {
                try
                {
                    callback.OnEnpool();
                }
                catch (Exception e)
                {
                    // get rid off just pushed item
                    base.TryGet(out T pop);

                    Debug.LogException(e);
                }

                return true;
            }

            return false;
        }
    }
}