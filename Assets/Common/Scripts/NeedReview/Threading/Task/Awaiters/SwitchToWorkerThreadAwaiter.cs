#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;
using STask = System.Threading.Tasks.Task;

/// <summary>
/// 2021-05-25 화 오후 5:31:57, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public struct SwitchToWorkerThreadAwaiter : IAwaitableAwaiter<SwitchToWorkerThreadAwaiter>
    {
        WorkerThread m_workerThread;

        public bool IsCompleted => false;

        public SwitchToWorkerThreadAwaiter(WorkerThread workerThread)
        {
            m_workerThread = workerThread;
        }

        public void GetResult()
        {

        }

        public void OnCompleted(Action continuation)
        {
            if (continuation != null)
            {
                m_workerThread?.Enqueue(new WorkerThreadActionStruct(continuation));
            }
        }

        public SwitchToWorkerThreadAwaiter GetAwaiter()
        {
            return this;
        }
    }
}