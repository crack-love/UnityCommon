using UnityCommon;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2021-04-13 화 오후 7:48:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon.Editors
{
    [CustomPropertyDrawer(typeof(SerializableHashSet<>), true)]
    class SerializableHashSetPropertyDrawer : PropertyDrawer
    {
        const string k_listName = "m_serialValues";
        const string k_failName = "m_deSerialFail";
        const string k_dupError = " Found Duplicated Key";

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            // get properties
            var pairs = property.FindPropertyRelative(k_listName);
            var fail = property.FindPropertyRelative(k_failName);

            // draw
            EditorGUITool.PropertyField(ref pos, pairs, new GUIContent(property.name), true);
            if (fail.boolValue)
            {
                EditorGUITool.HelpBox(ref pos, new GUIContent(k_dupError), MessageType.Error);
            }

            // apply
            property.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var list = property.FindPropertyRelative(k_listName);
            var fail = property.FindPropertyRelative(k_failName);

            // list height
            var height = EditorGUI.GetPropertyHeight(list);

            // helpbox height
            if (fail.boolValue)
            {
                height += EditorGUIUtility.singleLineHeight; 
            }

            return height;
        }
    }
}