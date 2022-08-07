#pragma warning disable CS0436 // disable 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'

// https://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityCommon;
using Task = UnityCommon.Task;

namespace UnityCommon.CompilerServices
{
    /// <summary>
    /// Provides a builder for asynchronous methods that return <see cref="UnityTask{TResult}"/>.
    /// This type is intended for compiler use only.
    /// </summary>
    /// <remarks>
    /// this is a value type, and thus it is copied by value.
    /// Prior to being copied, one of its Task, SetResult, or SetException members must be accessed,
    /// or else the copies may end up building distinct instances.    
    /// </remarks>
    public struct AsyncTaskFuncMethodBuilder<TResult>
    {
        /// <summary>
        /// Provides the ability to invoke a state machine's MoveNext method under a supplied ExecutionContext.
        /// </summary>
        StateMachineRunner<TResult> m_runner;

        /// <summary>
        /// local instance for if m_runner is not initialized
        /// </summary>
        Exception m_exception;

        /// <summary>
        /// local instance for if m_runner is not initialized
        /// </summary>
        TResult m_result;

        /// <summary>
        /// Initializes a new <see cref="System.Runtime.CompilerServices.AsyncTaskMethodBuilder"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncTaskFuncMethodBuilder<TResult> Create()
        {
            return new AsyncTaskFuncMethodBuilder<TResult>();
        }

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
            // Not neccessary ths imp to memory stateMachine boxed
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        [DebuggerHidden] // Stop debugger that can't make forward progress it slips threads.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            // If this is our first await set this stateMachine
            if (m_runner == null)
            {
                m_runner = StateMachineRunner<TResult>.Create(stateMachine);
            }

            // context switching could happen awaiter
            awaiter.OnCompleted(m_runner.MoveNextCache);
        }

        /// <summary>
        /// Schedules the specified state machine to be pushed forward when the specified awaiter completes.
        /// </summary>
        [DebuggerHidden] // Stop debugger that can't make forward progress it slips threads.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            // If this is our first await set this stateMachine
            if (m_runner == null)
            {
                m_runner = StateMachineRunner<TResult>.Create(stateMachine);
            }

            // context switching could happen awaiter
            awaiter.UnsafeOnCompleted(m_runner.MoveNextCache);
        }

        /// <summary>
        /// Gets the <see cref="UnityTask{TResult}"/> for this builder.
        /// </summary>
        public Task<TResult> Task
        {
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            { 
                if (m_exception != null)
                {
                    return UnityCommon.Task.Completed<TResult>(m_exception);
                }
                else if (m_runner != null)
                {
                    return new Task<TResult>(m_runner);
                }
                else
                {
                    return UnityCommon.Task.Completed(m_result);
                }
            }
        }

        /// <summary>
        /// Completes the Task in the TaskStatus.RanToCompletion state with the specified result.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(TResult result)
        {
            if (m_runner == null)
            {
                m_result = result;
            }
            else
            {
                m_runner.SetComplete(result);
            }
        }

        /// <summary>
        /// Completes the Task{TResult} in the TaskStatus.Faulted  state with the specified exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            //UnityEngine.Debug.Log(exception);

            if (m_runner == null)
            {
                m_exception = exception;
            }
            else
            {
                m_runner.SetComplete(exception);
            }
        }

        /* DEBUG
        /// <summary>Gets a description of the state of the state machine object, suitable for debug purposes.</summary>
        /// <param name="stateMachine">The state machine object.</param>
        /// <returns>A description of the state machine.</returns>
        internal static string GetAsyncStateMachineDescription(IAsyncStateMachine stateMachine)
        {
            Debug.Assert(stateMachine != null);

            Type stateMachineType = stateMachine.GetType();
            FieldInfo[] fields = stateMachineType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var sb = new StringBuilder();
            sb.AppendLine(stateMachineType.FullName);
            foreach (FieldInfo fi in fields)
            {
                sb.Append("    ").Append(fi.Name).Append(": ").Append(fi.GetValue(stateMachine)).AppendLine();
            }
            return sb.ToString();
        }
        */
    }
}

#pragma warning restore CS0436