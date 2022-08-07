/// <summary>
/// 2020-12-16 수 오전 1:38:13, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str.Length <= 0;
        }

        /// <summary>
        /// Is all of string chars are white space
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (str == null) return true;

            int size = str.Length;
            if (size <= 0) return true;

            for (int i = 0; i < size; ++i)
            {
                if (!char.IsWhiteSpace(str, i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}