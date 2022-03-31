using UnityEngine;
using System.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Concurrent;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.LowLevel;
using System.Runtime.ExceptionServices;

/// <summary>
/// 2020-06-01 월 오후 6:08:04, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Provide SyncronizationContext of UnityEngine and PlayerLoop as Update
    /// </summary>
    public static partial class UnityContext
    {
        static SynchronizationContext m_context;
        static int m_mainThreadID;

        /// <summary>
        /// 쓰레드 ID 비교
        /// </summary>
        public static bool IsCurrentMainThread
        {
            get => Thread.CurrentThread.ManagedThreadId == m_mainThreadID;
        }

        static void InitializeContext()
        {
            m_context = SynchronizationContext.Current;
            m_mainThreadID = Thread.CurrentThread.ManagedThreadId;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void RuntimeInitialization()
        {
            InitializeContext();
            InitializePlayerLoopStructure();
            InitializePlayerLoopRegistrationRuntime();
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void EditorInitialization()
        {
            InitializeContext();
            InitializePlayerLoopStructure();
            InitializePlayerLoopRegistrationEditor();
        }
#endif

        /// <summary>
        /// 유니티 엔진 컨텍스트에게 전송
        /// </summary>
        public static void Post(Action action)
        {
            m_context.Post(InvokeActionCache, action);
        }

        /// <summary>
        /// 유니티 엔진 컨텍스트에게 전송
        /// </summary>
        public static void Post(Action<object> action, object state)
        {
            // TODO Gabage generated
            m_context.Post(InvokeActionStateCache, Tuple.Create(action, state));
        }

        /// <summary>
        /// 유니티 엔진 컨텍스트에게 전송
        /// </summary>
        public static void Post(Exception exception)
        {
            m_context.Post(InvokeExeptionDispatchCache, ExceptionDispatchInfo.Capture(exception));
        }

        /// <summary>
        /// 유니티 엔진 컨텍스트에게 전송
        /// </summary>
        public static void Post(ExceptionDispatchInfo exception)
        {
            m_context.Post(InvokeExeptionDispatchCache, exception);
            //InvokeExeptionDispatchCache(exception);
        }

        /// <summary>
        /// 델리게이트 캐시 (타입 캐스팅)
        /// </summary>
        static SendOrPostCallback InvokeActionCache = InvokeAction;
        static void InvokeAction(object action)
        {
            var state = (Action)action;

            state?.Invoke();
        }

        /// <summary>
        /// 델리게이트 캐시 (타입 캐스팅)
        /// </summary>
        static SendOrPostCallback InvokeActionStateCache = InvokeActionState;
        static void InvokeActionState(object actionState)
        {
            var state = (Tuple<Action<object>, object>)actionState;

            state?.Item1?.Invoke(state.Item2);
        }

        /// <summary>
        /// 델리게이트 캐시 (타입 캐스팅)
        /// </summary>
        static SendOrPostCallback InvokeExeptionDispatchCache = InvokeExeptionDispatchInfo;
        static void InvokeExeptionDispatchInfo(object edi)
        {
            var state = (ExceptionDispatchInfo)edi;

            if (state != null)
            {
                state.Throw();
            }
            else
            {
                throw new ArgumentNullException(nameof(edi));
            }
        }
    }
}