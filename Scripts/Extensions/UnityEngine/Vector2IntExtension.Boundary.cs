using UnityEngine;

/// <summary>
/// 2021-01-12 화 오후 5:47:59, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class Vector2IntExtensionBoundary
    {
        /// <summary>
        /// Min value of each x, y
        /// </summary>
        public static int Min(this Vector2Int src)
        {
            return Mathf.Min(src.x, src.y);
        }

        /// <summary>
        /// Max value of each x, y
        /// </summary>
        public static int Max(this Vector2Int src)
        {
            return Mathf.Max(src.x, src.y);
        }

        public static Vector2Int Clamp(this Vector2Int src, int min, int max)
        {
            src.x = Mathf.Clamp(src.x, min, max);
            src.y = Mathf.Clamp(src.y, min, max);
            return src;
        }

        public static Vector2Int Clamp(this Vector2Int src, Vector2Int minMax)
        {
            src.x = Mathf.Clamp(src.x, minMax.x, minMax.y);
            src.y = Mathf.Clamp(src.y, minMax.x, minMax.y);
            return src;
        }

        public static Vector2Int Clamp(this Vector2Int src, Vector2Int min, Vector2Int max)
        {
            src.x = Mathf.Clamp(src.x, min.x, max.x);
            src.y = Mathf.Clamp(src.y, min.y, max.y);
            return src;
        }

        public static Vector2Int ClampMin(this Vector2Int src, int v)
        {
            src.x = Mathf.Max(src.x, v);
            src.y = Mathf.Max(src.y, v);
            return src;
        }

        public static Vector2Int ClampMin(this Vector2Int src, int x, int y)
        {
            src.x = Mathf.Max(src.x, x);
            src.y = Mathf.Max(src.y, y);
            return src;
        }

        public static Vector2Int ClampMin(this Vector2Int src, Vector2Int dst)
        {
            src.x = Mathf.Max(src.x, dst.x);
            src.y = Mathf.Max(src.y, dst.y);
            return src;
        }

        public static Vector2Int ClampMax(this Vector2Int src, int v)
        {
            src.x = Mathf.Min(src.x, v);
            src.y = Mathf.Min(src.y, v);
            return src;
        }

        public static Vector2Int ClampMax(this Vector2Int src, int x, int y)
        {
            src.x = Mathf.Min(src.x, x);
            src.y = Mathf.Min(src.y, y);
            return src;
        }

        public static Vector2Int ClampMax(this Vector2Int src, Vector2Int dst)
        {
            src.x = Mathf.Min(src.x, dst.x);
            src.y = Mathf.Min(src.y, dst.y);
            return src;
        }
    }
}