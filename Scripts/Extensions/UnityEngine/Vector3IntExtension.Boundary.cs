using UnityEngine;

namespace UnityCommon
{
    public static class Vector3IntExtensionBoundary
    {
        //------------------------------------------------------------------------------------------------------------------
        /// Min Max Ceil Floor Round ...
        //-----------------------------------------------------------------------------------------------------------------

        public static Vector3Int ClampMin(this Vector3Int src, int v)
        {
            src.x = Mathf.Max(src.x, v);
            src.y = Mathf.Max(src.y, v);
            src.z = Mathf.Max(src.z, v);

            return src;
        }

        public static Vector3Int ClampMin(this Vector3Int src, Vector3Int v)
        {
            src.x = Mathf.Max(src.x, v.x);
            src.y = Mathf.Max(src.y, v.y);
            src.z = Mathf.Max(src.z, v.z);

            return src;
        }

        public static Vector3Int ClampMax(this Vector3Int src, int v)
        {
            src.x = Mathf.Min(src.x, v);
            src.y = Mathf.Min(src.y, v);
            src.z = Mathf.Min(src.z, v);

            return src;
        }

        public static Vector3Int ClampMax(this Vector3Int src, Vector3Int v)
        {
            src.x = Mathf.Min(src.x, v.x);
            src.y = Mathf.Min(src.y, v.y);
            src.z = Mathf.Min(src.z, v.z);

            return src;
        }

        public static Vector3Int Clamp(this Vector3Int src, int min, int max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3Int Clamp(this Vector3Int src, Vector3Int min, int max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3Int Clamp(this Vector3Int src, int min, Vector3Int max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3Int Clamp(this Vector3Int src, Vector3Int min, Vector3Int max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        /// <summary>
        /// Get max value of compare each elements
        /// </summary>
        public static int Max(this Vector3Int src)
        {
            return Mathf.Max(src.x, src.y, src.z);
        }

        /// <summary>
        /// Get min value of compare each elements
        /// </summary>
        public static int Min(this Vector3Int src)
        {
            return Mathf.Min(src.x, src.y, src.z);
        }
    }
}
