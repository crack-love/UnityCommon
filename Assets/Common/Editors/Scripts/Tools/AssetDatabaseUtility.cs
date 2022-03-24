using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// 2020-12-22 화 오후 7:16:33, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common.Editors
{
    public static class AssetDatabaseUtility
    {
        static readonly char[] PathSpliter = { System.IO.Path.AltDirectorySeparatorChar };

        /// <summary>
        /// dirPath:Assets/Dir0/Dir1(/)
        /// </summary>
        public static void ValidateAssetDirPath(string dirPath)
        {
            var dirs = dirPath.Split(PathSpliter, System.StringSplitOptions.RemoveEmptyEntries);
            
            ValidateAssetDirInternal(dirs, dirs.Length);
        }

        /// <summary>
        /// dirPath:Assets/Dir0/Dir1/File
        /// </summary>
        public static void ValidateAssetDirFilePath(string filePath)
        {
            var dirs = filePath.Split(PathSpliter, System.StringSplitOptions.RemoveEmptyEntries);

            ValidateAssetDirInternal(dirs, dirs.Length - 1);
        }

        static void ValidateAssetDirInternal(string[] dirs, int size)
        {
            string parentPath = "";
            string currentPath;

            for (int i = 0; i < size; ++i)
            {
                var dir = dirs[i];
                if (dir.Length <= 0) continue;

                if (parentPath.Length > 0)
                {
                    currentPath = parentPath + PathSpliter[0] + dir;
                }
                else
                {
                    currentPath = dir;
                }

                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    AssetDatabase.CreateFolder(parentPath, dir);
                }

                parentPath = currentPath;
            }
        }

        /// <summary>
        /// dir : "Assets/.."
        /// </summary>
        public static List<T> LoadAllAssetsAtDir<T>(params string[] dirs) where T : UnityEngine.Object
        {
            var list = new List<T>();
            LoadAllAssetsAtDir(list, dirs);
            return list;
        }

        /// <summary>
        /// dir : "Assets/.."
        /// </summary>
        public static void LoadAllAssetsAtDir<T>(List<T> buffer, params string[] dirs) where T : UnityEngine.Object
        {
            string[] guids = AssetDatabase.FindAssets(null, dirs);

            for (int i = 0; i < guids.Length; ++i)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);

                if (asset)
                {
                    buffer.Add(asset);
                }
            }
        }
    }
}