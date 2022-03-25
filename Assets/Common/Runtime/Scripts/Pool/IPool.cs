using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-11 금 오후 4:30:03, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public interface IPool
    {
        int Count { get; }
        int MaxCapacity { get; set; }
    }

    public interface IPool<T> : IPool
    {
        bool TryGet(out T value);
        bool TryReturn(in T value);
    }
}