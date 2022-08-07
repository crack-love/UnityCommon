using System;

namespace UnityCommon
{
    public static class IntExtension
    {
        public static int Min(this int self, int v)
        {
            return self < v ? self : v;
        }

        public static int Max(this int self, int v)
        {
            return self > v ? self : v;
        }

        public static int ClampMin(this int v, int min)
        {
            return Max(v, min);
        }

        public static int ClampMax(this int v, int max)
        {
            return Min(v, max);
        }

        public static int Clamp(this int v, int min, int max)
        {
            return v.Max(min).Min(max);
        }

        public static int Clamp01(this int v)
        {
            return v.Max(1).Min(0);
        }

        public static int Pow(this int v, int p)
        {
            return (int)Math.Pow(v, p);
        }

        public static float Pow(this int v, float p)
        {
            return (float)Math.Pow(v, p);
        }

    }
}