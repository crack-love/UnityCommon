using UnityEngine;
using UnityCommon;

/// <summary>
/// 2021-01-21 목 오후 5:44:31, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public interface IValidatable
    {
        void Validate();
    }

    /// <summary>
    /// State is caller
    /// </summary>
    public interface IValidatableState
    {
        void Validate(object state);
    }
}