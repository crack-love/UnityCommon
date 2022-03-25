using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Assertions;

/// <summary>
/// 2021-05-25 화 오후 7:58:07, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public partial class LinkedList<T> : ISerializable
    {
        // First, Last node is sentinal node.
        readonly LinkedListNode<T> m_first;
        readonly LinkedListNode<T> m_last;
        int m_count;

        /// <summary> Return null if empty </summary>
        public LinkedListNode<T> First => m_count == 0 ? null : m_first.Next;
        /// <summary> Return null if empty </summary>
        public LinkedListNode<T> Last => m_count == 0 ? null : m_last.Prev;
        public IEnumerable<T> Values => GetValueEnumerable();
        public IEnumerable<IRemovableNode<T>> Nodes => GetNodeEnumerable();

        public LinkedList()
        {
            m_first = new LinkedListNode<T>();
            m_last = new LinkedListNode<T>();

            m_first.Next = m_last;
            m_last.Prev = m_first;
            m_first.List = m_last.List = this;
        }

        public LinkedList(IEnumerable<T> enumer) : this()
        {
            Assert.IsNotNull(enumer);

            AddRange(enumer);
        }

        // System Deserialization
        public LinkedList(SerializationInfo info, StreamingContext context) : this()
        {
            m_count = info.GetInt32("m_count");

            for (int i = 0; i < m_count; ++i)
            {
                var value = (T)info.GetValue(i.ToString(), typeof(T));
                var node = CreateNode(value);

                AddBeforeInternal(m_last, node);
            }
        }

        // System Serialization
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m_count", (Int32)m_count);

            var node = m_first;
            for (int i = 0; i < m_count; ++i)
            {
                node = node.Next;
                info.AddValue(i.ToString(), node.Value);
            }
        }

        protected virtual LinkedListNode<T> CreateNode(T value)
        {
            return new LinkedListNode<T>()
            {
                Value = value,
            };
        }

        protected virtual void DestroyNode(LinkedListNode<T> node)
        {
            node.Next = node.Prev = null;
            node.List = null;
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            var newNode = CreateNode(value);
            AddAfterInternal(m_first, newNode);
            return newNode;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            Assert.IsNotNull(node);

            AddAfterInternal(m_first, node);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            var newNode = CreateNode(value);
            AddBeforeInternal(m_last, newNode);
            return newNode;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            Assert.IsNotNull(node);

            AddBeforeInternal(m_last, node);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.List);

            var newNode = CreateNode(value);
            node.List.AddBeforeInternal(node, newNode);
            return newNode;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.List);

            node.List.AddBeforeInternal(node, newNode);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.List);

            var newNode = CreateNode(value);
            AddAfterInternal(node, newNode);
            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.List);

            node.List.AddAfterInternal(node, newNode);
        }

        public void AddRange(IEnumerable<T> enumer)
        {
            Assert.IsNotNull(enumer);

            foreach (var v in enumer)
            {
                var node = CreateNode(v);
                AddBeforeInternal(m_last, node);
            }
        }

        void AddBeforeInternal(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            // unlink newnode
            newNode.List?.RemoveInternal(newNode);

            // link node's prev with newNode
            var prev = node.Prev;
            if (prev != null)
            {
                prev.Next = newNode;
            }
            newNode.Prev = prev;

            // link node with newNode
            newNode.Next = node;
            node.Prev = newNode;

            newNode.List = this;

            // count
            IncreaseCount();
        }

        /// <summary>
        /// node is not null, newNode either
        /// </summary>
        void AddAfterInternal(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            // unlink newnode
            newNode.List?.RemoveInternal(newNode);

            // link node's next with newnode
            var next = node.Next;
            if (next != null)
            {
                next.Prev = newNode;
                newNode.Next = next;
            }

            // link node with newnode
            node.Next = newNode;
            newNode.Prev = node;

            newNode.List = this;

            // count
            IncreaseCount();
        }

        public void RemoveFirst()
        {
            if (m_count > 0)
            {
                RemoveInternal(m_first.Next);
            }
        }

        public void RemoveLast()
        {
            if (m_count > 0)
            {
                RemoveInternal(m_last.Prev);
            }
        }

        public void Remove(LinkedListNode<T> node)
        {
            Assert.IsNotNull(node);

            RemoveInternal(node);
        }

        void RemoveInternal(LinkedListNode<T> node)
        {
            // set node's neighbors
            var prev = node.Prev;
            var next = node.Next;
            if (prev != null)
            {
                prev.Next = next;
            }
            if (next != null)
            {
                next.Prev = prev;
            }

            // destroy
            DestroyNode(node);

            // count
            DecreaseCount();
        }

        public LinkedListNode<T> FindFirst(T value)
        {
            return FindFirstInternal(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return FindLastInternal(value);
        }

        LinkedListNode<T> FindFirstInternal(T value)
        {
            var curr = m_first.Next;

            for (int i = 0; i < m_count; ++i)
            {
                if (curr.Value.Equals(value))
                {
                    return curr;
                }

                curr = curr.Next;
            }

            return null;
        }

        LinkedListNode<T> FindLastInternal(T value)
        {
            var curr = m_last.Prev;

            for (int i = 0; i < m_count; ++i)
            {
                if (curr.Value.Equals(value))
                {
                    return curr;
                }

                curr = curr.Prev;
            }

            return null;
        }

        void IncreaseCount()
        {
            m_count += 1;
        }

        void DecreaseCount()
        {
            m_count -= 1;
        }

        IEnumerable<T> GetValueEnumerable()
        {
            LinkedListNode<T> curr = m_first;
            LinkedListNode<T> next;

            while (curr != null)
            {
                next = curr.Next;

                yield return curr.Value;

                curr = next;
            }
        }

        /// <summary>
        /// Node is tolerant for removing from list
        /// </summary>
        IEnumerable<IRemovableNode<T>> GetNodeEnumerable()
        {
            LinkedListNode<T> curr = m_first;
            LinkedListNode<T> next;

            while (curr != null)
            {
                next = curr.Next;

                yield return curr;

                curr = next;
            }
        }
    }
}