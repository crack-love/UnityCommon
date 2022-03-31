using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// 2020-08-14 금 오전 12:23:01, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Implimentation Guide to Awaitable
    /// </summary>
    interface IAwaitable
    {
        IAwaiter GetAwaiter();
    }

    /// <summary>
    /// Implimentation Guide to Awaitable. TAwaiter : specify type of awaiter
    /// </summary>
    interface IAwaitable<TAwaiter>
    {
        TAwaiter GetAwaiter();
    }

    /// <summary>
    /// Implimentation Guide of Awaiter
    /// </summary>
    /// INotifyCompletion: contracts to be Awaiter
    interface IAwaiter : INotifyCompletion 
    {
        /// <summary>
        /// IsCompleted is called by system start awaiting
        /// </summary>
        bool IsCompleted { get; }

        void GetResult();
    }

    /// <summary>
    /// Implimentation Guide of Awaiter
    /// </summary>
    interface IAwaiter<TResult> : INotifyCompletion
    {
        /// <summary>
        /// IsCompleted is called by system start awaiting
        /// </summary>
        bool IsCompleted { get; }

        TResult GetResult();
    }

    interface IAwaitableAwaiter<TSelf> : IAwaitable<TSelf>, IAwaiter
    {

    }

    interface IAwaitableAwaiter<TResult, TSelf> : IAwaitable<TSelf>, IAwaiter<TResult>
    {

    }
}