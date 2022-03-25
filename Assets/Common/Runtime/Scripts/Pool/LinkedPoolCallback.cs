using System;
using System.Collections.Generic;

/// <summary>
/// 2020-06-12
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// T is <see cref="ILinkedPoolItemGC{T}"/>
    /// </summary>
    public class LinkedPoolCallback<T> : LinkedPool<T> where T : ILinkedPoolItemCallback<T>
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