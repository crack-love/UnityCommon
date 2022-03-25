using System;

/// <summary>
/// 2020-06-13
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Returning pool.
    /// using <see cref="Singletone{T}"/> typed <see cref="ArrayedPool{T}"/>
    /// </summary>
    public abstract class ArrayedPoolItem<TDerived> : IPoolItem where TDerived : ArrayedPoolItem<TDerived>
    {
        protected static ArrayedPool<TDerived> s_pool = Singletone<ArrayedPool<TDerived>>.Instance;

        public void ClearReturn()
        {
            Clear();

            s_pool.TryReturn((TDerived)this);
        }

        public abstract void Clear();
    }
}