using System;
using System.Collections.Generic;

/// <summary>
/// 2020-09-22
/// </summary>
namespace Common
{
    /// <summary>
    /// Can return to pool as <see cref="LinkedPool{T}"/> : <see cref="ObjectSingletone{T}"/>
    /// </summary>
    public abstract class LinkedPoolItem<TDerived> : ILinkedPoolItem<TDerived> where TDerived : LinkedPoolItem<TDerived>//, new()
    {
        protected static LinkedPool<TDerived> s_pool = ObjectSingletone<LinkedPool<TDerived>>.Instance;

        public abstract TDerived NextPoolItem { get; set; }

        public bool ClearReturn()
        {
            Clear();

            return s_pool.TryReturn((TDerived)this);
        }

        public abstract void Clear();
    }
}