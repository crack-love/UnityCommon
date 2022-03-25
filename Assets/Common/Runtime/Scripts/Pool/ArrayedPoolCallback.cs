using UnityEngine;
using UnityCommon;
using System;
using System.Collections;

/// <summary>
/// 2020-09-20 일 오후 9:32:51, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Callback OnDepool and OnEnpool if target is IPoolItemCallback
    /// </summary>
    public class ArrayedPoolCallback<T> : ArrayedPool<T>, IPool
    {
        public new bool TryGet(out T value)
        {
            if (base.TryGet(out value) && value is IPoolItemCallback convert)
            {
                convert.OnDepool();

                return true;
            }

            return false;
        }

        public new bool TryReturn(in T value)
        {
            if (base.TryReturn(value) && value is IPoolItemCallback convert)
            {
                try
                {
                    convert.OnEnpool();
                }
                catch(Exception e)
                {
                    base.TryGet(out T pop);

                    Debug.LogException(e);
                }

                return true;
            }

            return false;
        }
    }
}