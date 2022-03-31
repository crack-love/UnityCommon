using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-09-28 월 오후 6:53:31, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class TaskCancellation : ArrayedPoolItemGC<TaskCancellation>, ITaskCancellation
    {
        bool m_isCanceled;

        public bool IsCanceled => m_isCanceled;

        public static TaskCancellation Create()
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new TaskCancellation();
            }

            return res;
        }

        private TaskCancellation()
        {

        }

        public override void Clear()
        {
            m_isCanceled = false;
        }

        public void Cancel()
        {
            m_isCanceled = true;
        }
    }
}