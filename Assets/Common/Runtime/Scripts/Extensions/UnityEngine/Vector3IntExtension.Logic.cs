using UnityEngine;

namespace Common
{
    public static class Vector3IntExtensionLogic
    {
        //------------------------------------------------------------------------------------------------------------------
        /// Same Any Not More Less ...
        //------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Same All (Approximately)
        /// </summary>
        public static bool Same(this Vector3Int v, float v2)
        {
            return Approximately(v, v2);
        }
        public static bool Same(this Vector3Int v, int v2)
        {
            if (v.x != v2) return false;
            if (v.y != v2) return false;
            if (v.z != v2) return false;

            return true;
        }
        public static bool Same(this Vector3Int v, Vector3 v2)
        {
            return Approximately(v, v2);
        }
        public static bool Same(this Vector3Int v, Vector3Int v2)
        {
            if (v.x != v2.x) return false;
            if (v.y != v2.y) return false;
            if (v.z != v2.z) return false;

            return true;
        }
        /// <summary>
        /// Same Any (Approximately)
        /// </summary>
        public static bool SameAny(this Vector3Int v, float v2)
        {
            return ApproximatelyAny(v, v2);
        }
        public static bool SameAny(this Vector3Int v, int v2)
        {
            if (v.x == v2) return true;
            if (v.y == v2) return true;
            if (v.z == v2) return true;

            return false;
        }
        public static bool SameAny(this Vector3Int v, Vector3 v2)
        {
            return ApproximatelyAny(v, v2);
        }
        public static bool SameAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x == v2.x) return true;
            if (v.y == v2.y) return true;
            if (v.z == v2.z) return true;

