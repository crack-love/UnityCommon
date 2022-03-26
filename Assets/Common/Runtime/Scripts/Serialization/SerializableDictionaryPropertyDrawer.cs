#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;

/// <summary>
/// 2021-04-13 화 오후 7:48:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    internal class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        static bool s_foldOut = false;
        static bool s_addFoldOut = false;

        static GUIStyle foldoutStyle;
        static GUIStyle headerStyle;
        static GUIStyle cellStyle;
        static GUIStyle botStyle;

        static SerializableDictionaryPropertyDrawer()
        {
            foldoutStyle = new GUIStyle(GUI.skin.GetStyle("foldOut"));
            headerStyle = new GUIStyle(GUI.skin.button);
            cellStyle = new GUIStyle(GUI.skin.button);
            botStyle = new GUIStyle(GUI.skin.button);
        }

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            var foIndent = 11;
            
            // foldour
            var foRect = new Rect(pos.x, pos.y, pos.width, foldoutStyle.CalcHeight(label, pos.width));
            s_foldOut = EditorGUI.Foldout(foRect, s_foldOut, label, true, foldoutStyle);
            if (s_foldOut)
            {
                // properties
                var keys = property.FindPropertyRelative("m_serializedKeys");
                var values = property.FindPropertyRelative("m_serializedValues");
                Check.When(keys == null, "KeyType is not serializable");
                Check.When(values == null, "ValueType is not serializable");
                Check.When(!keys.isArray, "Not Array");
                Check.When(!values.isArray, "Not Array");
                var size = keys.arraySize;

                // !!! max 100
                size = Math.Min(size, 100);


                // table haeders
                var width = foRect.width;
                var height = headerStyle.CalcHeight(GUIContent.none, 0);
                var h0Rect = new Rect(foRect.xMin, foRect.yMax, width / 2 - 20/2, height);
                var h1Rect = new Rect(h0Rect.xMax, foRect.yMax, width / 2 - 20/2, height);
                var h3Rect = new Rect(h1Rect.xMax, foRect.yMax, 20, height);
                GUI.Button(h0Rect, "Key", headerStyle);
                GUI.Button(h1Rect, "Value", headerStyle);
                GUI.Button(h3Rect, "", headerStyle); // remove btn

                var last0Rect = h0Rect;
                    
                // enumer array
                for (int i = 0; i < size; ++i)
                {
                    // properties
                    var key = keys.GetArrayElementAtIndex(i);
                    var val = values.GetArrayElementAtIndex(i);
                    //var use = isFills.GetArrayElementAtIndex(i);

                    var keyHeight = EditorGUI.GetPropertyHeight(key, GUIContent.none, true);
                    var valHeight = EditorGUI.GetPropertyHeight(val, GUIContent.none, true);
                    //var useHeight = EditorGUI.GetPropertyHeight(use, GUIContent.none, true);
                    var rmvHeight = cellStyle.CalcHeight(GUIContent.none, 0);
                    //var maxHeight = Mathf.Max(keyHeight, valHeight, useHeight, rmvHeight);
                    var maxHeight = Mathf.Max(keyHeight, valHeight, rmvHeight);

                    var c0Rect = new Rect(last0Rect.x, last0Rect.yMax, width / 2 - 20/2, maxHeight);
                    var c1Rect = new Rect(c0Rect.xMax, last0Rect.yMax, width / 2 - 20/2, maxHeight);
                    //var c2Rect = new Rect(c1Rect.xMax, last0Rect.yMax, 40, maxHeight);
                    var c3Rect = new Rect(c1Rect.xMax, last0Rect.yMax, 20, maxHeight);

                    // set value field prefix label width
                    var temp = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = c1Rect.width / 3;

                    EditorGUI.PropertyField(c0Rect, key, GUIContent.none, true);
                    EditorGUI.PropertyField(c1Rect, val, GUIContent.none, true);
                    //EditorGUI.PropertyField(c2Rect, use, GUIContent.none, true);
                    if (GUI.Button(c3Rect, "X", cellStyle))
                    {
                        keys.DeleteArrayElementAtIndex(i);
                        values.DeleteArrayElementAtIndex(i);

                        keys.arraySize -= 1;
                        values.arraySize -= 1;
                    }
                    
                    EditorGUIUtility.labelWidth = temp;
                    last0Rect = c0Rect;
                }

                AddingGUI();

                // adding
                void AddingGUI()
                {
                    var addFoLable = new GUIContent("Add");
                    var addFoRect = new Rect(last0Rect.x + foIndent, last0Rect.yMax, width, foldoutStyle.CalcHeight(addFoLable, width));
                    s_addFoldOut = EditorGUI.Foldout(addFoRect, s_addFoldOut, addFoLable, true, foldoutStyle);
                    addFoRect.x -= foIndent;
                    last0Rect = addFoRect;
                    
                    if (s_addFoldOut)
                    {
                        var kp = property.FindPropertyRelative("m_addingKey");
                        var vp = property.FindPropertyRelative("m_addingValue");
                        var ap = property.FindPropertyRelative("m_adding"); // btn
                        var keyHeight = EditorGUI.GetPropertyHeight(kp, GUIContent.none, true);
                        var valHeight = EditorGUI.GetPropertyHeight(vp, GUIContent.none, true);
                        var maxHeight = Mathf.Max(keyHeight, valHeight);
                        var c0Rect = new Rect(last0Rect.x, last0Rect.yMax, width / 2 - 20 / 2, maxHeight);
                        var c1Rect = new Rect(c0Rect.xMax, last0Rect.yMax, width / 2 - 20 / 2, maxHeight);
                        EditorGUI.PropertyField(c0Rect, kp, GUIContent.none, true);
                        EditorGUI.PropertyField(c1Rect, vp, GUIContent.none, true);
                        last0Rect = c0Rect;

                        var btnHeight = headerStyle.CalcHeight(GUIContent.none, 0);
                        var btnRect = new Rect(last0Rect.x, last0Rect.yMax, width - 20, btnHeight);
                        if (GUI.Button(btnRect, "Add"))
                        {
                            ap.boolValue = true;
                        }
                    }
                }
                /*
                var addFoLable = new GUIContent("Add"); 
                var addFoRect = new Rect(last0Rect.x + foIndent, last0Rect.yMax, width - foIndent, foldoutStyle.CalcHeight(addFoLable, width - foIndent));
                s_addFoldOut = EditorGUI.Foldout(addFoRect, s_addFoldOut, addFoLable, true, foldoutStyle);
                last0Rect = addFoRect;
                if (s_addFoldOut)
                {
                    // add btn
                    var botHeight = botStyle.CalcHeight(GUIContent.none, 0);
                    var botRect = new Rect(last0Rect.x, last0Rect.yMax, last0Rect.width, botHeight);
                    if (GUI.Button(botRect, "Add", botStyle))
                    {
                        size += 1;
                        keys.arraySize = size;
                        values.arraySize = size;
                    }
                    last0Rect = botRect;

                    // add property
                    var addingKey = property.FindPropertyRelative("m_addingKey");
                    var addingValue = property.FindPropertyRelative("m_addingValue");

                    var akeyHeight = EditorGUI.GetPropertyHeight(addingKey, GUIContent.none, true);
                    var avalHeight = EditorGUI.GetPropertyHeight(addingValue, GUIContent.none, true);
                    var armvHeight = cellStyle.CalcHeight(GUIContent.none, 0);
                    var amaxHeight = Mathf.Max(akeyHeight, avalHeight, armvHeight);
                    var ac0Rect = new Rect(last0Rect.x, last0Rect.yMax, last0Rect.width / 2, amaxHeight);
                    var ac1Rect = new Rect(ac0Rect.xMax, last0Rect.yMax, last0Rect.width / 2, amaxHeight);

                    EditorGUI.PropertyField(ac0Rect, addingKey, GUIContent.none, true);
                    EditorGUI.PropertyField(ac1Rect, addingValue, GUIContent.none, true);
                }
                */

                // apply
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        class AddEditorWindow : EditorWindow
        {
            public SerializedProperty KeyProperty;
            public SerializedProperty ValueProperty;

            private void OnGUI()
            {
                EditorGUILayout.PropertyField(KeyProperty);
                EditorGUILayout.PropertyField(ValueProperty);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float res = 0;
            var keys = property.FindPropertyRelative("m_serializedKeys");
            var vals = property.FindPropertyRelative("m_serializedValues");
            //var fils = property.FindPropertyRelative("m_isFilled");
            int size = keys.arraySize;

            // !!! max 100
            size = Math.Min(size, 100);

            // foldout
            res += foldoutStyle.CalcHeight(GUIContent.none, 0);

            if (s_foldOut)
            {

                // header
                res += headerStyle.CalcHeight(GUIContent.none, 0);

                // rows
                for (int i = 0; i < size; ++i)
                {
                    var key = keys.GetArrayElementAtIndex(i);
                    var val = vals.GetArrayElementAtIndex(i);
                    //var use = fils.GetArrayElementAtIndex(i);
                    var keyHeight = EditorGUI.GetPropertyHeight(key, GUIContent.none, true);
                    var valHeight = EditorGUI.GetPropertyHeight(val, GUIContent.none, true);
                    //var useHeight = EditorGUI.GetPropertyHeight(use, GUIContent.none, true);
                    var btnHeight = cellStyle.CalcHeight(GUIContent.none, 0);
                    //var maxHeight = Mathf.Max(keyHeight, valHeight, useHeight, btnHeight);
                    var maxHeight = Mathf.Max(keyHeight, valHeight, btnHeight);

                    res += maxHeight;
                }

                // add
                res += foldoutStyle.CalcHeight(GUIContent.none, 0);
                if (s_addFoldOut)
                {
                    res += botStyle.CalcHeight(GUIContent.none, 0);

                    var ak = property.FindPropertyRelative("m_addingKey");
                    var av = property.FindPropertyRelative("m_addingValue");

                    var akh = EditorGUI.GetPropertyHeight(ak);
                    var avh = EditorGUI.GetPropertyHeight(av);
                    res += Mathf.Max(akh, avh);
                }
            }

            return res;
        }
    }
#endif
}