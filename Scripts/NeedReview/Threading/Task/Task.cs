using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;
using UnityCompilerServices;
using System.Threading;
using System.Runtime.InteropServices;

/// <summary>
/// 2020-06-11 목 오후 5:56:01, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Represents an asynchronous operation.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    [AsyncMethodBuilder(typeof(AsyncTaskActionMethodBuilder))] // mark this async-await method target
    public partial struct Task : IAwaitable<TaskAwaiter> // district awaiter and task to hiding awaiter methods from user
    {
        /// <summary>
        /// If source is null, Task is handled as Succesfully completed
        /// </summary>
        ITaskSource m_src;

        /// <summary>
        /// Get current status
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                if (m_src == null)
                {
                    return TaskStatus.Succeed;
                }
                else
                {
                    return m_src.Status;
                }
            }
        }

        /// <summary>
        /// Same with Status is Pending
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (m_src == null)
                {
                    return false;
                }
                else
                {
                    return m_src.Status == TaskStatus.Pending;
                }
            }
        }

        /// <summary>
        /// Get source's cancellation
        /// </summary>
        public ITaskCancellation Cancellation
        {
            get
            {
                if (m_src == null)
                {
                    return null;
                }

                return m_src.Cancellation;
            }
            set
            {
                if (m_src != null)
                {
                    m_src.Cancellation = value;
                }
            }
        }

        /// <summary>
        /// Task does not starting with construction
        /// </summary>
        public Task(ITaskSource source)
        {
            m_src = source;
        }

        /// <summary>
        /// GetAwaiter is self-explanatory: it returns the awaiter object.
        /// </summary>
        public TaskAwaiter GetAwaiter()
        {
            return new TaskAwaiter(m_src);
        }

        /// <summary>
        /// Make this Task dismiss, exception still can be handeled when GC with TaskSource if support
        /// </summary>
        public void Forget()
        {

        }
    }

    [StructLayout(LayoutKind.Auto)]
    [AsyncMethodBuilder(typeof(AsyncTaskFuncMethodBuilder<>))]
    public partial struct Task<TR> : IAwaitable<TaskAwaiter<TR>>
    {
        ITaskSource<TR> m_src;

        /// <summary>
        /// Get current status
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                if (m_src == null)
                {
                    return TaskStatus.Succeed;
                }

                return m_src.Status;
            }
        }

        public bool IsRunning
        {
            get
            {
                if (m_src == null)
                {
                    return false;
                }
                else
                {
                    return m_src.Status == TaskStatus.Pending;
                }
            }
        }

        /// <summary>
        /// Get source's cancellation
        /// </summary>
        public ITaskCancellation Cancellation
        {
            get
            {
                if (m_src == null)
                {
                    return null;
                }
                else
                {
                    return m_src.Cancellation;
                }
            }
            set
            {
                if (m_src != null)
                {
                    m_src.Cancellation = value;
                }
            }
        }

        public Task(ITaskSource<TR> source)
        {
            m_src = source;
        }

        public static implicit operator Task(Task<TR> src)
        {
            return new Task(src.m_src);
        }

        /// <summary>
        /// GetAwaiter is self-explanatory: it returns the awaiter object.
        /// </summary>
        public TaskAwaiter<TR> GetAwaiter()
        {
            return new TaskAwaiter<TR>(m_src);
        }

        /// <summary>
        /// Make this Task dismiss, exception still can be handeled
        /// </summary>
        public void Forget()
        {

        }
    }

    /// <summary>
    /// Task status is either Pending or Completed.
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// The operation has not set completed.
        /// </summary>
        Pending,

        /// <summary>
        /// The operation completed with an error or canceled.
        /// </summary>
        Faulted,

        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        Succeed,
    }
}

/// 20200925 making no GC alloc overhaul
/// 20200928 add cancellation
/// 20210524 finish cancellation. distinct sources to awaiter/source