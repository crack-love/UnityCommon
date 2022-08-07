using UnityEngine;
using System;
using System.Collections.Generic;
using UnityCommon;

/// <summary>
/// 2020-06-01 월 오후 6:08:04, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static partial class UnityContext
    {
        class YieldRunner
        {
            // 큐가 2개인 이유:
            // 큐가 하나일 경우 해당 큐 enumeration 중 yield가 요청되면 바로 dequeue하기 때문에
            // yield를 정상적으로 미루지 않는다. 큐를 2개로 하여 대기 큐에 등록한다

            Queue<Action> m_fstQueue;
            Queue<Action> m_sndQueue;
            bool m_isFstQueueRuning;

            public YieldRunner()
            {
                m_fstQueue = new Queue<Action>();
                m_sndQueue = new Queue<Action>();
            }

            public void Queue(Action action)
            {
                if (m_isFstQueueRuning)
                {
                    m_sndQueue.Enqueue(action);
                }
                else
                {
                    m_fstQueue.Enqueue(action);
                }
            }

            public void Run()
            {
                if (m_isFstQueueRuning)
                {
                    EnumerateQueue(m_fstQueue);
                }
                else
                {
                    EnumerateQueue(m_sndQueue);
                }
                
                // swap queue
                m_isFstQueueRuning = !m_isFstQueueRuning;
            }

            void EnumerateQueue(Queue<Action> queue)
            {
                while (queue.Count > 0)
                {
                    try
                    {
                        queue.Dequeue()?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }

        class UpdateRunner
        {
            UnityCommon.LinkedList<ILoopable> m_list = new LinkedList<ILoopable>();

            public void Queue(ILoopable action)
            {
                m_list.AddLast(action);
            }

            internal void Dequeue(ILoopable update)
            {
                m_list.Remove(update);
            }

            internal void Dequeue(Action action)
            {
                foreach (var node in m_list.Nodes)
                {
                    var value = node.Value;

                    if (value is LoopableAction act)
                    {
                        if (act.Equals(action))
                        {
                            node.Remove();
                            //m_list.Remove(node);
                            return;
                        }
                    }
                }
            }

            internal void Dequeue(Func<bool> action)
            {
                foreach (var node in m_list.Nodes)
                {
                    var value = node.Value;

                    if (value is LoopableFunction func)
                    {
                        if (func.Equals(action))
                        {
                            node.Remove();
                            //m_list.Remove(node);
                            return;
                        }
                    }
                }
            }

            public void Run()
            {
                foreach(var node in m_list.Nodes)
                {
                    var runner = node.Value;

                    // invalid
                    if (runner == null)
                    {
                        node.Remove();
                        //m_list.Remove(node);
                        continue;
                    }

                    try
                    {
                        // finished
                        if (!runner.MoveNext())
                        {
                            node.Remove();
                            //m_list.Remove(node);
                        }
                    }
                    // node throw exception
                    catch (Exception e)
                    {
                        node.Remove();
                        //m_list.Remove(node);

                        Debug.LogException(e);
                    }
                }
            }
        }
    }
}