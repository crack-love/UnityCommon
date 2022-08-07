using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// 2020-08-15 토 오전 12:05:38, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public interface ITaskCancellation
    {
        bool IsCanceled { get; }

        void Cancel();
    }

    public interface ITaskCancelReciever
    {
        void OnCanceled();
    }
}