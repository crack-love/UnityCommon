using UnityEditor;
using UnityEngine;
using UnityCommon;

namespace UnityCommon.Editors
{
    [CustomEditor(typeof(Comment))]
    class CommentEditor : Editor
    {
        static Vector4Int s_padding = new Vector4Int(8, 8, 8, 8);
        static Vector4Int s_margin = new Vector4Int(5, 5, 2, 5);
        static Color s_textColor = Color.black;
        static Color s_backColor = Color.grey;
        static GUIStyle s_textAreaStyle;

        Comment m_target;
        string m_text;

        private void OnEnable()
        {
            m_target = (Comment)target;
            m_text = m_target.Text;
        }

        static GUIStyle CreateTextAreaStyle()
        {
            var style = new GUIStyle(GUI.skin.textArea);

            // allignment
            style.padding = new RectOffset(s_padding.x, s_padding.y, s_padding.z, s_padding.w);
            style.margin = new RectOffset(s_margin.x, s_margin.y, s_margin.z, s_margin.w);
            style.alignment = TextAnchor.UpperLeft;

            // colors
            var backTex = new Texture2D(1, 1); // background
            backTex.SetPixel(0, 0, s_backColor);
            backTex.Apply();
            style.normal.textColor = s_textColor;            
            style.normal.background = backTex;

            return style;
        }

        public static GUIStyle GetTextAreaStyle()
        {
            if (s_textAreaStyle == null)
                s_textAreaStyle = CreateTextAreaStyle();

            return s_textAreaStyle;
        }

        public override void OnInspectorGUI()
        {
            if (s_textAreaStyle == null)
                s_textAreaStyle = CreateTextAreaStyle();

            EditorGUI.BeginChangeCheck();
            m_text = EditorGUILayout.TextArea(m_text, s_textAreaStyle);
            if (EditorGUI.EndChangeCheck())
            {
                m_target.Text = m_text;
                EditorUtility.SetDirty(m_target);
            }
        }
    }
}