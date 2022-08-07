#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;

/// <summary>
/// 2021-05-26 수 오후 8:10:15, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace mvp7
{
    /// <summary>
    /// async Task to coroutine
    /// </summary>
    public class WaitForTask : CustomYieldInstruction
    {
        Task m_task;

        public override bool keepWaiting
        {
            get
            {
                return m_task.IsRunning;
            }
        }

        public WaitForTask(Task src)
        {
            m_task = src;
        }
    }

    /// <summary>
    /// async Task to coroutine
    /// </summary>
    public class WaitForTask<R> : CustomYieldInstruction
    {
        Task<R> m_task;

        public override bool keepWaiting
        {
            get
            {
                return m_task.IsRunning;
            }
        }

        public WaitForTask(Task<R> src)
        {
            m_task = src;
        }
    }
}