            return false;
        }
        /// <summary>
        /// Not All (Approximately)
        /// </summary>
        public static bool Not(this Vector3Int v, float v2)
        {
            return ApproximatelyNot(v, v2);
        }
        public static bool Not(this Vector3Int v, int v2)
        {
            if (v.x == v2) return false;
            if (v.y == v2) return false;
            if (v.z == v2) return false;

            return true;
        }
        public static bool Not(this Vector3Int v, Vector3 v2)
        {
            return ApproximatelyNot(v, v2);
        }
        public static bool Not(this Vector3Int v, Vector3Int v2)
        {
            if (v.x == v2.x) return false;
            if (v.y == v2.y) return false;
            if (v.z == v2.z) return false;

            return true;
        }
        /// <summary>
        /// Not Any (Approximately)
        /// </summary>
        public static bool NotAny(this Vector3Int v, float v2)
        {
            return ApproximatelyNotAny(v, v2);
        }
        public static bool NotAny(this Vector3Int v, int v2)
        {
            if (v.x != v2) return true;
            if (v.x != v2) return true;
            if (v.x != v2) return true;

            return false;
        }
        public static bool NotAny(this Vector3Int v, Vector3 v2)
        {
            return ApproximatelyNotAny(v, v2);
        }
        public static bool NotAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x != v2.x) return true;
            if (v.x != v2.y) return true;
            if (v.x != v2.z) return true;

            return false;
        }
        /// <summary>
        /// Same All
        /// </summary>
        public static bool Approximately(this Vector3Int v, float v2)
        {
            if (!Mathf.Approximately(v.x, v2)) return false;
            if (!Mathf.Approximately(v.y, v2)) return false;
            if (!Mathf.Approximately(v.z, v2)) return false;

            return true;
        }
        public static bool Approximately(this Vector3Int v, Vector3 v2)
        {
            if (!Mathf.Approximately(v.x, v2.x)) return false;
            if (!Mathf.Approximately(v.y, v2.y)) return false;
            if (!Mathf.Approximately(v.z, v2.z)) return false;

            return true;
        }
        /// <summary>
        /// Same Any
        /// </summary>
        public static bool ApproximatelyAny(this Vector3Int v, float v2)
        {
            if (Mathf.Approximately(v.x, v2)) return true;
            if (Mathf.Approximately(v.y, v2)) return true;
            if (Mathf.Approximately(v.z, v2)) return true;

            return true;
        }
        public static bool ApproximatelyAny(this Vector3Int v, Vector3 v2)
        {
            if (Mathf.Approximately(v.x, v2.x)) return true;
            if (Mathf.Approximately(v.y, v2.y)) return true;
            if (Mathf.Approximately(v.z, v2.z)) return true;

            return true;
        }
        /// <summary>
        /// Not All
        /// </summary>
        public static bool ApproximatelyNot(this Vector3Int v, float v2)
        {
            if (Mathf.Approximately(v.x, v2)) return false;
            if (Mathf.Approximately(v.y, v2)) return false;
            if (Mathf.Approximately(v.z, v2)) return false;

            return true;
        }
        public static bool ApproximatelyNot(this Vector3Int v, Vector3 v2)
        {
            if (Mathf.Approximately(v.x, v2.x)) return false;
            if (Mathf.Approximately(v.y, v2.y)) return false;
            if (Mathf.Approximately(v.z, v2.z)) return false;

            return true;
        }
        /// <summary>
        /// Not Any
        /// </summary>
        public static bool ApproximatelyNotAny(this Vector3Int v, float v2)
        {
            if (!Mathf.Approximately(v.x, v2)) return true;
            if (!Mathf.Approximately(v.y, v2)) return true;
            if (!Mathf.Approximately(v.z, v2)) return true;

            return true;
        }
        public static bool ApproximatelyNotAny(this Vector3Int v, Vector3 v2)
        {
            if (!Mathf.Approximately(v.x, v2.x)) return true;
            if (!Mathf.Approximately(v.y, v2.y)) return true;
            if (!Mathf.Approximately(v.z, v2.z)) return true;

            return true;
        }
        /// <summary>
        /// More All
        /// </summary>
        public static bool More(this Vector3Int v, float v2)
        {
            if (v.x <= v2) return false;
            if (v.y <= v2) return false;
            if (v.z <= v2) return false;

            return true;
        }
        public static bool More(this Vector3Int v, int v2)
        {
            if (v.x <= v2) return false;
            if (v.y <= v2) return false;
            if (v.z <= v2) return false;

            return true;
        }
        public static bool More(this Vector3Int v, Vector3 v2)
        {
            if (v.x <= v2.x) return false;
            if (v.y <= v2.y) return false;
            if (v.z <= v2.z) return false;

            return true;
        }
        public static bool More(this Vector3Int v, Vector3Int v2)
        {
            if (v.x <= v2.x) return false;
            if (v.y <= v2.y) return false;
            if (v.z <= v2.z) return false;

            return true;
        }
        /// <summary>
        /// More Any
        /// </summary>
        public static bool MoreAny(this Vector3Int v, float v2)
        {
            if (v.x > v2) return true;
            if (v.y > v2) return true;
            if (v.z > v2) return true;

            return false;
        }
        public static bool MoreAny(this Vector3Int v, int v2)
        {
            if (v.x > v2) return true;
            if (v.y > v2) return true;
            if (v.z > v2) return true;

            return false;
        }
        public static bool MoreAny(this Vector3Int v, Vector3 v2)
        {
            if (v.x > v2.x) return true;
            if (v.y > v2.y) return true;
            if (v.z > v2.z) return true;

            return false;
        }
        public static bool MoreAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x > v2.x) return true;
            if (v.y > v2.y) return true;
            if (v.z > v2.z) return true;

            return false;
        }
        /// <summary>
        /// More Same All
        /// </summary>
        public static bool MoreSame(this Vector3Int v, float v2)
        {
            if (v.x < v2) return false;
            if (v.y < v2) return false;
            if (v.z < v2) return false;

            return true;
        }
        public static bool MoreSame(this Vector3Int v, int v2)
        {
            if (v.x < v2) return false;
            if (v.y < v2) return false;
            if (v.z < v2) return false;

            return true;
        }
        public static bool MoreSame(this Vector3Int v, Vector3 v2)
        {
            if (v.x < v2.x) return false;
            if (v.y < v2.y) return false;
            if (v.z < v2.z) return false;

            return true;
        }
        public static bool MoreSame(this Vector3Int v, Vector3Int v2)
        {
            if (v.x < v2.x) return false;
            if (v.y < v2.y) return false;
            if (v.z < v2.z) return false;

            return true;
        }
        /// <summary>
        /// More Same Any
        /// </summary>
        public static bool MoreSameAny(this Vector3Int v, float v2)
        {
            if (v.x >= v2) return true;
            if (v.y >= v2) return true;
            if (v.z >= v2) return true;

            return false;
        }
        public static bool MoreSameAny(this Vector3Int v, int v2)
        {
            if (v.x >= v2) return true;
            if (v.y >= v2) return true;
            if (v.z >= v2) return true;

            return false;
        }
        public static bool MoreSameAny(this Vector3Int v, Vector3 v2)
        {
            if (v.x >= v2.x) return true;
            if (v.y >= v2.y) return true;
            if (v.z >= v2.z) return true;

            return false;
        }
        public static bool MoreSameAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x >= v2.x) return true;
            if (v.y >= v2.y) return true;
            if (v.z >= v2.z) return true;

            return false;
        }
        /// <summary>
        /// Less All
        /// </summary>
        public static bool Less(this Vector3Int v, float v2)
        {
            if (v.x >= v2) return false;
            if (v.y >= v2) return false;
            if (v.z >= v2) return false;

            return true;
        }
        public static bool Less(this Vector3Int v, int v2)
        {
            if (v.x >= v2) return false;
            if (v.y >= v2) return false;
            if (v.z >= v2) return false;

            return true;
        }
        public static bool Less(this Vector3Int v, Vector3 v2)
        {
            if (v.x >= v2.x) return false;
            if (v.y >= v2.y) return false;
            if (v.z >= v2.z) return false;

            return true;
        }
        public static bool Less(this Vector3Int v, Vector3Int v2)
        {
            if (v.x >= v2.x) return false;
            if (v.y >= v2.y) return false;
            if (v.z >= v2.z) return false;

            return true;
        }
        /// <summary>
        /// Less Any
        /// </summary>
        public static bool LessAny(this Vector3Int v, float v2)
        {
            if (v.x < v2) return true;
            if (v.y < v2) return true;
            if (v.z < v2) return true;

            return false;
        }
        public static bool LessAny(this Vector3Int v, int v2)
        {
            if (v.x < v2) return true;
            if (v.y < v2) return true;
            if (v.z < v2) return true;

            return false;
        }
        public static bool LessAny(this Vector3Int v, Vector3 v2)
        {
            if (v.x < v2.x) return true;
            if (v.y < v2.y) return true;
            if (v.z < v2.z) return true;

            return false;
        }
        public static bool LessAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x < v2.x) return true;
            if (v.y < v2.y) return true;
            if (v.z < v2.z) return true;

            return false;
        }
        /// <summary>
        /// Less Same All
        /// </summary>
        public static bool LessSame(this Vector3Int v, float v2)
        {
            if (v.x > v2) return false;
            if (v.y > v2) return false;
            if (v.z > v2) return false;

            return true;
        }
        public static bool LessSame(this Vector3Int v, int v2)
        {
            if (v.x > v2) return false;
            if (v.y > v2) return false;
            if (v.z > v2) return false;

            return true;
        }
        public static bool LessSame(this Vector3Int v, Vector3 v2)
        {
            if (v.x > v2.x) return false;
            if (v.y > v2.y) return false;
            if (v.z > v2.z) return false;

            return true;
        }
        public static bool LessSame(this Vector3Int v, Vector3Int v2)
        {
            if (v.x > v2.x) return false;
            if (v.y > v2.y) return false;
            if (v.z > v2.z) return false;

            return true;
        }
        /// <summary>
        /// Less Same Any
        /// </summary>
        public static bool LessSameAny(this Vector3Int v, float v2)
        {
            if (v.x <= v2) return true;
            if (v.y <= v2) return true;
            if (v.z <= v2) return true;

            return false;
        }
        public static bool LessSameAny(this Vector3Int v, int v2)
        {
            if (v.x <= v2) return true;
            if (v.y <= v2) return true;
            if (v.z <= v2) return true;

            return false;
        }
        public static bool LessSameAny(this Vector3Int v, Vector3Int v2)
        {
            if (v.x <= v2.x) return true;
            if (v.y <= v2.y) return true;
            if (v.z <= v2.z) return true;

            return false;
        }
        public static bool LessSameAny(this Vector3Int v, Vector3 v2)
        {
            if (v.x <= v2.x) return true;
            if (v.y <= v2.y) return true;
            if (v.z <= v2.z) return true;

            return false;
        }
    }
}
