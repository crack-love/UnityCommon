using System;
using System.Collections.Generic;

/// <summary>
/// 2020-09-22
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Returning <see cref="SingletoneBase{T}"/> typed <see cref="LinkedPoolCallback{T}"/> when <see cref="GC"/>. Item is <see cref="ILinkedPoolItem{T}"/>
    /// </summary>
    public abstract class LinkedPoolItemGC<TDerived> : ILinkedPoolItemCallback<TDerived> where TDerived : LinkedPoolItemGC<TDerived>//, new()
    {
        protected static LinkedPoolCallback<TDerived> s_pool = Singletone<LinkedPoolCallback<TDerived>>.Instance;

        bool m_isOutsideOfPool = true;

        public abstract TDerived NextPoolItem { get; set; }

        ~LinkedPoolItemGC()
        {
            if (m_isOutsideOfPool)
            {
                m_isOutsideOfPool = false;

                Clear();

                if (s_pool.TryReturn((TDerived)this))
                {
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        public void ClearReturn()
        {
            Clear();

            s_pool.TryReturn((TDerived)this);
        }

        public abstract void Clear();

        void IPoolItemCallback.OnEnpool()
        {
            m_isOutsideOfPool = false;
        }

        void IPoolItemCallback.OnDepool()
        {
            m_isOutsideOfPool = true;
        }
    }
}