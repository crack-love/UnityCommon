using System;
using System.Collections.Generic;

/// <summary>
/// 2021-04-19 월 오후 6:37:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// A List can return to pool
    /// </summary>
    public class ListPoolItem<T> : List<T>, IDisposable, IPoolItemCallback
    {
        [NonSerialized]
        bool m_isOutsideOfPool = true;

        ~ListPoolItem()
        {
            if (m_isOutsideOfPool)
            {
                m_isOutsideOfPool = false;

                if (ListPool<T>.Return(this))
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
                m_isOutsideOfPool = false;

                if (ListPool<T>.Return(this))
                {
                    Clear();
                }
            }

            return true;
        }

        void IDisposable.Dispose()
        {
            ClearReturn();
        }

        void IPoolItemCallback.OnDepool()
        {
            m_isOutsideOfPool = true;
        }

        void IPoolItemCallback.OnEnpool()
        {
            m_isOutsideOfPool = false;
        }
    }
}