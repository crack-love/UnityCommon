using Common;
using Common.Editors;
using System;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
class TestToolbar : MonoBehaviour//, IT
{
    
}

[CustomEditor(typeof(TestToolbar))]
class TestToolbarEditor : Editor
{
    [NonSerialized] Toolbar tb;
    [NonSerialized] FooTool tool0;
    [NonSerialized] BarTool tool1;
    
    public override void OnInspectorGUI()
    {
        if (tb == null)
        {
            tb = new Toolbar();
            tool0 ??= new FooTool();
            tool1 ??= new BarTool();
            tb.Add(tool0);
            tb.Add(tool1);
        }

        tb.DrawGUI();
    }
    
    class BarTool : ToolbarItem
    {
        public override string Name => "Bar";

        public override void OnGUI()
        {

        }
    }

    class FooTool : ToolbarItem
    {
        string tex;

        public override string Name => "Foo";

        public override void OnGUI()
        {
            tex = EditorGUILayout.TextField(tex);
        }
    }
}