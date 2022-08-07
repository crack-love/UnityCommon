using UnityEngine;

/// <summary>
/// 2021-01-12 화 오후 5:47:59, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class Vector2Extension
    {
        /// <summary>
        /// Min value of x, y
        /// </summary>
        public static float Min(this Vector2 src)
        {
            return Mathf.Min(src.x, src.y);
        }

        /// <summary>
        /// Max value of x, y
        /// </summary>
        public static float Max(this Vector2 src)
        {
            return Mathf.Max(src.x, src.y);
        }

        public static Vector2 Clamp(this Vector2 src, float xMin, float xMax, float yMin, float yMax)
        {
            src.x = Mathf.Clamp(src.x, xMin, xMax);
            src.y = Mathf.Clamp(src.y, yMin, yMax);
            return src;
        }

        public static Vector2Int RoundToInt(this Vector2 src)
        {
            return new Vector2Int(Mathf.RoundToInt(src.x), Mathf.RoundToInt(src.y));
        }

        public static Vector2Int CeilToInt(this Vector2 src)
        {
            return new Vector2Int(Mathf.CeilToInt(src.x), Mathf.CeilToInt(src.y));
        }

        public static Vector2Int FloorToInt(this Vector2 src)
        {
            return new Vector2Int(Mathf.FloorToInt(src.x), Mathf.FloorToInt(src.y));
        }
    }
}