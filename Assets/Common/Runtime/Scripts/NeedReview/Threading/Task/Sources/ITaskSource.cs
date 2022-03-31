using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-08-15 토 오전 12:05:38, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Start by creation. Canceled 
    /// </summary>
    public interface ITaskSource
    {
        /// <summary>
        /// Get current status
        /// </summary>
        TaskStatus Status { get; }

        /// <summary>
        /// Propagation of Task cancel
        /// </summary>
        ITaskCancellation Cancellation { get; set; }

        /// <summary>
        /// Throws exception if needed
        /// </summary>
        void GetResult();

        /// <summary>
        /// Set Continuation
        /// </summary>
        void OnCompleted(Action continuation);
    }

    public interface ITaskSource<R> : ITaskSource
    {
        /// <summary>
        /// Throws exception if needed
        /// </summary>
        new R GetResult();
    }
}