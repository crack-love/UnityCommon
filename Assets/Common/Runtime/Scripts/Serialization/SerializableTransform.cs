using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2020-07-08 수 오후 6:41:42, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Position, Rotation, LocalScale
    /// </summary>
    [Serializable]
    public struct SerializableTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 LocalScale;

        public SerializableTransform(Transform src)
        {
            Position = src.position;
            Rotation = src.rotation;
            LocalScale = src.localScale;
        }

        public void CopyFrom(Transform src)
        {
            Position = src.position;
            Rotation = src.rotation;
            LocalScale = src.localScale;
        }

        public void CopyTo(Transform dst)
        {
            dst.position = Position;
            dst.rotation = Rotation;
            dst.localScale = LocalScale;
        }

        public static implicit operator SerializableTransform(Transform src)
        {
            return new SerializableTransform(src);
        }
    }
}