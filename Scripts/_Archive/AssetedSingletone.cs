/*#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 2021-04-19 월 오후 4:12:06, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    public abstract class AssetedSingletone<TDerived> : MonoBehaviour where TDerived : AssetedSingletone<TDerived>
    {
        // Ex) Assets/Dir0/Dir1
        protected static string RootDirPath = "Assets";

        static string s_filePath;
        static TDerived s_instance;

        static protected TDerived Instance
        {
            get
            {
                if (!s_instance)
                {
                    Load();
                }

                return s_instance;
            }
        }

        static void Load()
        {
            if (!s_instance)
            {
                // find
                s_instance = FindObjectOfType<TDerived>();

#if UNITY_EDITOR
                // load
                if (!s_instance)
                {
                    ValidatePath();
                    TDerived loadedAsset = null;

                    loadedAsset = AssetDatabase.LoadAssetAtPath<TDerived>(s_filePath);

                    // create
                    if (!loadedAsset)
                    {
                        var o = new GameObject("Temp", typeof(TDerived));

                        PrefabUtility.SaveAsPrefabAsset(o, s_filePath);
                        loadedAsset = AssetDatabase.LoadAssetAtPath<TDerived>(s_filePath);

                        o.DestroySelfGameObject();
                    }

                    // instantiate to scene
                    s_instance = Instantiate(loadedAsset);
                    s_instance.name = loadedAsset.name;

                    SaveAsset();
                }
#endif
                Check.When(!s_instance, "Load Fail. type:" + typeof(TDerived).Name);
            }
        }

        public static void SaveAsset()
        {
#if UNITY_EDITOR
            ValidatePath();

            Check.When(s_filePath.IsNullOrWhiteSpace(), "Invalid Path. type:" + typeof(TDerived).Name);
            Check.When(!Instance, "Saving Invalid Instance. type:" + typeof(TDerived).Name);

            PrefabUtility.SaveAsPrefabAssetAndConnect(s_instance.gameObject, s_filePath, InteractionMode.AutomatedAction).
                GetComponent<TDerived>();
#endif
        }

        static void ValidatePath()
        {
#if UNITY_EDITOR
            if (s_filePath.IsNullOrWhiteSpace())
            { 
                var split = AssetDatabaseEx.PathSpliter[0];

                s_filePath = DirPath + split
                    + typeof(TDerived).FullName.Replace('.', '/').Replace('\'', '_') + ".prefab";

                AssetDatabaseEx.ValidateAssetFilePathDir(s_filePath);

                Check.When(s_filePath.IsNullOrWhiteSpace());
            }
#endif
        }
    }
}*/