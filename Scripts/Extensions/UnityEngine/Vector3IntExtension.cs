using UnityEngine;

namespace UnityCommon
{
    public static class Vector3IntExtension
    {
        /// <summary>
        /// move to dst scalar delta. x/y/z order
        /// </summary>
        public static Vector3Int Move(this Vector3Int src, Vector3Int dst, int stride)
        {
            var delta = dst - src;

            while (stride-- > 0 && delta != Vector3Int.zero)
            {
                if (delta.x > 0)
                {
                    src.x += 1;
                    delta.x -= 1;
                }
                else if (delta.x < 0)
                {
                    src.x -= 1;
                    delta.x += 1;
                }
                else if (delta.y > 0)
                {
                    src.y += 1;
                    delta.y -= 1;
                }
                else if (delta.y < 0)
                {
                    src.y -= 1;
                    delta.y += 1;
                }
                else if (delta.z > 0)
                {
                    src.z += 1;
                    delta.z -= 1;
                }
                else if (delta.z < 0)
                {
                    src.z -= 1;
                    delta.z += 1;
                }
            }

            return src;
        }
    }
}
