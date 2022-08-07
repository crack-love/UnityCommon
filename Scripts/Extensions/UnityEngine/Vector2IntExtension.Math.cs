using UnityEngine;

/// <summary>
/// 2021-01-12 화 오후 5:47:59, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class Vector2IntExtensionMath
    {
        public static int Sum(this Vector2Int src)
        {
            return src.x + src.y;
        }
        public static Vector2Int Add(this Vector2Int src, int v)
        {
            src.x += v;
            src.y += v;
            return src;
        }
        public static Vector2 Add(this Vector2Int src, float v)
        {
            return new Vector2(src.x + v, src.y + v);
        }
        public static Vector2Int Add(this Vector2Int src, int x, int y)
        {
            src.x += x;
            src.y += y;
            return src;
        }
        public static Vector2 Add(this Vector2Int src, float x, float y)
        {
            return new Vector2(src.x + x, src.y + y);
        }
        public static Vector2Int Add(this Vector2Int src, Vector2Int v)
        {
            src.x += v.x;
            src.y += v.y;
            return src;
        }
        public static Vector2 Add(this Vector2Int src, Vector2 v)
        {
            return new Vector2(src.x + v.x, src.y + v.y);
        }
        public static Vector2Int Sub(this Vector2Int src, int v)
        {
            src.x -= v;
            src.y -= v;
            return src;
        }
        public static Vector2 Sub(this Vector2Int src, float v)
        {
            return new Vector2(src.x - v, src.y - v);
        }
        public static Vector2Int Sub(this Vector2Int src, int x, int y)
        {
            src.x -= x;
            src.y -= y;
            return src;
        }
        public static Vector2 Sub(this Vector2Int src, float x, float y)
        {
            return new Vector2(src.x - x, src.y - y);
        }
        public static Vector2Int Sub(this Vector2Int src, Vector2Int v)
        {
            src.x -= v.x;
            src.y -= v.y;
            return src;
        }
        public static Vector2 Sub(this Vector2Int src, Vector2 v)
        {
            return new Vector2(src.x - v.x, src.y - v.y);
        }
        public static Vector2Int Mul(this Vector2Int src, int v)
        {
            src.x *= v;
            src.y *= v;
            return src;
        }
        public static Vector2 Mul(this Vector2Int src, float v)
        {
            return new Vector2(src.x * v, src.y * v);
        }
        public static Vector2Int Mul(this Vector2Int src, int x, int y)
        {
            src.x *= x;
            src.y *= y;
            return src;
        }
        public static Vector2 Mul(this Vector2Int src, float x, float y)
        {
            return new Vector2(src.x * x, src.y * y);
        }
        public static Vector2Int Mul(this Vector2Int src, Vector2Int v)
        {
            src.x *= v.x;
            src.y *= v.y;
            return src;
        }
        public static Vector2 Mul(this Vector2Int src, Vector2 v)
        {
            return new Vector2(src.x * v.x, src.y * v.y);
        }
        public static Vector2Int Div(this Vector2Int src, int v)
        {
            src.x /= v;
            src.y /= v;
            return src;
        }
        public static Vector2 Div(this Vector2Int src, float v)
        {
            return new Vector2(src.x / v, src.y / v);
        }
        public static Vector2Int Div(this Vector2Int src, int x, int y)
        {
            src.x /= x;
            src.y /= y;
            return src;
        }
        public static Vector2 Div(this Vector2Int src, float x, float y)
        {
            return new Vector2(src.x / x, src.y / y);
        }
        public static Vector2 Div(this Vector2Int src, Vector2Int v)
        {
            src.x /= v.x;
            src.y /= v.y;
            return src;
        }
        public static Vector2 Div(this Vector2Int src, Vector2 v)
        {
            return new Vector2(src.x / v.x, src.y / v.y);
        }
        public static Vector2Int Mod(this Vector2Int src, int v)
        {
            src.x %= v;
            src.y %= v;
            return src;
        }
        public static Vector2 Mod(this Vector2Int src, float v)
        {
            return new Vector2(src.x % v, src.y % v);
        }
        public static Vector2Int Mod(this Vector2Int src, Vector2Int v)
        {
            src.x %= v.x;
            src.y %= v.y;
            return src;
        }
        public static Vector2 Mod(this Vector2Int src, Vector2 v)
        {
            return new Vector2(src.x % v.x, src.y % v.y);
        }
    }
}