using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 3:55:52, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    class DialogWindow : EditorWindow
    {
        List<Field> m_fields;
        List<Button> m_buttons;

        Vector2 m_scroll;
        bool m_isDestroyed;
        bool m_isSubmited;
        bool m_isPositionInitialized;

        public bool IsCanceled => !m_isSubmited;

        public bool IsSubmited => m_isSubmited;

        internal void SetConfig(List<Field> fields, List<Button> btns)
        {
            m_fields = fields;
            m_buttons = btns;
        }

        private void OnDisable()
        {
            // make this not reloadable
            // cuz field's system.action is not serializable
            if (!m_isDestroyed)
            {
                m_isDestroyed = true;
                EditorUtility.SetDirty(this);
            }
        }

        void SetPositionToMouse()
        {
            // set window pos to mouse pos
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            mousePos.x -= position.width;
            position = new Rect(mousePos, position.size);
        }

        private void OnGUI()
        {
            if (m_isDestroyed)
            {
                Close();
                return;
            }
            else if (!m_isPositionInitialized)
            {
                m_isPositionInitialized = true;
                SetPositionToMouse();
            }

            EditorGUILayout.BeginScrollView(m_scroll);

            // context
            EditorGUILayout.BeginVertical();
            foreach (var field in m_fields)
            {
                field.DrawGUI();
            }
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();

            // button
            EditorGUILayout.BeginVertical();
            foreach (var button in m_buttons)
            {
                button.OnGUI(out bool needClose);
                if (needClose)
                {
                    if (button.IsSubmit)
                    {
                        m_isSubmited = true;

                        // callback submit
                        foreach (var field in m_fields)
                        {
                            field.OnSubmit?.Invoke();
                        }
                    }

                    Close();
                    break;
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }
    }

}