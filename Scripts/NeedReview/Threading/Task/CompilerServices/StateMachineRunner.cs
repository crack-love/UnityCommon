using System;
using System.Runtime.CompilerServices;
using UnityCommon;

namespace UnityCompilerServices
{
    /// <summary>
    /// For AsyncTaskActionMethodBuilder
    /// </summary>
    class StateMachineRunner : TaskSourceBase<StateMachineRunner>
    {
        IAsyncStateMachine m_stateMachine;
        Action m_moveNextCache;

        public Action MoveNextCache
        {
            get => m_moveNextCache;
        }

        public static StateMachineRunner Create(IAsyncStateMachine stateMachine)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new StateMachineRunner();
                res.m_moveNextCache = res.MoveNext;
            }

            res.m_stateMachine = stateMachine;

            return res;
        }

        public void MoveNext()
        {
            m_stateMachine.MoveNext();
        }

        protected override void OnClear()
        {
            m_stateMachine = null;

        }
    }

    /// <summary>
    /// async Task<T> method 스테이트머신 러너
    /// </summary>
    internal sealed class StateMachineRunner<TResult> : TaskSourceBase<StateMachineRunner<TResult>, TResult>
    {
        IAsyncStateMachine m_stateMachine;
        Action m_moveNextCache;

        public Action MoveNextCache
        {
            get => m_moveNextCache;
        }

        public static StateMachineRunner<TResult> Create(IAsyncStateMachine stateMachine)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new StateMachineRunner<TResult>();
                res.m_moveNextCache = res.MoveNext;
            }

            res.m_stateMachine = stateMachine;

            return res;
        }

        public void MoveNext()
        {
            m_stateMachine.MoveNext();
        }

        protected override void OnClear()
        {
            m_stateMachine = null;
        }
    }
}