using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

/// <summary>
/// 2020-06-11 목 오후 5:56:01, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// TaskSource's Core Structure
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    struct TaskSourceCore
    {
        /// <summary>
        /// Hold exception. pooled
        /// </summary>
        ExceptionDispatchInfoAndHandled m_exception;

        /// <summary>
        /// All routine after async finished. promise to call GetResult()
        /// </summary>
        FlexibleAction m_continuation;

        public TaskStatus Status
        {
            get
            {
                if (m_exception != null)
                {
                    return TaskStatus.Faulted;
                }
                else if (m_continuation.BaseEquals(TaskDelegate.CompletedSentinalAction))
                {
                    return TaskStatus.Succeed;
                }
                else
                {
                    return TaskStatus.Pending;
                }
            }
        }

        /// <summary>
        /// Clear can be called outside of class like finalizer
        /// </summary>
        public void Clear()
        {
            m_exception.ClearReturn();
            m_continuation.Clear();

            m_exception = null;
        }

        public void GetResult()
        {
            m_exception?.Exception?.Throw();
        }

        public void OnCompleted(Action continuation)
        {
            AddOrInvokeContinuation(continuation);
        }

        /// <summary>
        /// SetComplete can be called multiple times.
        /// </summary>
        public void SetComplete()
        {
            // state is set completed by volatile inovokation
            VolatileInvokeContinuation();
        }

        public void SetComplete(Exception e)
        {
            m_exception = ExceptionDispatchInfoAndHandled.Create(e);

            VolatileInvokeContinuation();
        }

        /// <summary>
        /// Set complete with OperationCanceledException
        /// </summary>
        public void Cancel()
        {
            SetComplete(new OperationCanceledException());
        }

        /// <summary>
        /// Volatile read, Set completed state
        /// </summary>
        void VolatileInvokeContinuation()
        {
            m_continuation.InvokeExchange(TaskDelegate.CompletedSentinalAction);
        }

        /// <summary>
        /// Add or Invoke if completed state
        /// </summary>
        /// <param name="continuation"></param>
        void AddOrInvokeContinuation(Action continuation)
        {
            // Add if not completed state
            if (!m_continuation.ExclusiveSet(continuation, TaskDelegate.CompletedSentinalAction))
            {
                // invoke if completed state
                continuation();
            }
        }

        // pooled
        class ExceptionDispatchInfoAndHandled : ArrayedPoolItem<ExceptionDispatchInfoAndHandled>
        {
            public ExceptionDispatchInfo Exception;

            /// <summary>
            /// Exception need to handled
            /// </summary>
            public bool IsNeedHandle;

            public static ExceptionDispatchInfoAndHandled Create(Exception e)
            {
                if (!s_pool.TryGet(out var res))
                {
                    res = new ExceptionDispatchInfoAndHandled();
                }

                res.Exception = ExceptionDispatchInfo.Capture(e);
                res.IsNeedHandle = true;

                return res;
            }

            public override void Clear()
            {
                if (IsNeedHandle)
                {
                    IsNeedHandle = false;
                    Debug.LogException(Exception.SourceException);
                    //UnityContext.Post(Exception);
                }

                Exception = null;
            }
        }
    }

    [StructLayout(LayoutKind.Auto)]
    struct TaskSourceCore<TResult>
    {
        TaskSourceCore m_core;

        TResult m_result;

        public TaskStatus Status
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_core.Status;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            m_result = default;
            m_core.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult GetResult()
        {
            m_core.GetResult();
            return m_result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            m_core.OnCompleted(continuation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetComplete(TResult result)
        {
            m_result = result;
            m_core.SetComplete();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetComplete()
        {
            m_core.SetComplete();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetComplete(Exception e)
        {
            m_core.SetComplete(e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Cancel()
        {
            m_core.Cancel();
        }
    }
}