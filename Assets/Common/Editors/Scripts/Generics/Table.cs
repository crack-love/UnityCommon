using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-24 오후 7:24:36, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common.Editors
{
    public partial class Table<TRow>
    {
        List<TRow> m_rows;
        List<Column> m_cols;
        Comparison<TRow> m_comparison;

        // guis
        Vector2 m_scroll;
        GUIStyle m_cellStyle;
        GUIStyle m_rowHStyle;
        GUIStyle m_row0Style;
        GUIStyle m_row1Style;
        readonly static Color k_rowHColor = new Color(0.25f, 0.25f, 0.25f);
        readonly static Color k_row0Color = new Color(0.35f, 0.35f, 0.35f);
        readonly static Color k_row1Color = new Color(0.25f, 0.25f, 0.25f);

        public Table()
        {
            m_rows = new List<TRow>();
            m_cols = new List<Column>();
        }

        public List<TRow> Rows
        {
            get => m_rows;
            set => m_rows = value;
        }

        public List<Column> Cols
        {
            get => m_cols;
        }

        public Comparison<TRow> Comparison
        {
            get => m_comparison;
            set
            {
                m_comparison = value; 
                m_rows.Sort(m_comparison);
            }
        }

        public GUIStyle RowHStyle
        {
            get => m_rowHStyle;
            set => m_rowHStyle = value;
        }

        public GUIStyle Row0Style
        {
            get => m_row0Style;
            set => m_row0Style = value;
        }

        public GUIStyle Row1Style
        {
            get => m_row1Style;
            set => m_row1Style = value;
        }

        void ValidateStyles()
        {
            if (m_cellStyle == null)
            {
                m_cellStyle = new GUIStyle(GUIStyle.none);
                // style.padding = new RectOffset(0, 0, 0, 0);
                // style.margin = new RectOffset(0, 0, 0, 0);
            }

            if (m_rowHStyle == null)
            {
                m_rowHStyle = new GUIStyle(GUIStyle.none);
                SetBackground(m_rowHStyle, k_rowHColor);
            }

            if (m_row0Style == null)
            {
                m_row0Style = new GUIStyle(GUIStyle.none);
                SetBackground(m_row0Style, k_row0Color);
            }

            if (m_row1Style == null)
            {
                m_row1Style = new GUIStyle(GUIStyle.none);
                SetBackground(m_row1Style, k_row1Color);
            }

            static void SetBackground(GUIStyle src, Color color)
            {
                var tex = new Texture2D(1, 1);
                tex.SetPixel(0, 0, color);
                tex.Apply();
                src.normal.background = tex;
            }
        }

        public void AddColumn(Column col)
        {
            m_cols.Add(col);
            col.Table = this;
        }

        public void DrawGUI()
        {
            ValidateStyles();

            // Box
            m_scroll = EditorGUILayout.BeginScrollView(m_scroll);

            // Header
            BeginRowLayout(-1);
            for (int i = 0; i < m_cols.Count; ++i)
            {
                var col = m_cols[i];

                BeginCellLayout(col.Width, col.ExpandWidth);
                col.DrawHeader();
                EndCellLayout();

            }
            EndRowLayout();

            // Each rows
            for (int i = 0; i < m_rows.Count; ++i)
            {
                var row = m_rows[i];

                BeginRowLayout(i);
                for (int j = 0; j < m_cols.Count; ++j)
                {
                    var col = m_cols[j];

                    // cell
                    BeginCellLayout(col.Width, col.ExpandWidth);
                    col.DrawCell(row);
                    EndCellLayout();
                }
                EndRowLayout();
            }

            EditorGUILayout.EndScrollView();
        }

        void BeginRowLayout(int rowIdx)
        {
            GUIStyle style;
            if (rowIdx < 0) style = m_rowHStyle;
            else style = rowIdx % 2 == 0 ? m_row0Style : m_row1Style;
            
            GUILayout.BeginHorizontal(style);
        }

        void EndRowLayout()
        {
            GUILayout.EndHorizontal();
        }

        void BeginCellLayout(float width, bool expand)
        {
            if (expand)
            {
                GUILayout.BeginVertical(m_cellStyle, GUILayout.ExpandWidth(expand));
            }
            else
            {
                GUILayout.BeginVertical(m_cellStyle, GUILayout.Width(width));
            }
        }

        void EndCellLayout()
        {
            GUILayout.EndVertical();
        }
    }
}