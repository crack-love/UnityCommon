using UnityEngine;
using UnityCommon;

/// <summary>
/// 2020-09-11 금 오후 4:30:03, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public interface IPool<T>
    {
        public int Count { get; }

        public int MaxCapacity { get; set; }

        bool TryGet(out T value);

        bool TryReturn(in T value);
    }
}