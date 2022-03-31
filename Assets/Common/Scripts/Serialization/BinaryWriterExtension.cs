using UnityEngine;

/// <summary>
/// 2021-04-05 월 오후 9:49:25, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// Unity Structure Support
    /// </summary>
    public static class BinaryWriterExtension
    {
        public static void Write(this BinaryWriter bw, Transform src)
        {
            // localpos
            bw.Write(src.localPosition);

            // localsale
            bw.Write(src.localScale);

            // localrot
            bw.Write(src.localRotation);
        }

        public static void Write(this BinaryWriter bw, Vector3 src)
        {
            bw.Write(src.x);
            bw.Write(src.y);
            bw.Write(src.z);
        }

        public static void Write(this BinaryWriter bw, Vector3Int src)
        {
            bw.Write(src.x);
            bw.Write(src.y);
            bw.Write(src.z);
        }

        public static void Write(this BinaryWriter bw, Quaternion src)
        {
            bw.Write(src.x);
            bw.Write(src.y);
            bw.Write(src.z);
            bw.Write(src.w);
        }
    }
}