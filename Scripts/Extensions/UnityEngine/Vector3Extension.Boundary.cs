using System;
using UnityEngine;

namespace UnityCommon
{
    public static class Vector3ExtensionBoundary
    {        
        //------------------------------------------------------------------------------------------------------------------
        /// Min Max Clamp
        //-----------------------------------------------------------------------------------------------------------------

        public static Vector3 ClampMin(this Vector3 src, float min)
        {
            src.x = Mathf.Max(src.x, min);
            src.y = Mathf.Max(src.y, min);
            src.z = Mathf.Max(src.z, min);

            return src;
        }

        public static Vector3 ClampMin(this Vector3 src, Vector3 min)
        {
            src.x = Mathf.Max(src.x, min.x);
            src.y = Mathf.Max(src.y, min.y);
            src.z = Mathf.Max(src.z, min.z);

            return src;
        }

        public static Vector3 ClampMax(this Vector3 src, float max)
        {
            src.x = Mathf.Min(src.x, max);
            src.y = Mathf.Min(src.y, max);
            src.z = Mathf.Min(src.z, max);

            return src;
        }

        public static Vector3 ClampMax(this Vector3 src, Vector3 max)
        {
            src.x = Mathf.Min(src.x, max.x);
            src.y = Mathf.Min(src.y, max.y);
            src.z = Mathf.Min(src.z, max.z);

            return src;
        }

        public static Vector3 Clamp(this Vector3 src, float min, float max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3 Clamp(this Vector3 src, Vector3 min, float max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3 Clamp(this Vector3 src, float min, Vector3 max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        public static Vector3 Clamp(this Vector3 src, Vector3 min, Vector3 max)
        {
            return src.ClampMin(min).ClampMax(max);
        }

        /// <summary>
        /// Get max value of compare each elements
        /// </summary>
        public static float Max(this Vector3 src)
        {
            return Mathf.Max(src.x, src.y, src.z);
        }

        /// <summary>
        /// Get min value of compare each elements
        /// </summary>
        public static float Min(this Vector3 src)
        {
            return Mathf.Min(src.x, src.y, src.z);
        }
    }
}
