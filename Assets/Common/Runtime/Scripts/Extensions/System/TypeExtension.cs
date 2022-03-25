using System;

/// <summary>
/// 2020-09-23 수 오후 7:15:13, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public static class TypeExtension
    {
        public static bool IsEqualsOrSubclassOf(this Type self, Type src)
        {
            if (self.Equals(src))
            {
                return true;
            }
            else if (self.IsSubclassOf(src))
            {
                return true;
            }

            return false;
        }
    }
}