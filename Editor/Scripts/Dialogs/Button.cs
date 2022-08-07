using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 4:53:23, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace UnityCommon.Editors
{
    /// <summary>
    /// Use Button.Factory
    /// </summary>
    public class Button
    {
        static ButtonFactory s_factory = new ButtonFactory();

        string m_text;
        bool m_isSubmit;
        bool m_isCancel;

        public event Action OnClicked;

        public Button(string text) : this(text, false, false)
        {
        }

        /// <summary>
        /// Cancel and Submit closes window on clicked. submit call ISubmitCallbackField
        /// </summary>
        public Button(string text, bool isSubmit, bool isCancel)
        {
            m_text = text;
            m_isSubmit = isSubmit;
            m_isCancel = isCancel;
        }

        public static ButtonFactory Factory
        {
            get => s_factory;
            set => s_factory = value;
        }

        public string Text
        {
            get => m_text;
            set => m_text = value;
        }

        public bool IsSubmit
        {
            get => m_isSubmit;
            set => m_isSubmit = value;
        }

        public bool IsCancel
        {
            get => m_isCancel;
            set => m_isCancel = value;
        }

        public void OnGUI(out bool needClose)
        {
            if (GUILayout.Button(m_text))
            {
                OnClicked?.Invoke();

                needClose = m_isSubmit || m_isCancel;
            }
            else
            {
                needClose = false;
            }
        }
    }
}