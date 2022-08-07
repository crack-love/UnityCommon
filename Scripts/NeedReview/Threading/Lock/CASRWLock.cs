using UnityEngine;
using UnityCommon;
using System;
using System.Threading;
using UnityCommon;

/// <summary>
/// 2020-09-24 목 오후 6:04:38, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Compare And Swap Read/Write Lock.
    /// </summary>
    public class CASRWLock
    {
        // TODO : class Token to struct

        public const int MaxRead = 1024;
        public const int MaxWrite = 1;

        int m_writeCount;
        int m_readCount;
        object m_creationLock;

        public int WriteCount => m_writeCount;

        public int ReadCount => m_readCount;

        public CASRWLock()
        {
            m_creationLock = new object();
        }

        /// <summary>
        /// Reading locks write
        /// </summary>
        public IDisposable Read()
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
        /// Writing locks read/write
        /// </summary>
        public IDisposable Write()
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

        class ReadToken : ArrayedPoolItem<ReadToken>, IDisposable
        {
            CASRWLock m_caslock;
            bool m_isNeedDispose;

            public static ReadToken Create(CASRWLock caslock)
            {
                if (!s_pool.TryGet(out var res))
                {
                    res = new ReadToken();
                }

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

            public override void Clear()
            {
                m_isNeedDispose = false;
                m_caslock = null;
            }

            public void Dispose()
            {
                m_isNeedDispose = false;

                Interlocked.Decrement(ref m_caslock.m_readCount);

                ClearReturn();
            }
        }

        class WriteToken : ArrayedPoolItem<WriteToken>, IDisposable
        {
            CASRWLock m_caslock;
            bool m_isNeedDispose;

            public static WriteToken Create(CASRWLock caslock)
            {
                if (!s_pool.TryGet(out var res))
                {
                    res = new WriteToken();
                }

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

            public override void Clear()
            {
                m_isNeedDispose = false;
                m_caslock = null;
            }

            public void Dispose()
            {
                m_isNeedDispose = false;

                Interlocked.Decrement(ref m_caslock.m_writeCount);

                ClearReturn();
            }
        }
    }
}