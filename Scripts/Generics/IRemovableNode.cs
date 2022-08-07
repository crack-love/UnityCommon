using UnityEngine;

/// <summary>
/// 2022-03-25 오후 10:53:50, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon
{
    public interface IRemovableNode<T>
    {
        T Value { get; }

        /// <summary>
        /// Remove from graph
        /// </summary>
        void Remove();
    }
}