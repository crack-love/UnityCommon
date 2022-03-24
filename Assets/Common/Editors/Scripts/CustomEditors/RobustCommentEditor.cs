using UnityEngine;
using UnityEditor;
using Common;

/// <summary>
/// 2022-03-20 오후 6:23:05, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common.Editors
{
    [CustomEditor(typeof(RobustComment))]
    class RobustCommentEditor : Editor
    {
        static GUIContent s_applyBtnTxt = new GUIContent(" Apply (Focus, Ctrl + S)");
        static GUIContent s_editBtnTxt = new GUIContent(" Edit (Focus, Ctrl + E)");
        static GUIContent s_undoBtnTxt = new GUIContent(" Undo");
        static GUIStyle s_stateStyle;

        string m_modificationText;
        RobustComment m_target;
        bool m_isEditing;
        bool m_isDirty;

        private void OnEnable()
        {
            m_target = (RobustComment)target;
            m_modificationText = m_target.Text;

            Undo.undoRedoPerformed += OnUndo;
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnUndo;
        }

        void OnUndo()
        {
            m_modificationText = m_target.Text;
            Repaint();
        }

        static GUIStyle CreateStateStyle()
        {
            var style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.grey;
            style.fontSize -= 1;
            return style;
        }

        void Apply()
        {
            // record prev object to undo
            Undo.RecordObject(m_target, "Edit Comment");
            Undo.RecordObject(this, "Edit Comment");

            m_target.Text = m_modificationText;
            m_target.State = System.DateTime.Now.ToString() + " " + System.Environment.UserName;
            EditorUtility.SetDirty(m_target);

            // unfocus textArea to prevent modification text remains on click edit again
            GUI.FocusControl("State");
            m_isEditing = false;
            m_isDirty = true;
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            var textAreaStyle = CommentEditor.GetTextAreaStyle();
            if (s_stateStyle == null)
                s_stateStyle = CreateStateStyle();

            // on edit
            if (m_isEditing)
            {
                // text area
                m_modificationText = EditorGUILayout.TextArea(m_modificationText, textAreaStyle);

                GUILayout.BeginHorizontal();
                {
                    // state
                    GUI.SetNextControlName("State"); // naming for focus
                    EditorGUILayout.LabelField(m_target.State, s_stateStyle);

                    // escape
                    var e = Event.current;
                    if (e.isKey && e.keyCode == KeyCode.Escape)
                    {
                        m_isEditing = false;
                        Repaint();
                    }

                    // apply btn
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(s_applyBtnTxt) ||
                        (e.isKey && e.control && e.keyCode == KeyCode.S))
                    {
                        Apply();
                        e.Use();
                    }
                }
                GUILayout.EndHorizontal();
            }
            // on view
            else
            {
                // text area
                bool temp = GUI.enabled;
                GUI.enabled = false;
                GUI.SetNextControlName("TextArea"); // naming for focus
                EditorGUILayout.TextArea(m_modificationText, textAreaStyle);
                GUI.enabled = temp;

                GUILayout.BeginHorizontal();
                {
                    // state
                    EditorGUILayout.LabelField(m_target.State, s_stateStyle);

                    // edit btn
                    GUILayout.FlexibleSpace();
                    var e = Event.current;
                    if (GUILayout.Button(s_editBtnTxt) || 
                        (e.isKey && e.control && e.keyCode == KeyCode.E))
                    {
                        GUI.FocusControl("TextArea"); // focus

                        m_isEditing = true;
                        Repaint();
                        e.Use();
                    }

                    if (m_isDirty && GUILayout.Button(s_undoBtnTxt))
                    {
                        Undo.PerformUndo();
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}