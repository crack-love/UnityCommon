using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

/// <summary>
/// 2020-06-11 목 오후 9:05:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    class WaitUntilSource : TaskSourceBase<WaitUntilSource>, ILoopable
    {
        Func<bool> m_predict;
        
        public Func<bool> Predicate
        {
            get => m_predict;
            set => m_predict = value;
        }

        public static WaitUntilSource Create(Func<bool> predict)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new WaitUntilSource();
            }

            res.m_predict = predict;

            // start
            UnityContext.QueueUpdate(PlayerLoopType.Update, res);

            return res;
        }

        public bool MoveNext()
        {
            if (Status != TaskStatus.Pending)
            {
                return false;
            }

            try
            {
                if (m_predict())
                {
                    SetComplete();

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                SetComplete(e);

                return false;
            }
        }

        protected override void OnClear()
        {
            m_predict = null;
        }
    }

}