using UnityEngine;

namespace UnityCommon
{
    public static class Vector3Extension
    {
        //------------------------------------------------------------------------------------------------------------------
        /// NaN, Infinity ...
        //------------------------------------------------------------------------------------------------------------------

        public static bool IsNaNAny(this Vector3 src)
        {
            return float.IsNaN(src.x) || float.IsNaN(src.y) || float.IsNaN(src.z);
        }

        public static bool IsInfinityAny(this Vector3 src)
        {
            return float.IsInfinity(src.x) || float.IsInfinity(src.y) || float.IsInfinity(src.z);
        }
    }
}
