using System;
using UnityEngine;

/// <summary>
/// 2020-06-13
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Returning pool when <see cref="GC"/>.
    /// using <see cref="Singletone{T}"/> typed <see cref="ArrayedPool{T}"/>
    /// </summary>
    public abstract class ArrayedPoolItemGC<TDerived> : IPoolItemCallback where TDerived : ArrayedPoolItemGC<TDerived>//, new()
    {
        protected static ArrayedPoolCallback<TDerived> s_pool = Singletone<ArrayedPoolCallback<TDerived>>.Instance;

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