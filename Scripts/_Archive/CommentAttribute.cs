/*using System;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 2020-04-04 오후 3:25:19, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon
{
    /// <summary>
    /// 인스펙터 멀티라인 코멘트
    /// </summary>
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class CommentAttribute : PropertyAttribute
    {
        public RectOffset Padding = new RectOffset(8, 8, 8, 8);
        public RectOffset Margin = new RectOffset(5, 5, 2, 5);
        public Color TextColor = Color.black;
        public Color BackColor = Color.grey;

        public CommentAttribute()
        {

        }

        public CommentAttribute(Color textColor, Color backColor)
        {
            TextColor = textColor;
            BackColor = backColor;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(CommentAttribute))]
    class CommentAttributeDrawer : PropertyDrawer
    {
        new CommentAttribute attribute;
        GUIStyle style;

        public CommentAttributeDrawer()
        {
            attribute = (CommentAttribute)base.attribute;
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            attribute = (CommentAttribute)base.attribute;
            var textContent = new GUIContent(prop.stringValue);

            if (style == null)
            {
                style = new GUIStyle(GUI.skin.textArea);
                style.padding = attribute.Padding;
                style.margin = attribute.Margin;
                style.alignment = TextAnchor.UpperLeft;

                // colors
                style.normal.textColor = attribute.TextColor;
                var tex = new Texture2D(1, 1); // background
                tex.SetPixel(0, 0, attribute.BackColor);
                style.normal.background = tex;
            }            

            var height = style.CalcHeight(textContent, EditorGUIUtility.currentViewWidth - style.margin.horizontal);

            return height + style.margin.vertical;
        }

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            OnGUINoColor(position, prop, label);
        }

        void OnGUINoColor(Rect position, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, prop);

            // Render
            prop.stringValue = EditorGUI.TextArea(position, prop.stringValue, style);

            EditorGUI.EndProperty();
        }

    }
#endif
}*/