using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 2021-02-03 수 오후 6:37:27, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common.Editors
{
    public class Toolbar
    {
        ToolbarButtonSize m_buttonSize;
        List<ToolbarItem> m_tools;
        string[] m_toolNames;
        int m_selected;
        bool m_isInitialized;

        public ToolbarButtonSize ButtonsSize
        {
            get => m_buttonSize;
            set => m_buttonSize = value;
        }

        public Toolbar()
        {
            m_tools = new List<ToolbarItem>();
        }

        ~Toolbar()
        {
            if (m_tools.Count > 0)
                m_tools[m_selected].DeActive();
        }

        void Validate()
        {
            if (m_tools.Count <= 0) return;

            // clamp idx
            m_selected = Mathf.Clamp(m_selected, 0, m_tools.Count - 1);

            // update names
            if (m_toolNames == null || m_toolNames.Length != m_tools.Count)
            {
                m_toolNames = new string[m_tools.Count];
            }
            for (int i = 0; i < m_tools.Count; ++i)
            {
                m_toolNames[i] = m_tools[i].Name;
            }

            // select current showing item
            if (!m_isInitialized)
            {
                m_isInitialized = true;
                m_tools[m_selected].Active();
            }
        }

        public void Add(ToolbarItem tool)
        {
            m_tools.Add(tool);
        }

        public void DrawGUI()
        {
            Validate();

            EditorGUI.BeginChangeCheck();
            
            // toolbar gui
            var newSelected = GUILayout.Toolbar(m_selected, m_toolNames, null, (GUI.ToolbarButtonSize)m_buttonSize);

            // change selection
            if (EditorGUI.EndChangeCheck() && newSelected != m_selected)
            {
                var oldOne = m_tools[m_selected];
                var newOne = m_tools[newSelected];
                oldOne.IsSelected = false;
                newOne.IsSelected = true;
                oldOne.DeActive();
                newOne.Active();
                m_selected = newSelected; 
            }

            // selected gui
            m_tools[m_selected].OnGUI();
        }
    }

    // structure copy for convinience
    public enum ToolbarButtonSize
    {
        Fixed = GUI.ToolbarButtonSize.Fixed,
        FitToContents = GUI.ToolbarButtonSize.FitToContents,
    }
}