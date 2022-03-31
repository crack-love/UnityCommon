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
    public struct SwitchToThreadPoolAwaiter : IAwaitableAwaiter<SwitchToThreadPoolAwaiter>
    {
        public bool IsCompleted => false;

        public SwitchToThreadPoolAwaiter GetAwaiter()
        {
            return this;
        }

        public void GetResult()
        {

        }

        public void OnCompleted(Action continuation)
        {
            if (continuation != null)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(
                    TaskDelegate.WaitCallbackActionInvoker,
                    continuation);
            }
        }
    }
}