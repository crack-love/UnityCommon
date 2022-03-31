using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-22 화 오후 8:11:25, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public enum PlayerLoopType
    {
        Initialization = 0,
        EarlyUpdate = 1,
        FixedUpdate = 2,
        PreUpdate = 3,
        Update = 4,
        PreLateUpdate = 5,
        PostLateUpdate = 6,
    }
}