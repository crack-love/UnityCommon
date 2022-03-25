using System;
using UnityEngine;

/// <summary>
/// 2020-06-13
/// </summary>
namespace Common
{
    /// <summary>
    /// Can return to pool when <see cref="GC"/>
    /// toward <see cref="ArrayedPool{T}"/> from <see cref="ObjectSingletone{T}"/>
    /// </summary>
    public abstract class ArrayedPoolItemGC<TDerived> : IPoolItemCallback where TDerived : ArrayedPoolItemGC<TDerived>//, new()
    {
        protected static ArrayedPoolCallback<TDerived> s_pool = ObjectSingletone<ArrayedPoolCallback<TDerived>>.Instance;

        [NonSerialized]
        bool m_isOutsideOfPool = true;

        ~ArrayedPoolItemGC()
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

        public bool ClearReturn()
        {
            Clear();

            // setting flag off
            // when pool capacity is full and flag is even ture,
            // there will be one more returning overhead
            m_isOutsideOfPool = false;

            return s_pool.TryReturn((TDerived)this);
        }

        public void SetDestroyed()
        {
            m_isOutsideOfPool = false;
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