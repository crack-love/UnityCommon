using UnityEngine;

namespace Common
{
    public static class Vector3IntExtensionMath
    {
        //------------------------------------------------------------------------------------------------------------------
        // avg, abs ...
        //------------------------------------------------------------------------------------------------------------------

        public static float Avg(this Vector3Int src)
        {
            return (src.x + src.y + src.z) / 3f;
        }

        public static Vector3Int Abs(this Vector3Int src)
        {
            src.x = Mathf.Abs(src.x);
            src.y = Mathf.Abs(src.y);
            src.z = Mathf.Abs(src.z);
            return src;
        }

        //------------------------------------------------------------------------------------------------------------------
        // + - * / %
        //------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Sum each elements
        /// </summary>
        public static int Sum(this Vector3Int src)
        {
            return src.x + src.y + src.z;
        }
        public static Vector3 Add(this Vector3Int src, float v)
        {
            Vector3 res;
            res.x = src.x + v;
            res.y = src.y + v;
            res.z = src.z + v;
            return res;
        }
        public static Vector3Int Add(this Vector3Int src, int v)
        {
            src.x += v;
            src.y += v;
            src.z += v;
            return src;
        }
        public static Vector3 Add(this Vector3Int src, Vector3 v)
        {
            v.x = src.x + v.x;
            v.y = src.y + v.y;
            v.z = src.z + v.z;
            return v;
        }
        public static Vector3Int Add(this Vector3Int src, Vector3Int v)
        {
            v.x = src.x + v.x;
            v.y = src.y + v.y;
            v.z = src.z + v.z;
            return v;
        }
        public static Vector3 Add(this Vector3Int src, float x, float y, float z)
        {
            Vector3 res;
            res.x = src.x + x;
            res.y = src.y + y;
            res.z = src.z + z;
            return res;
        }
        public static Vector3Int Add(this Vector3Int src, int x, int y, int z)
        {
            src.x += x;
            src.y += y;
            src.z += z;
            return src;
        }
        public static Vector3 Sub(this Vector3Int src, float v)
        {
            Vector3 res;
            res.x = src.x - v;
            res.y = src.y - v;
            res.z = src.z - v;
            return res;
        }
        public static Vector3Int Sub(this Vector3Int src, int v)
        {
            src.x -= v;
            src.y -= v;
            src.z -= v;
            return src;
        }
        public static Vector3 Sub(this Vector3Int src, Vector3 v)
        {
            v.x = src.x - v.x;
            v.y = src.y - v.y;
            v.z = src.z - v.z;
            return v;
        }
        public static Vector3Int Sub(this Vector3Int src, Vector3Int v)
        {
            v.x = src.x - v.x;
            v.y = src.y - v.y;
            v.z = src.z - v.z;
            return v;
        }
        public static Vector3 Sub(this Vector3Int src, float x, float y, float z)
        {
            Vector3 res;
            res.x = src.x - x;
            res.y = src.y - y;
            res.z = src.z - z;
            return res;
        }
        public static Vector3Int Sub(this Vector3Int src, int x, int y, int z)
        {
            src.x -= x;
            src.y -= y;
            src.z -= z;
            return src;
        }
        /// <summary>
        /// Multiply each elements
        /// </summary>
        public static int Mul(this Vector3Int src)
        {
            return src.x * src.y * src.z;
        }
        public static Vector3 Mul(this Vector3Int src, float v)
        {
            Vector3 res;
            res.x = src.x * v;
            res.y = src.y * v;
            res.z = src.z * v;
            return res;
        }
        public static Vector3Int Mul(this Vector3Int src, int v)
        {
            src.x *= v;
            src.y *= v;
            src.z *= v;
            return src;
        }
        public static Vector3 Mul(this Vector3Int src, Vector3 v)
        {
            v.x = src.x * v.x;
            v.y = src.y * v.y;
            v.z = src.z * v.z;
            return v;
        }
        public static Vector3Int Mul(this Vector3Int src, Vector3Int v)
        {
            v.x = src.x * v.x;
            v.y = src.y * v.y;
            v.z = src.z * v.z;
            return v;
        }
        public static Vector3 Mul(this Vector3Int src, float x, float y, float z)
        {
            Vector3 v;
            v.x = src.x * x;
            v.y = src.y * y;
            v.z = src.z * z;
            return v;
        }
        public static Vector3Int Mul(this Vector3Int src, int x, int y, int z)
        {
            src.x *= x;
            src.y *= y;
            src.z *= z;
            return src;
        }
        public static Vector3 Div(this Vector3Int src, float v)
        {
            Vector3 res;
            res.x = src.x / v;
            res.y = src.y / v;
            res.z = src.z / v;
            return res;
        }
        public static Vector3Int Div(this Vector3Int src, int v)
        {
            src.x /= v;
            src.y /= v;
            src.z /= v;
            return src;
        }
        public static Vector3 Div(this Vector3Int src, Vector3 v)
        {
            v.x = src.x / v.x;
            v.y = src.y / v.y;
            v.z = src.z / v.z;
            return v;
        }
        public static Vector3Int Div(this Vector3Int src, Vector3Int v)
        {
            src.x /= v.x;
            src.y /= v.y;
            src.z /= v.z;
            return src;
        }
        public static Vector3 Div(this Vector3Int src, float x, float y, float z)
        {
            Vector3 v;
            v.x = src.x / x;
            v.y = src.y / y;
            v.z = src.z / z;
            return v;
        }
        public static Vector3Int Div(this Vector3Int src, int x, int y, int z)
        {
            src.x /= x;
            src.y /= y;
            src.z /= z;
            return src;
        }
        public static Vector3 Mod(this Vector3Int src, float v)
        {
            Vector3 res;
            res.x = src.x % v;
            res.y = src.y % v;
            res.z = src.z % v;
            return res;
        }
        public static Vector3Int Mod(this Vector3Int src, int v)
        {
            src.x %= v;
            src.y %= v;
            src.z %= v;
            return src;
        }
        public static Vector3 Mod(this Vector3Int src, Vector3 v)
        {
            Vector3 res;
            res.x = src.x % v.x;
            res.y = src.y % v.y;
            res.z = src.z % v.z;
            return res;
        }
        public static Vector3 Mod(this Vector3Int src, float x, float y, float z)
        {
            Vector3 res;
            res.x = src.x % x;
            res.y = src.y % y;
            res.z = src.z % z;
            return res;
        }
        public static Vector3Int Mod(this Vector3Int src, Vector3Int v)
        {
            src.x %= v.x;
            src.y %= v.y;
            src.z %= v.z;
            return src;
        }
        public static Vector3Int Mod(this Vector3Int src, int x, int y, int z)
        {
            src.x %= x;
            src.y %= y;
            src.z %= z;
            return src;
        }
    }
}
