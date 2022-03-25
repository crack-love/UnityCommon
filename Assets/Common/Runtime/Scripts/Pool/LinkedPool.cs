
/// <summary>
/// 2020-06-12
/// </summary>
namespace UnityCommon
{
    public class LinkedPool<T> : Singletone<LinkedPool<T>>, IPool<T> where T : ILinkedPoolItem<T>
    {
        T m_last;
        int m_count;
        int m_maxCapacity;

        public int Count => m_count;

        public int MaxCapacity
        {
            get => m_maxCapacity;
            set
            {
                value = value.Clamp(1, CommonPoolVariables.MaxCapacity);
                m_maxCapacity = value;
            }
        }

        public LinkedPool()
        {
            m_mutex = new object();
            m_maxCapacity = 512;
        }

        public bool TryGet(out T value)
        {
            lock (m_mutex)
            {
                if (m_last == null)
                {
                    value = default;
                    return false;
                }
                else
                {
                    // pop
                    value = m_last;
                    m_last = m_last.NextPoolItem;

                    // clear
                    value.NextPoolItem = default;

                    --m_count;

                    return true;
                }
            }
        }

        public bool TryReturn(in T value)
        {
            lock (m_mutex)
            {
                // Overflowed
                if (m_count > m_maxCapacity - 1)
                {
                    return false;
                }

                // push
                if (m_last != null)
                {
                    value.NextPoolItem = m_last;
                }

                m_last = value;
                ++m_count;

                return true;
            }
        }
    }
}