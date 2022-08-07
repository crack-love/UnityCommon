#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;
using System.Threading;

/// <summary>
/// 2021-05-25 화 오후 6:38:12, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    static class TaskDelegate
    {
        public static readonly Action CompletedSentinalAction = CompletedSentinalActionMethod;
        public static readonly WaitCallback WaitCallbackActionInvoker = ActionInvoker;

        static void CompletedSentinalActionMethod() { }

        static void ActionInvoker(object action)
        {
            ((Action)action).Invoke();
        }
    }
}