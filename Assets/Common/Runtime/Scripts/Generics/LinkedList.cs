#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System;

/// <summary>
/// 2021-05-25 화 오후 7:58:07, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class LinkedList<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>, ISerializable
    {
        // First, Last node is sentinal node.
        LinkedListNode<T> m_first;
        LinkedListNode<T> m_last;
        object m_mutex;
        int m_count;

        /// <summary>
        /// Return null if empty
        /// </summary>
        public LinkedListNode<T> First
        {
            get
            {
                lock (m_mutex)
                {
                    if (m_count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return m_first.Next;
                    }
                }
            }
        }

        /// <summary>
        /// Return null if empty
        /// </summary>
        public LinkedListNode<T> Last
        {
            get
            {
                lock (m_mutex)
                {
                    if (m_count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return m_last.Previous;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                lock (m_mutex)
                {
                    return m_count;
                }
            }
        }

        /// <summary>
        /// Get Nodes Enumerator. Enumerator's Current is tolerant of modification even removing.
        /// but not allowed remove current's previous node
        /// </summary>
        public NodeEnumerator Nodes
        {
            get => new NodeEnumerator(m_first, m_last);
        }

        object ICollection.SyncRoot
        {
            get => m_mutex;
        }

        bool ICollection.IsSynchronized
        {
            get => true;
        }

        bool ICollection<T>.IsReadOnly
        {
            get => false;
        }

        public LinkedList()
        {
            m_mutex = new object();
            m_first = CreateNode(default);
            m_last = CreateNode(default);

            m_first.Next = m_last;
            m_last.Previous = m_first;
            m_first.List = m_last.List = this;
        }

        public LinkedList(IEnumerable<T> enumer)
        {
            AddRange(enumer);
        }

        // System Deserialization
        protected LinkedList(SerializationInfo info, StreamingContext context) : base()
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
            info.AddValue("m_count", m_count);

            var node = m_first;
            for (int i = 0; i < m_count; ++i)
            {
                node = node.Next;

                info.AddValue(i.ToString(), node.Value);
            }
        }

        LinkedListNode<T> CreateNode(T value)
        {
            return LinkedListNode<T>.Create(default);
        }

        void DisposeNode(LinkedListNode<T> node)
        {
            if (node != null)
            {
                node.List?.RemoveInternal(node);

                // node.ClearReturn();
            }
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            lock (m_mutex)
            {
                // create node
                var newNode = LinkedListNode<T>.Create(value);

                // link
                AddAfterInternal(m_first, newNode);

                return newNode;
            }
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            lock (m_mutex)
            {
                Check.Null(node);

                // link
                AddAfterInternal(m_first, node);
            }
        }

        public LinkedListNode<T> AddLast(T value)
        {
            lock (m_mutex)
            {
                // create node
                var newNode = LinkedListNode<T>.Create(value);

                // link
                AddBeforeInternal(m_last, newNode);

                return newNode;
            }
        }

        public void AddLast(LinkedListNode<T> node)
        {
            lock (m_mutex)
            {
                Check.Null(node);

                // link
                AddBeforeInternal(m_last, node);
            }
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            lock (m_mutex)
            {
                Check.Null(node);
                Check.When(node.List != this);

                var newNode = LinkedListNode<T>.Create(value);

                AddBeforeInternal(node, newNode);

                return newNode;
            }
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            lock (m_mutex)
            {
                Check.Null(node);
                Check.Null(newNode);
                Check.When(node.List != this);

                AddBeforeInternal(node, newNode);
            }
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            lock (m_mutex)
            {
                Check.Null(node);
                Check.When(node.List != this);

                var newNode = LinkedListNode<T>.Create(value);

                AddAfterInternal(node, newNode);

                return newNode;
            }
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            lock (m_mutex)
            {
                Check.Null(node);
                Check.Null(newNode);
                Check.When(node.List != this);

                AddAfterInternal(node, newNode);
            }
        }

        public void AddRange(IEnumerable<T> enumer)
        {
            lock (m_mutex)
            {
                foreach (var v in enumer)
                {
                    var node = CreateNode(v);

                    AddBeforeInternal(m_last, node);
                }
            }
        }

        /// <summary>
        /// node is not null, newNode either
        /// </summary>
        void AddBeforeInternal(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            // unlink newnode
            newNode.List?.RemoveInternal(newNode);

            // link node's prev with newNode
            var prev = node.Previous;
            if (prev != null)
            {
                prev.Next = newNode;
            }
            newNode.Previous = prev;

            // link node with newNode
            newNode.Next = node;
            node.Previous = newNode;

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
                next.Previous = newNode;
                newNode.Next = next;
            }

            // link node with newnode
            node.Next = newNode;
            newNode.Previous = node;

            newNode.List = this;

            // count
            IncreaseCount();
        }

        public void RemoveFirst()
        {
            lock (m_mutex)
            {
                if (m_count > 0)
                {
                    RemoveInternal(m_first.Next);
                }
            }
        }

        public void RemoveLast()
        {
            lock (m_mutex)
            {
                if (m_count > 0)
                {
                    RemoveInternal(m_last.Previous);
                }
            }
        }

        /// <summary>
        /// Remove first found value
        /// </summary>
        public bool Remove(T value)
        {
            lock (m_mutex)
            {
                // find node
                var node = FindFirstInternal(value);

                if (node != null)
                {
                    RemoveInternal(node);

                    return true;
                }

                return false;
            }
        }

        public void Remove(LinkedListNode<T> node)
        {
            lock (m_mutex)
            {
                RemoveInternal(node);
            }
        }

        void RemoveInternal(LinkedListNode<T> node)
        {
            if (node == null) return;
            if (node.List != this) return;

            // set node's neighbors
            var prev = node.Previous;
            var next = node.Next;
            if (prev != null)
            {
                prev.Next = next;
            }
            if (next != null)
            {
                next.Previous = prev;
            }

            // set node
            node.List = null;
            node.Previous = node.Next = null;

            // count
            DecreaseCount();
        }

        public bool Contains(T value)
        {
            lock (m_mutex)
            {
                return FindFirstInternal(value) != null;
            }
        }

        /// <summary>
        /// Find first value
        /// </summary>
        public LinkedListNode<T> Find(T value)
        {
            lock (m_mutex)
            {
                return FindFirstInternal(value);
            }
        }

        public LinkedListNode<T> FindLast(T value)
        {
            lock (m_mutex)
            {
                return FindLastInternal(value);
            }
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
            var curr = m_last.Previous;

            for (int i = 0; i < m_count; ++i)
            {
                if (curr.Value.Equals(value))
                {
                    return curr;
                }

                curr = curr.Previous;
            }

            return null;
        }

        public void CopyTo(T[] array, int index)
        {
            lock (m_mutex)
            {
                var node = m_first.Next;

                for (int i = 0; i < m_count; ++i)
                {
                    array[index + i] = node.Value;

                    node = node.Next;
                }
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            lock (m_mutex)
            {
                var node = m_first.Next;

                for (int i = 0; i < m_count; ++i)
                {
                    array.SetValue(node.Value, index + i);

                    node = node.Next;
                }
            }
        }

        public void Clear()
        {
            lock (m_mutex)
            {
                var curr = m_first.Next;

                for (int i = 0; i < m_count; ++i)
                {
                    var next = curr.Next;

                    curr.Clear();

                    curr = next;
                }

                m_count = 0;

                // reset first last
                m_first.Next = m_last;
                m_last.Previous = m_first;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_first, m_last);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(m_first, m_last);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(m_first, m_last);
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        void IncreaseCount()
        {
            m_count += 1;
        }

        void DecreaseCount()
        {
            m_count -= 1;
        }

        public struct Enumerator : IEnumerator<T>
        {
            NodeEnumerator m_nodeEnumer;

            internal Enumerator(LinkedListNode<T> first, LinkedListNode<T> last)
            {
                m_nodeEnumer = new NodeEnumerator(first, last);
            }

            public T Current => m_nodeEnumer.Current.Value;

            object IEnumerator.Current => m_nodeEnumer.Current.Value;

            public void Dispose()
            {
                m_nodeEnumer.Dispose();
            }

            public bool MoveNext()
            {
                return m_nodeEnumer.MoveNext();
            }

            public void Reset()
            {
                m_nodeEnumer.Reset();
            }
        }

        public struct NodeEnumerator : IEnumerator<LinkedListNode<T>>
        {
            LinkedListNode<T> m_current;
            LinkedListNode<T> m_prev; // store prev to make current removable
            LinkedListNode<T> m_last;

            public LinkedListNode<T> Current => m_current;

            object IEnumerator.Current => m_current;

            internal NodeEnumerator(LinkedListNode<T> first, LinkedListNode<T> last)
            {
                m_current = first;
                m_prev = null;
                m_last = last;
            }

            public NodeEnumerator GetEnumerator()
            {
                return this;
            }

            public void Dispose()
            {
                m_current = null;
                m_last = null;
            }

            public bool MoveNext()
            {
                // current is removed
                if (m_current.List == null)
                {
                    // get next from stored
                    m_current = m_prev.Next;
                }
                else
                {
                    // move current
                    m_current = m_current.Next;
                    m_prev = m_current.Previous;
                }

                return m_current != m_last;
            }

            public void Reset()
            {
                Check.Throw("Reset not avaliable");
            }
        }
    }
}