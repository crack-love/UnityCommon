using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;
using UnityCommon.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;

/// <summary>
/// 2020-06-11 목 오후 5:56:01, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public struct TaskAwaiter : IAwaiter
    {
        /// <summary>
        /// If source is null, Task is handled as Succesfully completed
        /// </summary>
        ITaskSource m_src;

        /// <summary>
        /// IsCompleted is called by the system to check 
        /// if the task is already complete and therefore 
        /// there’s no reason to suspend the async function.
        /// Is called once only start awaiting
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                if (m_src == null)
                {
                    return true;
                }
                else
                {
                    return m_src.Status != TaskStatus.Pending;
                }
            }
        }

        public TaskAwaiter(ITaskSource src)
        {
            m_src = src;
        }

        /// <summary>
        /// GetResult is called to get the outcome of the task when it's done.
        /// This can be any type or even void if there is no outcome.
        /// If the task failed, this method throws an exception to indicate that.
        /// </summary>
        public void GetResult()
        {
            if (m_src == null)
            {
                return;
            }
            else
            {
                m_src.GetResult();
            }
        }

        /// <summary>
        /// OnCompleted is called to schedule a "continuation" to the task.
        /// This is an Action delegate that's invoked when the task is done.
        /// </summary>
        public void OnCompleted(Action continuation)
        {
            if (m_src == null)
            {
                continuation();
            }
            else
            {

                m_src.OnCompleted(continuation);
            }
        }
    }

    public struct TaskAwaiter<R> : IAwaiter<R>
    {
        /// <summary>
        /// If source is null, Task is handled as Succesfully completed
        /// </summary>
        ITaskSource<R> m_src;

        /// <summary>
        /// IsCompleted is called by the system to check 
        /// if the task is already complete and therefore 
        /// there’s no reason to suspend the async function
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                if (m_src == null)
                {
                    return true;
                }
                else
                {
                    return m_src.Status != TaskStatus.Pending;
                }
            }
        }

        public ITaskCancellation Cancellation
        {
            get => m_src.Cancellation;
            set => m_src.Cancellation = value;
        }

        public TaskAwaiter(ITaskSource<R> src)
        {
            m_src = src;
        }

        /// <summary>
        /// GetResult is called to get the outcome of the task when it's done.
        /// This can be any type or even void if there is no outcome.
        /// If the task failed, this method throws an exception to indicate that.
        /// </summary>
        public R GetResult()
        {
            if (m_src == null)
            {
                return default;
            }
            else
            {
                return m_src.GetResult();
            }
        }

        /// <summary>
        /// OnCompleted is called to schedule a "continuation" to the task.
        /// This is an Action delegate that's invoked when the task is done.
        /// </summary>
        public void OnCompleted(Action continuation)
        {
            if (m_src == null)
            {
                continuation();
            }
            else
            {

                m_src.OnCompleted(continuation);
            }
        }
    }
}