#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2021-05-25 화 오후 5:31:57, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Does not switch if current is main
    /// </summary>
    struct SwitchToMainAwaiter : IAwaitableAwaiter<SwitchToMainAwaiter>
    {
        public bool IsCompleted => UnityContext.IsCurrentMainThread;

        public SwitchToMainAwaiter GetAwaiter()
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
                if (UnityContext.IsCurrentMainThread)
                {
                    continuation.Invoke();
                }
                else
                {
                    UnityContext.QueueYield(PlayerLoopType.Update, continuation);
                }
            }
        }
    }
}