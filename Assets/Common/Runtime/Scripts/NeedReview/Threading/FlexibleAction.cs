using UnityEngine;
using UnityCommon;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

/// <summary>
/// 2020-10-05 월 오후 5:43:27, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    [StructLayout(LayoutKind.Auto)]
    public struct FlexibleAction
    {
        FlexibleActionNode m_root;
        int m_mutex;

        public FlexibleAction(Action action)
        {
            m_root = FlexibleActionNode.Create(action);
            m_mutex = 0;
        }

        public static implicit operator FlexibleAction(Action action)
        {
            return new FlexibleAction(action);
        }

        public static FlexibleAction operator +(FlexibleAction src, Action action)
        {
            src.Add(action);

            return src;
        }

        public static FlexibleAction operator -(FlexibleAction src, Action action)
        {
            src.Remove(action);

            return src;
        }

        public void Invoke()
        {
            m_root?.Invoke();
        }

        /// <summary>
        /// Exchange and Invoke previous actions
        /// </summary>
        public void InvokeExchange(Action exchange)
        {
            FlexibleActionNode prev;

            // exchange
            Interlock.Enter(ref m_mutex);
            try
            {
                prev = m_root;
                m_root = FlexibleActionNode.Create(exchange);
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }

            // Invoke
            if (prev != null)
            {
                prev.Invoke();
                prev.ClearReturn();
            }
        }

        public void Add(Action action)
        {
            if (action == null) return;

            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root == null)
                {
                    m_root = FlexibleActionNode.Create(action);
                }
                else
                {
                    m_root.Add(action);
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        public void Remove(Action action)
        {
            if (action == null) return;

            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root != null)
                {
                    m_root.Remove(action);
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        public void Set(Action action)
        {
            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root != null)
                {
                    m_root.Clear();

                    if (action != null)
                    {
                        m_root.Add(action);
                    }
                }
                else
                {
                    m_root = FlexibleActionNode.Create(action);
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        /// <summary>
        /// Set if root is not same with comparison
        /// </summary>
        public bool ExclusiveSet(Action action, Action comparison)
        {
            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root == null)
                {
                    if (comparison != null)
                    {
                        m_root = FlexibleActionNode.Create(action);

                        return true;
                    }
                }
                else if (!m_root.BaseEquals(comparison))
                {
                    m_root.Clear();

                    if (action != null)
                    {
                        m_root.Add(action);
                    }

                    return true;
                }

                return false;
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        /// <summary>
        /// Compare with root action
        /// </summary>
        public bool BaseEquals(Action comparison)
        {
            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root == null)
                {
                    return comparison == null;
                }
                else
                {
                    return m_root.BaseEquals(comparison);
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        /// <summary>
        /// Contains action
        /// </summary>
        public bool Contains(Action action)
        {
            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root == null)
                {
                    return action == null;
                }
                else
                {
                    return m_root.Contains(action);
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        public void Clear()
        {
            Interlock.Enter(ref m_mutex);
            try
            {
                if (m_root != null)
                {
                    m_root.ClearReturn();
                    m_root = null;
                }
            }
            finally
            {
                Interlock.Exit(ref m_mutex);
            }
        }

        class FlexibleActionNode : LinkedPoolItemGC<FlexibleActionNode>
        {
            FlexibleActionNode m_next;
            Action m_action;

            public override FlexibleActionNode NextPoolItem
            {
                get => m_next;
                set => m_next = value;
            }

            public static FlexibleActionNode Create(Action action)
            {
                if (action == null)
                {
                    return null;
                }

                if (!s_pool.TryGet(out var res))
                {
                    res = new FlexibleActionNode();
                }

                res.m_action = action;
                res.m_next = null;

                return res;
            }

            public void Add(Action action)
            {
                Check.Null(action);

                if (m_action == null)
                {
                    m_action = action;
                }
                else if (m_next != null)
                {
                    m_next.Add(action);
                }
                else
                {
                    m_next = Create(action);
                }
            }

            public void Remove(Action action)
            {
                Check.Null(action);

                if (m_action == action)
                {
                    m_action = null;
                }
                else if (m_next != null)
                {
                    m_next.Remove(action);
                }
            }

            public void Invoke()
            {
                // action can be null whereas next is not null
                m_action?.Invoke();
                m_next?.Invoke();
            }

            public bool Contains(Action action)
            {
                Check.Null(action);

                if (m_action == action)
                {
                    return true;
                }
                else if (m_next != null)
                {
                    return m_next.Contains(action);
                }
                else
                {
                    return false;
                }
            }

            public bool BaseEquals(Action src)
            {
                return m_action == src;
            }

            public override void Clear()
            {
                m_action = null;

                if (m_next != null)
                {
                    m_next.ClearReturn();
                    m_next = null;
                }
            }
        }
    }
}