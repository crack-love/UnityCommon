using UnityEngine;

/// <summary>
/// 2022-03-20 오후 5:42:45, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common
{
    /// <summary>
    /// For robust engine api execution
    /// </summary>
    public static class Robust
    {
        public static void DestoryComponent<T>(ref T src, float sec = 0f) 
            where T : Component
        {
            if (src)
            {
                Object.Destroy(src, sec);
            }

            src = null;
        }

        public static void DestroyGameObject<T>(ref T src, float sec = 0f) 
            where T : Component
        {
            if (src && src.gameObject)
            {
                Object.Destroy(src.gameObject, sec);
            }

            src = null;
        }

        public static void DestroyGameObject(ref GameObject src, float sec = 0f)
        {
            if (src)
            {
                Object.Destroy(src, sec);
            }

            src = null;
        }

        public static void DontDestroyOnLoad(this Component src)
        {
            if (Application.isPlaying)
            {
                Object.DontDestroyOnLoad(src.gameObject);
            }
        }

        public static void DontDestroyOnLoad(this GameObject src)
        {
            if (Application.isPlaying)
            {
                Object.DontDestroyOnLoad(src);
            }
        }
    }
}