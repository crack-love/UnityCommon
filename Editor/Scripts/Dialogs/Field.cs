using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 3:56:00, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon.Editors
{
    /// <summary>
    /// Use Field.Factory
    /// </summary>
    public class Field
    {
        static FieldFactory s_fieldFactory = new FieldFactory();

        string m_prefixLabel;
        bool m_enabled = true;
        Action m_onGUI;
        Action m_onPreGUI;
        Action m_onSubmit;

        public Field() : this(null)
        {
        }

        public Field(string prefixLabel)
        {
            m_prefixLabel = prefixLabel;
        }

        public static FieldFactory Factory
        {
            get => s_fieldFactory;
            set => s_fieldFactory = value;
        }

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        public string PrefixLabel
        {
            set => m_prefixLabel = value;
            get => m_prefixLabel;
        }

        public Action OnGUI
        {
            get => m_onGUI;
            set => m_onGUI = value;
        }

        /// <summary>
        /// Before Draw Prefix Label
        /// </summary>
        public Action OnPreGUI
        {
            get => m_onPreGUI;
            set => m_onPreGUI = value;
        }

        /// <summary>
        /// On Submit Button Clicked
        /// </summary>
        public Action OnSubmit
        {
            get => m_onSubmit;
            set => m_onSubmit = value;
        }

        public void DrawGUI()
        {
            bool temp = GUI.enabled;
            if (!m_enabled)
            {
                GUI.enabled = false;
            }

            m_onPreGUI?.Invoke();

            if (m_prefixLabel != null)
            {
                EditorGUILayout.PrefixLabel(m_prefixLabel);
            }

            m_onGUI?.Invoke();

            if (!m_enabled)
            {
                GUI.enabled = temp;
            }
        }
    }
}