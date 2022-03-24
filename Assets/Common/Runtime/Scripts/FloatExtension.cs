using UnityEngine;

/// <summary>
/// 2022-03-20 오후 6:03:20, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common
{
    public static class FloatExtension
    {
        public static int ToMillisec(this float sec)
        {
            return (int)(sec * 1000);
        }
    }
}