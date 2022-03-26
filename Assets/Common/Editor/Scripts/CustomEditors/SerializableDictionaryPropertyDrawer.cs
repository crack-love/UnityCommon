using Common;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2021-04-13 화 오후 7:48:24, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace CommonEditor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            // get properties
            var pairs = property.FindPropertyRelative("m_serializedPairs");
            var fail = property.FindPropertyRelative("m_deserializationFail");

            EditorGUITool.PropertyField(ref pos, pairs, new GUIContent(property.name), true);

            if (fail.boolValue)
            {
                EditorGUITool.HelpBox(ref pos, new GUIContent($" Found duplicated key. {property.name} is broken"), MessageType.Error);
            }

            // apply
            property.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var pairs = property.FindPropertyRelative("m_serializedPairs");
            var fail = property.FindPropertyRelative("m_deserializationFail");

            var height = EditorGUI.GetPropertyHeight(pairs);
            if (fail.boolValue) height += EditorGUIUtility.singleLineHeight;

            return height;
        }
    }
}