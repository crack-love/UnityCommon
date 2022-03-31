using UnityEngine;
using UnityCommon;
using System;
using System.Threading;

/// <summary>
/// 2020-09-24 목 오후 6:04:38, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class CASRWLockItem<T>
    {
        public const int MaxRead = 1024;
        public const int MaxWrite = 1;

        int m_writeCount;
        int m_readCount;
        object m_creationLock;
        T m_item;

        public int WriteCount => m_writeCount;

        public int ReadCount => m_readCount;

        public T Item
        {
            get => m_item;
            set => m_item = value;
        }

        public CASRWLockItem()
        {
            m_creationLock = new object();
        }

        public CASRWLockItem(T item)
        {
            m_creationLock = new object();
            m_item = item;
        }

        /// <summary>
        /// ReadToken locks write and is disposable
        /// </summary>
        public ReadToken Read()
        {
            lock (m_creationLock)
            {
                // wait all write
                Interlock.WaitCompare(ref m_writeCount, 0);

                // enter read
                Interlock.Enter(ref m_readCount, MaxRead);
            }

            return ReadToken.Create(this);
        }

        /// <summary>
        /// WriteToken locks read/write and is disposable
        /// </summary>
        public WriteToken Write()
        {
            lock (m_creationLock)
            {
                // wait all read
                Interlock.WaitCompare(ref m_readCount, 0);

                // enter write
                Interlock.Enter(ref m_writeCount, MaxWrite);
            }

            return WriteToken.Create(this);
        }

        public class ReadToken : IDisposable
        {
            CASRWLockItem<T> m_caslock;
            bool m_isNeedDispose;

            public T Item => m_caslock.m_item;

            public static ReadToken Create(CASRWLockItem<T> caslock)
            {
                //if (!s_pool.TryGet(out var res))
                //{
                //  res = new ReadToken();
                //}
                var res = new ReadToken();
                res.m_caslock = caslock;
                res.m_isNeedDispose = true;

                return res;
            }

            ~ReadToken()
            {
                if (m_isNeedDispose)
                {
                    Interlocked.Decrement(ref m_caslock.m_readCount);

                    Debug.LogError("CASRWLock's Read is not disposed");
                }
            }

            public void Clear()
            {
                m_isNeedDispose = false;
                m_caslock = null;
            }

            public void Dispose()
            {
                m_isNeedDispose = false;

                Interlocked.Decrement(ref m_caslock.m_readCount);

                //ClearReturn();
            }
        }

        public class WriteToken : IDisposable
        {
            CASRWLockItem<T> m_caslock;
            bool m_isNeedDispose;

            public T Item => m_caslock.m_item;

            public static WriteToken Create(CASRWLockItem<T> caslock)
            {
                //if (!s_pool.TryGet(out var res))
                //{
                    
                //}

                var res = new WriteToken();
                res.m_caslock = caslock;
                res.m_isNeedDispose = true;

                return res;
            }

            ~WriteToken()
            {
                if (m_isNeedDispose)
                {
                    Interlocked.Decrement(ref m_caslock.m_writeCount);

                    Debug.LogError("CASRWLock's Write is not disposed");
                }
            }

            public void Clear()
            {
                m_isNeedDispose = false;
                m_caslock = null;
            }

            public void Dispose()
            {
                m_isNeedDispose = false;

                Interlocked.Decrement(ref m_caslock.m_writeCount);

                //ClearReturn();
            }
        }
    }
}