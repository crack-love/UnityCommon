using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-21 오전 12:26:15, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common.Editors
{
    public partial class Table<TRow>
    {
        public abstract class GetterSetterColumn<TValue> : Column
        {
            public Func<TRow, TValue> Getter { get; set; }
            public Action<TRow, TValue> Setter { get; set; }
        }

        public class LabelColumn : Column
        {
            public Func<TRow, string> Getter { get; set; }

            protected override void DrawCellField(TRow row)
            {
                GUILayout.Label(Getter(row));
            }
        }

        public class ButtonColumn : Column
        {
            public string Text { get; set; }
            public Action<TRow> OnClicked { get; set; }

            protected override void DrawCellField(TRow row)
            {
                if (GUILayout.Button(Text))
                {
                    OnClicked?.Invoke(row);
                }
            }
        }

        public class RemoveButtonColumn : Column
        {
            public string Text { get; set; } = "X";
            public Action<TRow> OnRemoved { get; set; }

            public RemoveButtonColumn()
            {
                Width = 30;
            }

            protected override void DrawCellField(TRow row)
            {
                if (GUILayout.Button(Text))
                {
                    Table.Rows.Remove(row);
                    OnRemoved?.Invoke(row);
                }
            }
        }

        public class IntFieldColumn : GetterSetterColumn<int>
        {
            protected override void DrawCellField(TRow row)
            {
                EditorGUI.BeginChangeCheck();
                var v = EditorGUILayout.IntField(Getter(row));
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }

        public class FloatFieldColumn : GetterSetterColumn<float>
        {
            protected override void DrawCellField(TRow row)
            {
                EditorGUI.BeginChangeCheck();
                var v = EditorGUILayout.FloatField(Getter(row));
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }

        public class IntSliderColumn : GetterSetterColumn<int>
        {
            public Vector2Int MinMax { get; set; }

            protected override void DrawCellField(TRow row)
            {
                MinMax = new Vector2Int(Mathf.Min(MinMax.x, MinMax.y), Mathf.Max(MinMax.x, MinMax.y));

                EditorGUI.BeginChangeCheck();
                var v = EditorGUILayout.IntSlider(Getter(row), MinMax.x, MinMax.y);
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }

        public class FloatSliderColumn : GetterSetterColumn<float>
        {
            public Vector2 MinMax { get; set; }

            protected override void DrawCellField(TRow row)
            {
                MinMax = new Vector2(Mathf.Min(MinMax.x, MinMax.y), Mathf.Max(MinMax.x, MinMax.y));

                EditorGUI.BeginChangeCheck();
                var v = EditorGUILayout.Slider(Getter(row), MinMax.x, MinMax.y);
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }

        public class ObjectFieldColumn<TObject> : GetterSetterColumn<TObject>
            where TObject : UnityEngine.Object
        {
            public bool AllowSceneObject { get; set; }

            protected override void DrawCellField(TRow row)
            {
                EditorGUI.BeginChangeCheck();
                var v = (TObject)EditorGUILayout.ObjectField(Getter(row), typeof(TObject), AllowSceneObject);
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }

        public class TextFieldColumn : GetterSetterColumn<string>
        {
            protected override void DrawCellField(TRow row)
            {
                EditorGUI.BeginChangeCheck();
                var v = EditorGUILayout.TextField(Getter(row));
                if (EditorGUI.EndChangeCheck())
                {
                    Setter(row, v);
                }
            }
        }
    }
}