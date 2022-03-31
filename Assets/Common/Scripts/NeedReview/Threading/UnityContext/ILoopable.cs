using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-22 화 오후 8:10:45, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public interface ILoopable
    {
        /// <summary>
        /// Return false if finished
        /// </summary>
        bool MoveNext();
    }
}