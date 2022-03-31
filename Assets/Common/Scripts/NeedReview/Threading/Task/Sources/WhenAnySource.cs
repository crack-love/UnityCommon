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
    class WhenAnySource : TaskSourceBase<WhenAnySource>, ILoopable
    {
        LinkedList<Task> m_tasks;

        public static WhenAnySource Create(IEnumerable<Task> tasks)
        {
            Check.Null(tasks);

            if (!s_pool.TryGet(out var res))
            {
                res = new WhenAnySource();
                res.m_tasks = new LinkedList<Task>();
            }

            res.m_tasks.AddRange(tasks);

            // start
            UnityContext.QueueUpdate(PlayerLoopType.Update, res);

            return res;
        }

        public bool MoveNext()
        {
            // enumeration all
            foreach(var v in m_tasks)
            {
                if (v.GetAwaiter().IsCompleted)
                {
                    // complete
                    SetComplete();

                    return false;
                }
            }

            return true;
        }

        protected override void OnClear()
        {
            m_tasks.Clear();
        }
    }
}