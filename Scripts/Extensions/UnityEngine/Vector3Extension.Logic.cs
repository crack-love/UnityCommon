using System;
using UnityEngine;

/// <summary>
/// 2022-03-25 오후 8:03:30, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon
{
    public static class Vector3ExtensionLogic
    {
        //------------------------------------------------------------------------------------------------------------------
        /// Same Any Not More Less ...
        //------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Same All (Approximately)
        /// </summary>
        public static bool Same(this Vector3 v, float v2)
        {
            return Approximately(v, v2);
        }
        public static bool Same(this Vector3 v, Vector3 v2)
        {
            return Approximately(v, v2);
        }
        public static bool SameAny(this Vector3 v, float v2)
        {
            return ApproximatelyAny(v, v2);
        }
        public static bool SameAny(this Vector3 v, Vector3 v2)
        {
            return ApproximatelyAny(v, v2);
        }
        /// <summary>
        /// Not All (Approximately)
        /// </summary>
        public static bool Not(this Vector3 v, float v2)
        {
            return ApproximatelyNot(v, v2);
        }
        public static bool Not(this Vector3 v, Vector3 v2)
        {
            return ApproximatelyNot(v, v2);
        }
        public static bool NotAny(this Vector3 v, float v2)
        {
            return ApproximatelyNotAny(v, v2);
        }
        public static bool NotAny(this Vector3 v, Vector3 v2)
        {
            return ApproximatelyNotAny(v, v2);
        }
        /// <summary>
        /// Same All
        /// </summary>
        public static bool Approximately(this Vector3 v, float v2)
        {
            if (!Mathf.Approximately(v.x, v2)) return false;
            if (!Mathf.Approximately(v.y, v2)) return false;
            if (!Mathf.Approximately(v.z, v2)) return false;

            return true;
        }
        public static bool Approximately(this Vector3 v, Vector3 v2)
        {
            if (!Mathf.Approximately(v.x, v2.x)) return false;
            if (!Mathf.Approximately(v.y, v2.y)) return false;
            if (!Mathf.Approximately(v.z, v2.z)) return false;

            return true;
        }
        public static bool ApproximatelyAny(this Vector3 v, float v2)
        {
            if (Mathf.Approximately(v.x, v2)) return true;
            if (Mathf.Approximately(v.y, v2)) return true;
            if (Mathf.Approximately(v.z, v2)) return true;

            return true;
        }
        public static bool ApproximatelyAny(this Vector3 v, Vector3 v2)
        {
            if (Mathf.Approximately(v.x, v2.x)) return true;
            if (Mathf.Approximately(v.y, v2.y)) return true;
            if (Mathf.Approximately(v.z, v2.z)) return true;

            return true;
        }
        /// <summary>
        /// Not All
        /// </summary>
        public static bool ApproximatelyNot(this Vector3 v, float v2)
        {
            if (Mathf.Approximately(v.x, v2)) return false;
            if (Mathf.Approximately(v.y, v2)) return false;
            if (Mathf.Approximately(v.z, v2)) return false;

            return true;
        }
        public static bool ApproximatelyNot(this Vector3 v, Vector3 v2)
        {
            if (Mathf.Approximately(v.x, v2.x)) return false;
            if (Mathf.Approximately(v.y, v2.y)) return false;
            if (Mathf.Approximately(v.z, v2.z)) return false;

            return true;
        }
        public static bool ApproximatelyNotAny(this Vector3 v, float v2)
        {
            if (!Mathf.Approximately(v.x, v2)) return true;
            if (!Mathf.Approximately(v.y, v2)) return true;
            if (!Mathf.Approximately(v.z, v2)) return true;

            return true;
        }
        public static bool ApproximatelyNotAny(this Vector3 v, Vector3 v2)
        {
            if (!Mathf.Approximately(v.x, v2.x)) return true;
            if (!Mathf.Approximately(v.y, v2.y)) return true;
            if (!Mathf.Approximately(v.z, v2.z)) return true;

            return true;
        }
        /// <summary>
        /// More All
        /// </summary>
        public static bool More(this Vector3 v, float v2)
        {
            if (v.x <= v2) return false;
            if (v.y <= v2) return false;
            if (v.z <= v2) return false;

            return true;
        }
        public static bool More(this Vector3 v, Vector3 v2)
        {
            if (v.x <= v2.x) return false;
            if (v.y <= v2.y) return false;
            if (v.z <= v2.z) return false;

            return true;
        }
        public static bool MoreAny(this Vector3 v, float v2)
        {
            if (v.x > v2) return true;
            if (v.y > v2) return true;
            if (v.z > v2) return true;

            return false;
        }
        public static bool MoreAny(this Vector3 v, Vector3 v2)
        {
            if (v.x > v2.x) return true;
            if (v.y > v2.y) return true;
            if (v.z > v2.z) return true;

            return false;
        }
        /// <summary>
        /// More Same All
        /// </summary>
        public static bool MoreSame(this Vector3 v, float v2)
        {
            if (v.x < v2) return false;
            if (v.y < v2) return false;
            if (v.z < v2) return false;

            return true;
        }
        public static bool MoreSame(this Vector3 v, Vector3 v2)
        {
            if (v.x < v2.x) return false;
            if (v.y < v2.y) return false;
            if (v.z < v2.z) return false;

            return true;
        }
        public static bool MoreSameAny(this Vector3 v, float v2)
        {
            if (v.x >= v2) return true;
            if (v.y >= v2) return true;
            if (v.z >= v2) return true;

            return false;
        }
        public static bool MoreSameAny(this Vector3 v, Vector3 v2)
        {
            if (v.x >= v2.x) return true;
            if (v.y >= v2.y) return true;
            if (v.z >= v2.z) return true;

            return false;
        }
        /// <summary>
        /// Less All
        /// </summary>
        public static bool Less(this Vector3 v, float v2)
        {
            if (v.x >= v2) return false;
            if (v.y >= v2) return false;
            if (v.z >= v2) return false;

            return true;
        }
        public static bool Less(this Vector3 v, Vector3 v2)
        {
            if (v.x >= v2.x) return false;
            if (v.y >= v2.y) return false;
            if (v.z >= v2.z) return false;

            return true;
        }
        public static bool LessAny(this Vector3 v, float v2)
        {
            if (v.x < v2) return true;
            if (v.y < v2) return true;
            if (v.z < v2) return true;

            return false;
        }
        public static bool LessAny(this Vector3 v, Vector3 v2)
        {
            if (v.x < v2.x) return true;
            if (v.y < v2.y) return true;
            if (v.z < v2.z) return true;

            return false;
        }
        /// <summary>
        /// Less Same All
        /// </summary>
        public static bool LessSame(this Vector3 v, float v2)
        {
            if (v.x > v2) return false;
            if (v.y > v2) return false;
            if (v.z > v2) return false;

            return true;
        }
        public static bool LessSame(this Vector3 v, Vector3 v2)
        {
            if (v.x > v2.x) return false;
            if (v.y > v2.y) return false;
            if (v.z > v2.z) return false;

            return true;
        }
        public static bool LessSameAny(this Vector3 v, float v2)
        {
            if (v.x <= v2) return true;
            if (v.y <= v2) return true;
            if (v.z <= v2) return true;

            return false;
        }
        public static bool LessSameAny(this Vector3 v, Vector3 v2)
        {
            if (v.x <= v2.x) return true;
            if (v.y <= v2.y) return true;
            if (v.z <= v2.z) return true;

            return false;
        }
    }
}