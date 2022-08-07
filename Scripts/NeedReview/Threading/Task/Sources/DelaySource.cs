using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// 2020-06-11 목 오후 9:05:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Timer start with creation
    /// </summary>
    class DelaySource : TaskSourceBase<DelaySource>, ILoopable
    {
        float m_timeLeftSec;
        bool m_isUnscale;

        public static DelaySource Create(float ms, bool isUnscale)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new DelaySource();
            }

            // ms to sec
            res.m_timeLeftSec = ms / 1000;
            res.m_isUnscale = isUnscale;

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

            if (m_isUnscale)
            {
                m_timeLeftSec -= UnityContext.UnscaleDeltaTime;
            }
            else
            {
                m_timeLeftSec -= UnityContext.DeltaTime;
            }

            // Complete
            if (m_timeLeftSec <= 0)
            {
                SetComplete();

                return false;
            }

            return true;
        }

        protected override void OnClear()
        {
            m_timeLeftSec = 0;
        }
    }
}