using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-06-14 일 오후 2:34:39, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class WorkerThreadAction : ArrayedPoolItemGC<WorkerThreadAction>, IWorkerThreadAction
    {
        Action m_action;

        public static WorkerThreadAction Create(Action action)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new WorkerThreadAction()
                {
                    m_action = action,
                };
            }

            return res;
        }

        public void Execute()
        {
            m_action.Invoke();
        }

        public void OnExecuteFault(Exception e)
        {
            UnityContext.Post(e);
        }

        public override void Clear()
        {
            m_action = default;
        }
    }
}