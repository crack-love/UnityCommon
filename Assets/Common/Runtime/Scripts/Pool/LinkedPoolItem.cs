using System;
using System.Collections.Generic;

/// <summary>
/// 2020-09-22
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Returning pool.
    /// using <see cref="SingletoneBase{T}"/> typed <see cref="LinkedPool{T}"/>
    /// </summary>
    public abstract class LinkedPoolItem<TDerived> : ILinkedPoolItem<TDerived> where TDerived : LinkedPoolItem<TDerived>//, new()
    {
        protected static LinkedPool<TDerived> s_pool = Singletone<LinkedPool<TDerived>>.Instance;

        public abstract TDerived NextPoolItem { get; set; }

        public void ClearReturn()
        {
            Clear();

            s_pool.TryReturn((TDerived)this);
        }

        public abstract void Clear();
    }
}