/*#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;

/// <summary>
/// 2021-05-04 화 오후 12:00:17, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class WaitForUpdateSkipped : CustomYieldInstruction
    {
        int m_skip = 0;

        public override bool keepWaiting
        {
            get
            {
                if (m_skip <= 0)
                {
                    ArrayedPool<WaitForUpdateSkipped>.Instance.TryReturn(this);

                    return false;
                }
                else
                {
                    m_skip -= 1;

                    return true;
                }
            }
        }

        public WaitForUpdateSkipped(int skip)
        {
            m_skip = skip;
        }

        public void Set(int skip)
        {
            m_skip = skip;
        }
    }
}*/