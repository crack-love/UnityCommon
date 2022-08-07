#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2021-05-26 수 오후 7:14:26, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    class CompletedSource : TaskSourceBase<CompletedSource>
    {
        public static CompletedSource Create(Exception e)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new CompletedSource();
            }

            res.SetComplete(e);

            return res;
        }

        protected override void OnClear()
        {

        }
    }

    class CompletedSource<R> : TaskSourceBase<CompletedSource<R>, R>
    {
        public static CompletedSource<R> Create(R value)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new CompletedSource<R>();
            }

            res.SetComplete(value);

            return res;
        }

        public static CompletedSource<R> Create(Exception e)
        {
            if (!s_pool.TryGet(out var res))
            {
                res = new CompletedSource<R>();
            }

            res.SetComplete(e);

            return res;
        }

        protected override void OnClear()
        {

        }
    }
}