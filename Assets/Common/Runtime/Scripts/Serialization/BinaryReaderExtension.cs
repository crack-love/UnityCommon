using UnityEngine;

/// <summary>
/// 2021-04-05 월 오후 9:50:40, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public static class BinaryReaderExtension
    {
        public static void ReadTransform(this BinaryReader br, Transform src)
        {
            // localpos
            src.localPosition = br.ReadVector3();

            // localsale
            src.localScale = br.ReadVector3();

            // localrot
            src.localRotation = br.ReadQuaternion();
        }

        public static Vector3 ReadVector3(this BinaryReader br)
        {
            Vector3 res = default;
            res.x = br.ReadSingle();
            res.y = br.ReadSingle();
            res.z = br.ReadSingle();
            return res;
        }

        public static Vector3Int ReadVector3Int(this BinaryReader br)
        {
            Vector3Int res = default;
            res.x = br.ReadInt32();
            res.y = br.ReadInt32();
            res.z = br.ReadInt32();
            return res;
        }

        public static Quaternion ReadQuaternion(this BinaryReader br)
        {
            Quaternion res = default;
            res.x = br.ReadSingle();
            res.y = br.ReadSingle();
            res.z = br.ReadSingle();
            res.w = br.ReadSingle();
            return res;
        }
    }
}