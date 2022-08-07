using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-24 오후 9:51:43, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    public partial class Table<TRow>
    {
        /// <summary>
        /// Abstracted Column
        /// </summary>
        public abstract class Column
        {
            static GUIStyle s_headerStyle;

            const float k_defaultWidth = 90;

            bool m_reverseSort;

            public string HeaderText { get; set; }

            public float Width { get; set; } = k_defaultWidth;

            public bool ExpandWidth { get; set; }

            public bool Disabled { get; set; }

            public Action OnHeaderClicked { get; set; }

            public Comparison<TRow> OnHeaderClickedSorter { get; set; }

            internal Table<TRow> Table { get; set; }

            void ValidateStyles()
            {
                if (s_headerStyle == null)
                {
                    s_headerStyle = new GUIStyle(EditorStyles.miniButton);
                }
            }

            public void DrawHeader()
            {
                ValidateStyles();

                bool enableTemp = GUI.enabled;
                if (OnHeaderClicked == null && OnHeaderClickedSorter == null)
                {
                    GUI.enabled = false;
                }

                if (GUILayout.Button(HeaderText, s_headerStyle))
                {
                    OnHeaderClicked?.Invoke();

                    // set table sorter
                    if (OnHeaderClickedSorter != null)
                    {
                        if (m_reverseSort)
                        {
                            Table.Comparison = (x, y) => -OnHeaderClickedSorter(x, y);
                            m_reverseSort = !m_reverseSort;
                        }
                        else
                        {
                            Table.Comparison = OnHeaderClickedSorter;
                            m_reverseSort = !m_reverseSort;
                        }
                    }
                }

                GUI.enabled = enableTemp;
            }

            public void DrawCell(TRow row)
            {
                var temp = GUI.enabled;
                GUI.enabled = !Disabled;

                DrawCellField(row);

                GUI.enabled = temp;
            }

            protected abstract void DrawCellField(TRow row);
        }

    }
}