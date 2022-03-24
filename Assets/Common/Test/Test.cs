
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Common.Editors;

namespace Common.Test
{
    class Test : MonoBehaviour
    {

    }

    [CustomEditor(typeof(Test))]
    class TestEditor : Editor
    {
        Table<int> table;

        public static void OnEnableStatic(ref Table<int> table)
        {
            table = new Table<int>();
            table.Rows = new List<int>();
            table.AddColumn(new Table<int>.LabelColumn()
            {
                HeaderText = "LABEL",
                ExpandWidth = true,
                Getter = (x) => x.ToString(),
                //OnHeaderClickedSorter = (x,y) => x - y,
            });
            table.AddColumn(new Table<int>.TextFieldColumn()
            {
                HeaderText = "TEXT",
                Getter = (x) => (x + 1).ToString(),
                Setter = (x, y) => x = Convert.ToInt32(y),
            });
            table.AddColumn(new Table<int>.RemoveButtonColumn()
            {

            });
        }

        private void OnEnable()
        {
            OnEnableStatic(ref table);
        }

        public override void OnInspectorGUI()
        {
            table.DrawGUI();
            if (GUILayout.Button("Add"))
                table.Rows.Add(UnityEngine.Random.Range(1, 10));
        }
    }


    class TesetWindow : EditorWindow
    {
        Table<int> table;

        [MenuItem("Show/Text")]
        static void ShowTest()
        {
            var w = GetWindow<TesetWindow>();
            w.Show();
        }

        private void OnEnable()
        {
            TestEditor.OnEnableStatic(ref table);
        }

        public void OnGUI()
        {
            table.DrawGUI();
            if (GUILayout.Button("Add"))
                table.Rows.Add(UnityEngine.Random.Range(1, 10));
        }
    }
}