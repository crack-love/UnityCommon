using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-08-16 일 오전 12:29:49, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// SetComplete Need to be called. 
    /// GC Pooled. Cancellation callback registed
    /// </summary>
    public abstract class TaskSourceBase<TDerived> : ArrayedPoolItemGC<TDerived>, ITaskSource, ITaskCancelReciever  where TDerived : TaskSourceBase<TDerived>
    {
        TaskSourceCore m_core;
        ITaskCancellation m_cancellation;

        /// <summary>
        /// Pending as Processing.
        /// Faulted with Cancelatoin or Exception.
        /// and Succed as Finished.
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                if (m_cancellation != null && m_cancellation.IsCanceled)
                {
                    return TaskStatus.Faulted;
                }
                else
                {
                    return m_core.Status;
                }
            }
        }

        public ITaskCancellation Cancellation
        {
            get => m_cancellation;
            set => m_cancellation = value;
        }

        public void GetResult()
        {
            m_core.GetResult();
        }

        public void OnCompleted(Action continuation)
        {
            m_core.OnCompleted(continuation);
        }

        public void SetComplete(Exception e)
        {
            m_core.SetComplete(e);
        }

        public void SetComplete()
        {
            m_core.SetComplete();
        }

        public void OnCanceled()
        {
            m_core.Cancel();
        }

        public override void Clear()
        {
            m_core.Clear();
            m_cancellation = null;

            OnClear();
        }

        protected abstract void OnClear();
    }

    abstract class TaskSourceBase<TDerived, R> : ArrayedPoolItemGC<TDerived>, ITaskSource<R>, ITaskCancelReciever where TDerived : TaskSourceBase<TDerived, R>
    {
        TaskSourceCore<R> m_core;
        ITaskCancellation m_cancellation;

        public TaskStatus Status
        {
            get
            {
                if (m_cancellation != null && m_cancellation.IsCanceled)
                {
                    return TaskStatus.Faulted;
                }
                else
                {
                    return m_core.Status;
                }
            }
        }

        public ITaskCancellation Cancellation
        {
            get => m_cancellation;
            set => m_cancellation = value;
        }

        void ITaskSource.GetResult()
        {
            m_core.GetResult();
        }

        public R GetResult()
        {
            return m_core.GetResult();
        }

        public void OnCompleted(Action continuation)
        {
            m_core.OnCompleted(continuation);
        }

        public void SetComplete(Exception e)
        {
            m_core.SetComplete(e);
        }

        public void SetComplete(R res)
        {
            m_core.SetComplete(res);
        }

        public void SetComplete()
        {
            m_core.SetComplete();
        }

        public void OnCanceled()
        {
            m_core.Cancel();
        }

        public override void Clear()
        {
            m_core.Clear();
            m_cancellation = null;

            OnClear();
        }

        protected abstract void OnClear();
    }
}