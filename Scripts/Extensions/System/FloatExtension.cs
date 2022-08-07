using Math = System.Math;
using Assert = UnityEngine.Assertions.Assert;

namespace UnityCommon
{
    public static class FloatExtension
    {
        public class Internal
        {
            public static float Normal = 1.17549435E-38f;
            public static float Denormal = float.Epsilon;
            public static bool IsFlushToZeroEnable = (Denormal == 0);
        }

        public static readonly float Epsilon = Internal.IsFlushToZeroEnable ? Internal.Normal : Internal.Denormal;

        public static float Min(this float v, float other)
        {
            return v < other ? v : other;
        }
        public static float Max(this float v, float other)
        {
            return v > other ? v : other;
        }

        public static float ClampMin(this float v, float min)
        {
            return v < min ? min : v;
        }

        public static float ClampMax(this float v, float max)
        {
            return v > max ? max : v;
        }

        public static float Clamp(this float v, float min, float max)
        {
            Assert.IsTrue(min <= max);

            return (v < min ? min : v) > max ? max: v;
        }

        public static float Clamp01(this float v)
        {
            return (v < 0f ? 0f : v) > 1f ? 1f : v;
        }
        public static float Round(this float v)
        {
            return (float)Math.Round(v);
        }

        public static float Ceil(this float v)
        {
            return (float)Math.Ceiling(v);
        }

        public static float Floor(this float v)
        {
            return (float)Math.Floor(v);
        }

        public static float Pow(this float v, float p)
        {
            return (float)Math.Pow(v, p);
        }

        public static float Abs(this float v)
        {
            return (float)Math.Abs(v);
        }

        public static bool Approximately(this float a, float b)
        {
            return Abs(b - a) < Max(1e-6f * Max(Abs(a), Abs(b)), Epsilon * 8);
        }

        // Conversions /////////////////////////////////////////////////

        public static int ToMillisec(this float sec)
        {
            return (int)(sec * 1000);
        }

        /// <summary>
        /// 0~1 => 0~255
        /// </summary>
        public static byte ToBytePercent(this float x)
        {
            x = x.Clamp01();

            return (byte)(255f * x).Round();
        }

        /// <summary>
        /// -1~1 => -127~127
        /// </summary>
        public static sbyte ToSBytePercent(this float x)
        {
            x = x.Clamp(-1, 1);

            return (sbyte)(127f * x).Round();
        }
    }
}