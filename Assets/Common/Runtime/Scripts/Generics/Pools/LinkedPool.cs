/// <summary>
/// 2020-06-12
/// </summary>
namespace Common
{
    public class LinkedPool<T> : IPool<T> where T : ILinkedPoolItem<T>
    {
        const int InitialMaxCapacity = 512;

        T m_last;
        int m_count;
        int m_maxCapacity;

        public int Count => m_count;

        public int MaxCapacity
        {
            get => m_maxCapacity;
            set => m_maxCapacity = value;
        }

        public LinkedPool()
        {
            m_count = 0;
            m_maxCapacity = InitialMaxCapacity;
        }

        public bool TryGet(out T value)
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
                --m_count;

                // clear
                value.NextPoolItem = default;

                return true;
            }
        }

        public bool TryReturn(in T value)
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