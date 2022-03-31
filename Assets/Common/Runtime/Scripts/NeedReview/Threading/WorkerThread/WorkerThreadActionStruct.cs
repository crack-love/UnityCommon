using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-06-14 일 오후 2:34:39, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public struct WorkerThreadActionStruct : IWorkerThreadAction
    {
        Action m_action;

        public WorkerThreadActionStruct(Action action)
        {
            m_action = action;
        }

        public void Execute()
        {
            m_action.Invoke();
        }

        public void OnExecuteFault(Exception e)
        {
            UnityContext.Post(e);
        }
    }
}