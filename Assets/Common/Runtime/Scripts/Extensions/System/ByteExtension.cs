
/// <summary>
/// 2021-04-20 화 오후 9:31:36, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public static class ByteExtension
    {
        /// <summary>
        /// -127~127 to -1~1
        /// </summary>
        public static float ToFloatPercent(this sbyte x)
        {
            if (x < -127) x = -127;

            return (float)x / sbyte.MaxValue;
        }

        /// <summary>
        /// 0~255 to 0~1
        /// </summary>
        public static float ToFloatPercent(this byte x)
        {
            return (float)x / byte.MaxValue;
        }
    }
}