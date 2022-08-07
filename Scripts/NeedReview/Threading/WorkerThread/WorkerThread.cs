using System.Threading;
using System;
using System.Collections.Concurrent;
using ThreadPriority = System.Threading.ThreadPriority;
using UnityEngine;

/// <summary>
/// 2020-06-01 월 오후 6:08:04, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public interface IWorkerThreadAction
    {
        void Execute();
        void OnExecuteFault(Exception e);
    }

    /// <summary>
    /// Need to call start
    /// </summary>
    [Serializable]
    public class WorkerThread : ISerializationCallbackReceiver
    {
        // Fields
        [SerializeField] bool _isBackground;
        [SerializeField] ThreadPriority _priority;
        [SerializeField] int _yieldMs = 100;
        [SerializeField] int _maxCount = 10000;

        [SerializeField, InspectorReadOnly] int _id;
        [SerializeField, InspectorReadOnly] bool _isRunning;
        [SerializeField, InspectorReadOnly] bool _isAbortSafeCalled;
        [SerializeField, InspectorReadOnly] int _lastCount;

        Thread _thread;
        ConcurrentQueue<IWorkerThreadAction> _queue;
        ConcurrentQueue<WorkerThreadActionStruct> _queueStruct;

        // Properties
        public int YieldMs
        {
            get => _yieldMs;
            set => _yieldMs = value;
        }
        public bool IsBackgrounded
        {
            get => _thread.IsBackground;
            set
            {
                _isBackground = value;

                if (_thread != null)
                {
                    _thread.IsBackground = _isBackground;
                }
            }
        }
        public ThreadPriority Priority
        {
            get => _priority;
            set
            {
                _priority = value;

                if (_thread != null)
                {
                    _thread.Priority = _priority;
                }
            }
        }
        public bool IsRunning => _isRunning;

        ~WorkerThread()
        {
            if (_isBackground)
            {
                AbortSafe();
            }
            else
            {
                Abort();
            }
        }

        public void Start()
        {
            if (_thread != null) _thread.Abort();
            if (_queue == null) _queue = new ConcurrentQueue<IWorkerThreadAction>();
            if (_queueStruct == null) _queueStruct = new ConcurrentQueue<WorkerThreadActionStruct>();
            _thread = new Thread(new ThreadStart(Run));

            _thread.IsBackground = _isBackground;
            _thread.Priority = _priority;
            _isRunning = true;
            _isAbortSafeCalled = false;
            _thread.Start();
            _id = _thread.ManagedThreadId;
        }

        public void Enqueue(IWorkerThreadAction task)
        {
            if (_isRunning && _queue.Count < _maxCount)
            {
                _queue.Enqueue(task);
            }
            else
            {
                Debug.LogError("Enqueued WorkerThread Not Running");
                task.Execute();
            }

            _lastCount = _queue.Count +_queueStruct.Count;
        }

        public void Enqueue(WorkerThreadActionStruct task)
        {
            if (_isRunning && _queueStruct.Count < _maxCount)
            {
                _queueStruct.Enqueue(task);
            }
            else
            {
                Debug.LogError("Enqueued WorkerThread Not Running");
                task.Execute();
            }

            _lastCount = _queue.Count + _queueStruct.Count;
        }

        /// <summary>
        /// 즉시 종료
        /// </summary>
        public void Abort()
        {
            _isRunning = false;
            _isAbortSafeCalled = false;
            _thread?.Abort();
            _thread = null;
        }

        /// <summary>
        /// 모든 작업 수행후 종료
        /// </summary>
        public void AbortSafe()
        {
            if (_isRunning)
            {
                _isAbortSafeCalled = true;
            }
        }

        void Run()
        {
            while (_isRunning)
            {
                _lastCount = _queue.Count + _queueStruct.Count;

                if (_queue.TryDequeue(out var task))
                {
                    try
                    {
                        task.Execute();
                    }
                    catch(ThreadAbortException)
                    {
                        _thread = null;

                        // set aborted state
                        Abort();
                    }
                    catch(Exception e)
                    {
                        task.OnExecuteFault(e);
                    }
                }
                else if (_queueStruct.TryDequeue(out var taskStruct))
                {
                    try
                    {
                        taskStruct.Execute();
                    }
                    catch (ThreadAbortException)
                    {
                        _thread = null;

                        // set aborted state
                        Abort();
                    }
                    catch (Exception e)
                    {
                        taskStruct.OnExecuteFault(e);
                    }
                }
                else
                {
                    if (_isAbortSafeCalled)
                    {
                        Abort();
                    }

                    // switch context
                    Thread.Sleep(_yieldMs);
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _isAbortSafeCalled = false;
            _isRunning = false;
        }
    }
}