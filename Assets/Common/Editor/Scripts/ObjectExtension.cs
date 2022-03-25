using UnityEditor;
using UnityEngine;

/// <summary>
/// 2021-05-10 월 오후 8:25:35, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace CommonEditor
{
    public static class ObjectExtension
    {
        public static T CreateAsset<T>(this T src) where T : Object
        {
            // change extension to .asset
            var name = typeof(T).Name;
            var start = name.LastIndexOf('.');
            if (start > 0)
            {
                name = name.Substring(start, name.Length - start);
            }

            var uniqPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/{name}.asset");

            AssetDatabase.CreateAsset(src, uniqPath);

            return src;
        }
    }
}