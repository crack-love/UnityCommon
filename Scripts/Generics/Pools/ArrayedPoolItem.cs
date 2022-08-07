/// <summary>
/// 2020-06-13
/// </summary>
namespace Common
{
    /// <summary>
    /// Can return to pool as <see cref="ArrayedPool{T}"/> : <see cref="ObjectSingletone{T}"/>
    /// </summary>
    public abstract class ArrayedPoolItem<TDerived> : IPoolItem where TDerived : ArrayedPoolItem<TDerived>
    {
        protected static ArrayedPool<TDerived> s_pool = ObjectSingletone<ArrayedPool<TDerived>>.Instance;

        public bool ClearReturn()
        {
            Clear();

            return s_pool.TryReturn((TDerived)this);
        }

        public abstract void Clear();
    }
}