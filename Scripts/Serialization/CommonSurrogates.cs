using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// 2020-07-04 토 오후 8:37:28, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    partial class CommonSurrogateSelector
    {
        class Vector3S : CommonSurrogate<Vector3>
        {
            protected override void GetObjectData(Vector3 obj, SerializationInfo info, StreamingContext context)
            {
                info.AddValue("x", obj.x);
                info.AddValue("y", obj.y);
                info.AddValue("z", obj.z);
            }

            protected override Vector3 SetObjectData(Vector3 obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                obj.x = info.GetSingle("x");
                obj.y = info.GetSingle("y");
                obj.z = info.GetSingle("z");

                return obj;
            }
        }

        class Vector3IntS : CommonSurrogate<Vector3Int>
        {
            protected override void GetObjectData(Vector3Int obj, SerializationInfo info, StreamingContext context)
            {
                info.AddValue("x", obj.x);
                info.AddValue("y", obj.y);
                info.AddValue("z", obj.z);
            }

            protected override Vector3Int SetObjectData(Vector3Int obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                obj.x = info.GetInt32("x");
                obj.y = info.GetInt32("y");
                obj.z = info.GetInt32("z");

                return obj;
            }
        }

        class QuaternionS : CommonSurrogate<Quaternion>
        {
            protected override void GetObjectData(Quaternion obj, SerializationInfo info, StreamingContext context)
            {
                info.AddValue("x", obj.x);
                info.AddValue("y", obj.y);
                info.AddValue("z", obj.z);
                info.AddValue("w", obj.w);
            }

            protected override Quaternion SetObjectData(Quaternion obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                obj.x = info.GetSingle("x");
                obj.y = info.GetSingle("y");
                obj.z = info.GetSingle("z");
                obj.w = info.GetSingle("w");

                return obj;
            }
        }
    }
}