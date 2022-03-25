/// <summary>
/// 2020-09-20
/// </summary>
namespace Common
{
    public class LinkedListNode<T> : IRemovableNode<T>
    {
        LinkedList<T> m_list;
        LinkedListNode<T> m_next;
        LinkedListNode<T> m_prev;
        T m_value;

        public LinkedList<T> List
        {
            get => m_list;
            internal set => m_list = value;
        }

        public LinkedListNode<T> Next
        {
            get => m_next;
            internal set => m_next = value;
        }

        public LinkedListNode<T> Prev
        {
            get => m_prev;
            internal set => m_prev = value;
        }

        public T Value
        {
            get => m_value;
            set => m_value = value;
        }

        void IRemovableNode<T>.Remove()
        {
            m_list.Remove(this);
        }
    }
}