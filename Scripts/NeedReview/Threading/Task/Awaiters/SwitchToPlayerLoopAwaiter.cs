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
    public struct SwitchToPlayerLoopAwaiter : IAwaitableAwaiter<SwitchToPlayerLoopAwaiter>
    {
        PlayerLoopType m_destination;

        public bool IsCompleted => false;

        public SwitchToPlayerLoopAwaiter(PlayerLoopType destination)
        {
            m_destination = destination;
        }

        public void GetResult()
        {

        }

        public void OnCompleted(Action continuation)
        {
            if (continuation != null)
            {
                UnityContext.QueueYield(m_destination, continuation);
            }
        }

        public SwitchToPlayerLoopAwaiter GetAwaiter()
        {
            return this;
        }
    }
}