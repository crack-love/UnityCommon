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
    public struct SwitchToTaskPoolAwaiter : IAwaitableAwaiter<SwitchToTaskPoolAwaiter>
    {
        public bool IsCompleted => false;

        public SwitchToTaskPoolAwaiter GetAwaiter()
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
                STask.Factory.StartNew(
                    continuation, 
                    System.Threading.CancellationToken.None,
                    System.Threading.Tasks.TaskCreationOptions.AttachedToParent,
                    System.Threading.Tasks.TaskScheduler.Default);
            }
        }
    }
}