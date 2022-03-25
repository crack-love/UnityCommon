using System;
using System.Collections.Generic;

/// <summary>
/// 2020-09-22
/// </summary>
namespace Common
{
    /// <summary>
    /// Can return to pool when <see cref="GC"/>
    /// toward <see cref="LinkedPoolCallback{T}"/> from <see cref="ObjectSingletone{T}"/>
    /// </summary>
    public abstract class LinkedPoolItemGC<TDerived> : ILinkedPoolItemCallback<TDerived> where TDerived : LinkedPoolItemGC<TDerived>//, new()
    {
        protected static LinkedPoolCallback<TDerived> s_pool = ObjectSingletone<LinkedPoolCallback<TDerived>>.Instance;

        [NonSerialized]
        bool m_isOutsideOfPool = true;

        public abstract TDerived NextPoolItem { get; set; }

        ~LinkedPoolItemGC()
        {
            if (m_isOutsideOfPool)
            {
                m_isOutsideOfPool = false;

                if (s_pool.TryReturn((TDerived)this))
                {
                    Clear();
                    GC.ReRegisterForFinalize(this);
                }
            }
        }

        public bool ClearReturn()
        {
            if (m_isOutsideOfPool)
            {
                // setting flag off
                // when pool capacity is full and flag is even ture,
                // there will be one more returning overhead
                m_isOutsideOfPool = false;

                if (s_pool.TryReturn((TDerived)this))
                {
                    Clear();
                    return true;
                }
            }

            return false;
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