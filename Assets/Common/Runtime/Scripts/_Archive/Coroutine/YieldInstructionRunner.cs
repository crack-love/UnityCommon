/*#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections;

/// <summary>
/// 2021-04-29 목 오후 1:41:22, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{    
    /// <summary>
    /// Runing UnityEngine Yields:
    /// WaitForEndOfFrame;
    /// WaitForFixedUpdate;
    /// WaitForSeconds;
    /// </summary>
    [ExecuteAlways]
    class YieldInstructionRunner : MonoBehaviourSingletone<YieldInstructionRunner>
    {
        public static IEnumerator StartYieldInstruction(YieldInstruction src)
        {
#if UNITY_EDITOR
            // Editor scope coroutine is unexpectable
            // skip frame whatever source is
            if (!Application.isPlaying)
            {
                return new WaitForUpdateSkipped(1);
            }
#endif
            var res = new YieldInstructionWaiting();

            Instance.StartCoroutine(res.Wait(src));

            return res;
        }
    }

    class YieldInstructionWaiting : CustomYieldInstruction
    {
        bool m_isWaiting;

        public override bool keepWaiting => m_isWaiting;

        public IEnumerator Wait(YieldInstruction src)
        {
            m_isWaiting = true;

            yield return src;

            m_isWaiting = false;
        }
    }
}*/