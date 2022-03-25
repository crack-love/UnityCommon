using System;
using UnityEngine;

/// <summary>
/// 2022-03-25 오후 8:03:30, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common
{
    public static class Vector3ExtensionMath
    {
        //------------------------------------------------------------------------------------------------------------------
        // Avg Abs ...
        //------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Average of each elements
        /// </summary>
        public static float Avg(this Vector3 src)
        {
            return (src.x + src.y + src.z) / 3f;
        }

        public static Vector3 Abs(this Vector3 src)
        {
            src.x = Mathf.Abs(src.x);
            src.y = Mathf.Abs(src.y);
            src.z = Mathf.Abs(src.z);
            return src;
        }

        public static Vector3 Sign(this Vector3 src)
        {
            src.x = Math.Sign(src.x);
            src.y = Math.Sign(src.y);
            src.z = Math.Sign(src.z);
            return src;
        }
        public static Vector3Int CeilToInt(this Vector3 src)
        {
            Vector3Int res = Vector3Int.zero;
            res.x = Mathf.CeilToInt(src.x);
            res.y = Mathf.CeilToInt(src.y);
            res.z = Mathf.CeilToInt(src.z);

            return res;
        }

        public static Vector3Int FloorToInt(this Vector3 src)
        {
            Vector3Int res = Vector3Int.zero;
            res.x = Mathf.FloorToInt(src.x);
            res.y = Mathf.FloorToInt(src.y);
            res.z = Mathf.FloorToInt(src.z);

            return res;
        }

        public static Vector3Int RoundToInt(this Vector3 src)
        {
            Vector3Int res = Vector3Int.zero;
            res.x = Mathf.RoundToInt(src.x);
            res.y = Mathf.RoundToInt(src.y);
            res.z = Mathf.RoundToInt(src.z);

            return res;
        }

        //------------------------------------------------------------------------------------------------------------------
        // + - * / %
        //------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Sum each elements
        /// </summary>
        public static float Sum(this Vector3 src)
        {
            return src.x + src.y + src.z;
        }

        public static Vector3 Add(this Vector3 src, float v)
        {
            src.x += v;
            src.y += v;
            src.z += v;
            return src;
        }

        public static Vector3 Add(this Vector3 src, Vector3 v)
        {
            src.x += v.x;
            src.y += v.y;
            src.z += v.z;
            return src;
        }

        public static Vector3 Sub(this Vector3 src, float v)
        {
            src.x -= v;
            src.y -= v;
            src.z -= v;
            return src;
        }

        public static Vector3 Sub(this Vector3 src, Vector3 v)
        {
            src.x -= v.x;
            src.y -= v.y;
            src.z -= v.z;
            return src;
        }

        /// <summary>
        /// Multiply each elements
        /// </summary>
        public static float Mul(this Vector3 src)
        {
            return src.x * src.y * src.z;
        }

        /// <summary>
        /// return scaled Vector
        /// </summary>
        public static Vector3 Mul(this Vector3 src, float v)
        {
            src.x *= v;
            src.y *= v;
            src.z *= v;
            return src;
        }

        public static Vector3 Mul(this Vector3 src, Vector3 v)
        {
            src.x *= v.x;
            src.y *= v.y;
            src.z *= v.z;
            return src;
        }
        public static Vector3 Mul(this Vector3 src, float x, float y, float z)
        {
            src.x *= x;
            src.y *= y;
            src.z *= z;
            return src;
        }

        /// <summary>
        /// return scaled Vector
        /// </summary>
        public static Vector3 Div(this Vector3 src, float v)
        {
            src.x /= v;
            src.y /= v;
            src.z /= v;
            return src;
        }

        public static Vector3 Div(this Vector3 src, Vector3 v)
        {
            src.x /= v.x;
            src.y /= v.y;
            src.z /= v.z;
            return src;
        }

        public static Vector3 Mod(this Vector3 src, float v)
        {
            src.x %= v;
            src.y %= v;
            src.z %= v;
            return src;
        }

        public static Vector3 Mod(this Vector3 src, Vector3 v)
        {
            src.x %= v.x;
            src.y %= v.y;
            src.z %= v.z;
            return src;
        }
    }
}