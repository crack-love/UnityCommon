using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-23 오후 6:30:43, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    public class FieldFactory
    {
        public Field Create(string prefix = null)
        {
            Field res = new Field();

            res.PrefixLabel = prefix;

            return res;
        }

        public Field Label(string value, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                EditorGUILayout.LabelField(value);
            };

            return res;
        }

        public Field Header(string value, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                GUILayout.Space(12);
                EditorGUILayout.LabelField(value, EditorStyles.boldLabel);
            };

            return res;
        }
        
        public Field Text(string value, Action<string> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.TextField(value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field TextArea(string value, Action<string> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.TextArea(value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }
        
        public Field HelpBox(string value, MessageType type = MessageType.Info, bool wide = true, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                EditorGUILayout.HelpBox(value, type, wide);
            };

            return res;
        }

        public Field Toggle(bool value, Action<bool> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.Toggle(value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field ToggleGroupBegin(string groupName, bool enabled = true)
        {
            Field res = Create();

            res.OnGUI = () =>
            {
                enabled = EditorGUILayout.BeginToggleGroup(groupName, enabled);
            };

            return res;
        }

        public Field ToggleGroupEnd()
        {
            Field res = Create();

            res.OnGUI = () =>
            {
                EditorGUILayout.EndToggleGroup();
            };

            return res;
        }

        public Field Int(int value, Action<int> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.IntField("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field Float(float value, Action<float> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.FloatField("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field Vector2(Vector2 value, Action<Vector2> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.Vector2Field("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field Vector2Int(Vector2Int value, Action<Vector2Int> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.Vector2IntField("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field Vector3(Vector3 value, Action<Vector3> setter = null, string prefix = null)
        {
            Field res = Create(prefix);

            res.OnGUI = () =>
            {
                value = EditorGUILayout.Vector3Field("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }

        public Field Vector3Int(Vector3Int value, Action<Vector3Int> setter = null, string prefix = null)
        {
            Field res = Create(prefix);
            
            res.OnGUI = () =>
            {
                value = EditorGUILayout.Vector3IntField("", value);
            };

            res.OnSubmit = () =>
            {
                setter?.Invoke(value);
            };

            return res;
        }
    }
}