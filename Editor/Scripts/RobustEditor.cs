using UnityEngine;
using UnityEditor;

/// <summary>
/// 2022-03-20 오후 5:42:45, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon.Editors
{
    /// <summary>
    /// For robust engine api execution
    /// </summary>
    public static class RobustEditor
    {
        public static void DestroyComponent<T>(ref T cmp) 
            where T : Component
        {
            if (cmp)
            {
                Object.DestroyImmediate(cmp);
            }
            
            cmp = null;
        }

        public static void DestroyGameObject<T>(ref T cmp)
            where T : Component
        {
            if (cmp && cmp.gameObject)
            {
                Object.DestroyImmediate(cmp.gameObject);
            }

            cmp = null;
        }

        public static void DestroyGameObject(ref GameObject go)
        {
            if (go)
            {
                Object.DestroyImmediate(go);
            }

            go = null;
        }

        public static void DestroyObject<T>(ref T o) where T : Object
        {
            if (o)
            {
                Object.DestroyImmediate(o);
            }

            o = null;
        }

        public static void DestoryAsset<T>(ref T o) where T : Object
        {
            if (o)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(o));
            }

            o = null;
        }

        public static void SetDirty(Object src)
        {
            // make scene(.unity) or .asset dirty
            EditorUtility.SetDirty(src);

            // additinal set dirty prefab asset
            if (PrefabUtility.IsPartOfAnyPrefab(src))
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(src);
            }
        }
    }
}