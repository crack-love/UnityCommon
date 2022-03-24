using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-21 오후 3:45:13, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    /// <summary>
    /// Use Dialog.Factory
    /// </summary>
    public class Dialog
    {
        static DialogFactory s_factory = new DialogFactory();

        /// <summary>
        /// 인터페이스를 위해 윈도우와 다이얼로그 분리
        /// </summary>
        readonly DialogWindow m_window;
        readonly List<Field> m_fields;
        readonly List<Button> m_buttons;

        public Dialog()
        {
            m_fields = new List<Field>();
            m_buttons = new List<Button>();
            m_window = ScriptableObject.CreateInstance<DialogWindow>();
            m_window.SetConfig(m_fields, m_buttons);
        }

        public static DialogFactory Factory
        {
            get => s_factory;
            set => s_factory = value;
        }

        public Dialog Show()
        {
            m_window.Show();
            return this;
        }

        public Dialog ShowModal()
        {
            m_window.ShowModal();
            return this;
        }

        public bool IsCanceled
        {
            get => m_window.IsCanceled;
        }

        public bool IsSubmited
        {
            get => m_window.IsSubmited;
        }

        public string Title
        {
            get => m_window.titleContent?.text;
            set => m_window.titleContent = new GUIContent(value);
        }

        public void AddButton(Button btn)
        {
            m_buttons.Add(btn);
        }

        public void AddField(Field field)
        {
            m_fields.Add(field);
        }
    }
}