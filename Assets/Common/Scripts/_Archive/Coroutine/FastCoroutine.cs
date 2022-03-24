/*using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System.Collections;
using System;

/// <summary>
/// 2021-04-28 수 오후 4:43:02, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Move until wait instruction ex) WaitXXX
    /// </summary>
    // Static Manager part
    public partial class FastCoroutine
    {
        static List<FastCoroutine> m_coroutines;

        static FastCoroutine()
        {
            m_coroutines = new List<FastCoroutine>();

            UnityContext.QueueUpdate(PlayerLoopType.Update, Update);
        }
        
        static void Update()
        {
            int size = m_coroutines.Count;
            for (int i = 0; i < size; ++i)
            {
                var coroutine = m_coroutines[i];
                bool isRemove = false;

                try
                {
                    if (coroutine == null || !coroutine.MoveNext())
                    {
                        isRemove = true;
                    }
                }
                catch(Exception e)
                {
                    Debug.LogException(e);
                    isRemove = true;
                }

                if (isRemove)
                {
                    m_coroutines.RemoveAt(i);
                    --i;
                    --size;

                    coroutine?.Return();
                }
            }
        }

        public static FastCoroutine StartCoroutine(IEnumerator root)
        {
            var res = CreateAndMove(root);

            if (res != null)
            {
                m_coroutines.Add(res);
            }

            return res;
        }

        
        //public static FastCoroutine Yield(Action action)
        //{
        //    return StartCoroutine(YieldOperation(action));
        //}
        

        //static IEnumerator YieldOperation(Action action)
        //{
        //    yield return YieldFactroy.WaitForUpdateSkipped(1);

        //    action();
        //}
    
        public static void StopCoroutine(FastCoroutine src)
        {
            m_coroutines.Remove(src);
        }
    }

    // Coroutine instance part
    public partial class FastCoroutine
    {
        static ArrayedPool<FastCoroutine> s_pool = Singletone<ArrayedPool<FastCoroutine>>.Instance;
        Stack<IEnumerator> m_stack;
        
        FastCoroutine()
        {
            m_stack = new Stack<IEnumerator>();
        }

        /// <summary>
        /// Return null if finished
        /// </summary>
        static FastCoroutine CreateAndMove(IEnumerator start)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new FastCoroutine();
            }

            res.m_stack.Push(start);
            
            // move when create
            if (res.MoveNext())
            {
                return res;
            }
            else
            {
                res.Return();
                return null;
            }
        }

        bool MoveNext()
        {
            // Move all stack
            while (m_stack.Count > 0)
            {
                var peek = m_stack.Peek();
                var isBroken = false;

                // Move peek to end
                while (peek.MoveNext())
                {
                    // peek is yield operation
                    if (peek is CustomYieldInstruction cyi)
                    {
                        // yield
                        return true;
                    }
                    // peek is enumerable
                    else
                    {
                        // get next
                        var curr = peek.Current;

                        // YieldInstruction is sealed, Wrap proxy runner
                        if (curr is YieldInstruction yi)
                        {
                            // wrap yieldInstruction
                            var waiting = YieldInstructionRunner.StartYieldInstruction(yi);
                            
                            // stack up
                            m_stack.Push(waiting);
                            isBroken = true;
                            break;
                        }
                        // insde step
                        else if (curr is IEnumerator insideStep)
                        {
                            // stack up
                            m_stack.Push(insideStep);
                            isBroken = true;
                            break;
                        }
                    }
                }

                if (!isBroken)
                {
                    m_stack.Pop();
                }
            }

            // finish
            return false;
        }

        void Return()
        {
            // clear
            m_stack.Clear();

            s_pool.TryReturn(this);
        }

        public static implicit operator bool(FastCoroutine src)
        {
            return src != null && src.m_stack.Count > 0;
        }
    }
}*/