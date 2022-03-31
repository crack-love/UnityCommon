/*#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;

/// <summary>
/// 2021-05-04 화 오전 11:54:52, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class YieldFactroy
    {
        static Dictionary<float, WaitForSeconds> m_wfsDic;
        static Dictionary<float, WaitForSecondsRealtime> m_wfsrtDic;
        static WaitForFixedUpdate m_wffu;
        static WaitForEndOfFrame m_wfeof;

        static YieldFactroy()
        {
            m_wfsDic = new Dictionary<float, WaitForSeconds>();
            m_wfsrtDic = new Dictionary<float, WaitForSecondsRealtime>();
        }

        public static WaitForSeconds WaitForSeconds(float sec)
        {
            if (!m_wfsDic.TryGetValue(sec, out var res))
            {
                res = new WaitForSeconds(sec);
                m_wfsDic.Add(sec, res);
            }

            return res;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime(float sec)
        {
            if (!m_wfsrtDic.TryGetValue(sec, out var res))
            {
                res = new WaitForSecondsRealtime(sec);
                m_wfsrtDic.Add(sec, res);
            }

            return res;
        }

        public static WaitForFixedUpdate WaitForFixedUpdate()
        {
            if (m_wffu == null) m_wffu = new WaitForFixedUpdate();

            return m_wffu;
        }

        public static WaitForEndOfFrame WaitForEndOfFrame()
        {
            if (m_wfeof == null) m_wfeof = new WaitForEndOfFrame();

            return m_wfeof;
        }

        public static WaitForUpdateSkipped WaitForUpdateSkipped(int skip)
        {
            if (!ArrayedPool<WaitForUpdateSkipped>.Instance.TryGet(out var res))
            {
                res = new WaitForUpdateSkipped(skip);
            }
            else
            {
                res.Set(skip);
            }

            return res;
        }
    }
}*/