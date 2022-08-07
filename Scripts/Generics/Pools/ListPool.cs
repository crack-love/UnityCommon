/// <summary>
/// 2021-04-19 월 오후 6:35:40, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Generic List's Pool that returning on GC
    /// </summary>
    public static class ListPool<T>
    {
        static readonly ArrayedPoolCallback<ListPoolItem<T>> s_pool = new ArrayedPoolCallback<ListPoolItem<T>>();

        public static int Count => s_pool.Count;

        public static int MaxCapacity
        {
            get => s_pool.MaxCapacity;
            set => s_pool.MaxCapacity = value;
        }

        public static ListPoolItem<T> Get()
        {
            if (!s_pool.TryGet(out var res))
            {
                // create new item
                res = new ListPoolItem<T>();
                ((IPoolItemCallback)res).OnDepool();
            }

            return res;
        }

        public static bool Return(in ListPoolItem<T> value)
        {
            return s_pool.TryReturn(value);
        }
    }

}