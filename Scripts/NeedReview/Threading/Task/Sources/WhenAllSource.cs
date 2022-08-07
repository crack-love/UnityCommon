using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

/// <summary>
/// 2020-10-16
/// </summary>
namespace UnityCommon
{
    class WhenAllSource : TaskSourceBase<WhenAllSource>, ILoopable
    {
        LinkedList<Task> m_tasks;

        public static WhenAllSource Create(IEnumerable<Task> tasks)
        {
            Check.Null(tasks);

            if (!s_pool.TryGet(out var res))
            {
                res = new WhenAllSource();
                res.m_tasks = new LinkedList<Task>();
            }

            res.m_tasks.AddRange(tasks);

            // start
            UnityContext.QueueUpdate(PlayerLoopType.Update, res);

            return res;
        }

        public bool MoveNext()
        {
            var node = m_tasks.First;

            while (node != null)
            {
                // completed
                if (node.Value.GetAwaiter().IsCompleted)
                {
                    m_tasks.Remove(node);

                    // next task
                    node = node.Next;
                }
                // yield
                else
                {
                    return true;
                }
            }

            // all completed
            SetComplete();

            return false;
        }

        protected override void OnClear()
        {
            m_tasks.Clear();
        }
    }
}