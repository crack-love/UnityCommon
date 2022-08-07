#pragma warning disable CS0436 // disable 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityCommon;
using Task = UnityCommon.Task;

// 경량화 버전. 닷넷 코어 참조하여 작성함
// https://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs

namespace UnityCommon.CompilerServices
{
    /// <summary>
    /// Provides a builder for asynchronous methods that return <see cref="UnityTask"/>.
    /// This type is intended for compiler use only.
    /// </summary>
    /// <remarks>
    /// this is a value type, and thus it is copied by value.
    /// Prior to being copied, one of its Task, SetResult, or SetException members must be accessed,
    /// or else the copies may end up building distinct instances.    
    /// </remarks>
    public struct AsyncTaskActionMethodBuilder
    {
        /// <summary>
        /// Provides the ability to invoke a state machine's MoveNext method under a supplied ExecutionContext.
        /// </summary>
        StateMachineRunner m_runner;

        /// <summary>
        /// local instance for if m_runner is not initialized
        /// </summary>
        Exception m_exception;

        /// <summary>
        /// Initializes a new <see cref="AsyncTaskMethodBuilder"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskActionMethodBuilder Create()
        {
            return new AsyncTaskActionMethodBuilder();
        }

        // this method called actual running process
        /// <summary>
        /// Initiates the builder's execution with the associated state machine.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        /// <summary>
        /// Associates the builder with the state machine it represents.
        /// </summary>
        /// <param name="stateMachine">The heap-allocated state machine object.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // Not neccessary to memory stateMachine boxed in this case
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        [DebuggerHidden] // Stop debugger that can't make forward progress. it slips threads. (when calling OnCompleted)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            // If this is our first await set this stateMachine
            if (m_runner == null)
            {
                m_runner = StateMachineRunner.Create(stateMachine);
            }
            
            // context switching could happen awaiter.
            // each runner's moveNext is cached to prevent garbage
            awaiter.OnCompleted(m_runner.MoveNextCache);
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        [DebuggerHidden] // Stop debugger that can't make forward progress .it slips threads. (when calling OnCompleted)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            // If this is our first await set this stateMachine
            if (m_runner == null)
            {
                m_runner = StateMachineRunner.Create(stateMachine);
            }

            // context switching could happen awaiter
            // each runner's moveNext is cached to prevent garbage
            awaiter.UnsafeOnCompleted(m_runner.MoveNextCache);

            UnityEngine.Debug.Log("unsafe");
        }

        /// <summary>
        /// Gets the <see cref="UnityTask{TResult}"/> for this builder.
        /// </summary>
        public Task Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (m_exception != null)
                {
                    return Task.Completed(m_exception);
                }
                else if (m_runner != null)
                {
                    return new Task(m_runner);
                }
                else
                {
                    return Task.Completed();
                }
            }
        }

        /// <summary>
        /// Completes the Task in the TaskStatus.RanToCompletion state with the specified result.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
            if (m_runner != null)
            { 
                m_runner.SetComplete();
            }
        }

        /// <summary>
        /// Completes the Task{TResult} in the TaskStatus.Faulted  state with the specified exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            if (m_runner == null)
            {
                m_exception = exception;
            }
            else
            {
                m_runner.SetComplete(exception);
            }
        }
    }
}

#pragma warning restore CS0436