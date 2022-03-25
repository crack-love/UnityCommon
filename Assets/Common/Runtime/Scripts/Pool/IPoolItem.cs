using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-11 금 오후 4:30:03, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public interface IPoolItem
    {
        /// <summary>
        /// Clear and return to pool
        /// </summary>
        /// <returns>Is successfully returned to pool</returns>
        bool ClearReturn();
    }

    public interface IPoolItemCallback : IPoolItem
    {
        void OnEnpool();

        void OnDepool();
    }

    public interface ILinkedPoolItem<TDerived> : IPoolItem where TDerived : ILinkedPoolItem<TDerived>
    {
        TDerived NextPoolItem { get; set; }
    }

    public interface ILinkedPoolItemCallback<TDerived> : ILinkedPoolItem<TDerived>, IPoolItemCallback where TDerived : ILinkedPoolItemCallback<TDerived>
    {

    }
}