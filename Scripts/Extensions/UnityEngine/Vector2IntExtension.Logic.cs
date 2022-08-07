using UnityEngine;

/// <summary>
/// 2021-01-12 화 오후 5:47:59, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class Vector2IntExtensionLogic
    {
        public static bool Equals(this Vector2Int src, int v)
        {
            return src.x == v && src.y == v;
        }

        public static bool Equals(this Vector2Int src, int x, int y)
        {
            return src.x == x && src.y == y;
        }

        public static bool Equals(this Vector2Int src, Vector2Int v)
        {
            return src.x == v.x && src.y == v.y;
        }

        public static bool EqualsAny(this Vector2Int src, int v)
        {
            return src.x == v || src.y == v;
        }

        public static bool EqualsAny(this Vector2Int src, int x, int y)
        {
            return src.x == x || src.y == y;
        }

        public static bool EqualsAny(this Vector2Int src, Vector2Int v)
        {
            return src.x == v.x || src.y == v.y;
        }

        public static bool LessAny(this Vector2Int src, int v)
        {
            return src.x < v || src.y < v;
        }

        public static bool LessAny(this Vector2Int src, int x, int y)
        {
            return src.x < x || src.y < y;
        }

        public static bool LessAny(this Vector2Int src, Vector2Int v)
        {
            return src.x < v.x || src.y < v.y;
        }

        public static bool Less(this Vector2Int src, int v)
        {
            return src.x < v && src.y < v;
        }

        public static bool Less(this Vector2Int src, int x, int y)
        {
            return src.x < x && src.y < y;
        }

        public static bool Less(this Vector2Int src, Vector2Int v)
        {
            return src.x < v.x && src.y < v.y;
        }

        public static bool LessSameAny(this Vector2Int src, int v)
        {
            return src.x <= v || src.y <= v;
        }

        public static bool LessSameAny(this Vector2Int src, int x, int y)
        {
            return src.x <= x || src.y <= y;
        }

        public static bool LessSameAny(this Vector2Int src, Vector2Int v)
        {
            return src.x <= v.x || src.y <= v.y;
        }

        public static bool LessSame(this Vector2Int src, int v)
        {
            return src.x <= v && src.y <= v;
        }

        public static bool LessSame(this Vector2Int src, int x, int y)
        {
            return src.x <= x && src.y <= y;
        }

        public static bool LessSame(this Vector2Int src, Vector2Int v)
        {
            return src.x <= v.x && src.y <= v.y;
        }

        public static bool MoreAny(this Vector2Int src, int v)
        {
            return src.x > v || src.y > v;
        }

        public static bool MoreAny(this Vector2Int src, int x, int y)
        {
            return src.x > x || src.y > y;
        }

        public static bool MoreAny(this Vector2Int src, Vector2Int v)
        {
            return src.x > v.x || src.y > v.y;
        }

        public static bool More(this Vector2Int src, int v)
        {
            return src.x > v && src.y > v;
        }

        public static bool More(this Vector2Int src, int x, int y)
        {
            return src.x > x && src.y > y;
        }

        public static bool More(this Vector2Int src, Vector2Int v)
        {
            return src.x > v.x && src.y > v.y;
        }

        public static bool MoreSameAny(this Vector2Int src, int v)
        {
            return src.x >= v || src.y >= v;
        }

        public static bool MoreSameAny(this Vector2Int src, int x, int y)
        {
            return src.x >= x || src.y >= y;
        }

        public static bool MoreSameAny(this Vector2Int src, Vector2Int v)
        {
            return src.x >= v.x || src.y >= v.y;
        }

        public static bool MoreSame(this Vector2Int src, int v)
        {
            return src.x >= v && src.y >= v;
        }

        public static bool MoreSame(this Vector2Int src, int x, int y)
        {
            return src.x >= x && src.y >= y;
        }

        public static bool MoreSame(this Vector2Int src, Vector2Int v)
        {
            return src.x >= v.x && src.y >= v.y;
        }
    }
}