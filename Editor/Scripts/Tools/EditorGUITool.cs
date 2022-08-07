using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-27 오전 1:38:03, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon.Editors
{
    /// <summary>
    /// Auto calculate rects
    /// </summary>
    public static class EditorGUITool
    {
        // 정확한 GUI 레이아웃 계산은 스택으로 역추적하면 되지만
        // 너무 방대해지기 때문에 그렇게 하지 않고 간단하게 작성

        const float IndentDelta = 11;
        const float Margin = 2;

        enum Allignment
        {
            Vertical, Horizontal,
        }

        static Allignment m_allign = Allignment.Vertical;
        static float m_horWidth = 0;
        static float m_horBegX = 0;
        static float m_horMaxY = 0;
        static int m_horSize = 0;

        /// <param name="horSize">how many element will be drawn in this horizontal</param>
        public static void BeginHorizontal(in Rect pos, int horSize)
        {
            m_horBegX = pos.x;
            m_horWidth = pos.width;
            m_horSize = horSize;
            m_allign = Allignment.Horizontal;
        }

        public static void EndHorizontal(ref Rect pos)
        {
            pos.x = m_horBegX;
            pos.y += m_horMaxY + Margin;
            pos.width = m_horWidth;

            m_horWidth = 0;
            m_horSize = 0;
            m_horBegX = 0;
            m_horMaxY = 0;
            m_allign = Allignment.Vertical;
        }

        static void GetHorizontalWidth(in Rect pos, int hidx, out float x, out float width)
        {
            if (m_allign != Allignment.Horizontal)
            {
                x = pos.x;
                width = pos.width;
                return;
            }

            width = (m_horWidth - ((m_horSize - 1) * Margin)) / m_horSize;
            x = m_horBegX + hidx * (width + Margin);
        }

        static void SetHorizontalHeight(float height)
        {
            if (m_allign != Allignment.Horizontal) return;

            m_horMaxY = Mathf.Max(height, m_horMaxY);
        }

        public static bool Foldout(ref Rect pos, ref bool foldout, GUIContent label, int horIdx = 0)
        {
            GetHorizontalWidth(pos, horIdx, out var x, out var width);
            var y = pos.y;
            var foStyle = EditorStyles.foldout;
            var height = foStyle.CalcHeight(label, width);
            var rect = new Rect(x, y, width, height);
            SetHorizontalHeight(height);

            foldout = EditorGUI.Foldout(rect, foldout, label, true, foStyle);

            // indent
            if (!foldout)
            {
                pos.x += IndentDelta;
                pos.width -= IndentDelta;
            }

            MoveNextVertical(rect, ref pos);

            return foldout;
        }

        public static bool Button(ref Rect pos, GUIContent label, int horidx = 0)
        {
            GetHorizontalWidth(pos, horidx, out var x, out var width);
            var y = pos.y;
            var style = EditorStyles.miniButton;
            var height = style.CalcHeight(label, width);
            var rect = new Rect(x, y, width, height);
            SetHorizontalHeight(height);

            bool clicked = GUI.Button(rect, label, style);

            MoveNextVertical(rect, ref pos);

            return clicked;
        }

        public static void PropertyField(ref Rect pos, SerializedProperty prop, GUIContent label, bool includeChild = true, int horIdx = 0)
        {
            GetHorizontalWidth(pos, horIdx, out var x, out var width);
            var y = pos.y;
            var height = EditorGUI.GetPropertyHeight(prop, label, includeChild);
            var rect = new Rect(x, y, width, height);
            SetHorizontalHeight(height);

            var temp = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = rect.width / 2f;
            EditorGUI.PropertyField(rect, prop, label);
            EditorGUIUtility.labelWidth = temp;

            MoveNextVertical(rect, ref pos);
        }

        public static void HelpBox(ref Rect pos, GUIContent content, MessageType type, int horidx = 0)
        {
            GetHorizontalWidth(pos, horidx, out var x, out var width);
            var y = pos.y;
            var style = EditorStyles.helpBox;
            var height = style.CalcHeight(content, width);
            var rect = new Rect(x, y, width, height);
            SetHorizontalHeight(height);

            EditorGUI.HelpBox(rect, content.text, type);

            MoveNextVertical(rect, ref pos);
        }

        static void MoveNextVertical(in Rect drew, ref Rect pos)
        {
            if (m_allign == Allignment.Vertical)
            {
                pos.y += drew.height + Margin;
            }
            else if (m_allign == Allignment.Horizontal)
            {

            }
        }
    }

}