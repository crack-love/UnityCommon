/*using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using System;

// editrpref : 에디터 프리퍼런스 (영구)
// sessionstate : 세션 유지되는 에디터 프리퍼런스 (종료시 제거, 어셈 리로드시에는 유지)
// playerpref(editor/build) : 플레이어 프리퍼런스 (영구)

namespace UnityCommon.Editors
{
    public class PlayerPrefsEditorWindow : EditorWindow
    {
        Toolbar m_toolbar;

        [MenuItem("Edit/Player Prefs")]
        public static void OpenWindow()
        {
            var window = GetWindow(typeof(PlayerPrefsEditorWindow));
            window.titleContent = new GUIContent("Prefs");
            window.Show();
        }

        private void OnEnable()
        {
            if (m_toolbar == null)
            {
                m_toolbar = new Toolbar();
                m_toolbar.Add<EditorPrefsDrawer>();
                m_toolbar.Add<SessionPrefsDrawer>();
                m_toolbar.Add<PlayerPrefsDrawer>();
            }

            m_toolbar.OnEnable();
        }

        private void OnDisable()
        {
            m_toolbar.OnDisable();
        }

        private void OnGUI()
        {
            m_toolbar.OnGUI();
        }


        enum PrefType
        {
            Int, // p e s
            Float, // p e s
            Bool, //e s
            String, //p e s
            Vector3, // s
            IntArray // s
        }

        class Pref
        {
            public string Key;
            public PrefType Type;
        }

        abstract class PrefsDrawer : ToolbarItem
        {
            readonly protected Table<Pref> m_table;

            public PrefsDrawer()
            {
                // create table
                m_table = new Table<Pref>();
                m_table.AddColumn(new Table<Pref>.LabelColumn()
                {
                    HeaderText = "Key",
                    Getter = (x) => x.Key,
                    Disabled = true,
                });

                m_table.AddColumn(new Table<Pref>.LabelColumn()
                {
                    HeaderText = "Type",
                    Getter = (x) => Enum.GetName(typeof(PrefType), x.Type),
                });

                m_table.AddColumn(new Table<Pref>.TextFieldColumn()
                {
                    HeaderText = "Value",
                    ExpandWidth = true,
                    Getter = (x) => GetValueText(x),
                    Setter = (x, y) =>
                    {
                        SetValue(x, y);
                        Save();
                    },
                });

                m_table.AddColumn(new Table<Pref>.RemoveButtonColumn()
                {
                    OnRemoved = (x) =>
                    {
                        DeleteKey(x);
                        Save();
                    }
                });
            }

            string m_settingKey = "";
            PrefType m_settingType;
            protected string m_message = null;

            public override void OnGUI()
            {
                if (m_table.Rows.Count > 0)
                {
                    m_table.DrawGUI();
                }

                if (m_message != null)
                {
                    EditorGUILayout.HelpBox(m_message, MessageType.Error);
                }

                EditorGUILayout.Separator();

                m_settingType = (PrefType)EditorGUILayout.EnumPopup("Key Type", m_settingType);
                m_settingKey = EditorGUILayout.TextField("Key", m_settingKey);

                if (GUILayout.Button("Add Key"))
                {
                    var pref = new Pref()
                    {
                        Key = m_settingKey,
                        Type = m_settingType
                    };

                    m_table.Rows.Add(pref);
                }

                if (GUILayout.Button("Delete All Keys"))
                {
                    DeleteAll();
                    m_table.Rows.Clear();
                    Save();
                }
            }

            protected abstract string GetValueText(Pref pref);

            protected abstract void SetValue(Pref pref, string value);

            protected abstract void DeleteKey(Pref pref);

            protected abstract void DeleteAll();

            protected abstract void Save();
        }

        class EditorPrefsDrawer : PrefsDrawer
        {
            public override string name => "Editor";

            protected override void DeleteAll()
            {
                EditorPrefs.DeleteAll();
            }

            protected override void DeleteKey(Pref pref)
            {
                EditorPrefs.DeleteKey(pref.Key);
            }

            protected override string GetValueText(Pref pref)
            {
                switch (pref.Type)
                { 
                    case PrefType.Bool : return EditorPrefs.GetBool(pref.Key).ToString();
                    case PrefType.Float : return EditorPrefs.GetFloat(pref.Key).ToString();
                    case PrefType.Int : return EditorPrefs.GetInt(pref.Key).ToString();
                    case PrefType.String : return EditorPrefs.GetString(pref.Key);
                    default : return "Invalid Type";
                }
            }

            protected override void SetValue(Pref pref, string value)
            {
                switch (pref.Type)
                {
                    case PrefType.Int: 
                        if (!int.TryParse(value, out var v)) m_message = "Invalid Value";
                        else EditorPrefs.SetInt(pref.Key, v);
                        break;
                    case PrefType.Float:
                        if (!float.TryParse(value, out var fv)) m_message = "Invalid Value";
                        else EditorPrefs.SetFloat(pref.Key, fv);
                        break;
                    case PrefType.Bool:
                        if (!bool.TryParse(value, out var bv)) m_message = "Invalid Value";
                        else EditorPrefs.SetBool(pref.Key, bv);
                        break;
                    case PrefType.String:
                        EditorPrefs.SetString(pref.Key, value);
                        break;
                    default:
                        break;
                }
            }

            protected override void Save()
            {
            }
        }

        class SessionPrefsDrawer : PrefsDrawer
        {
            public override string name => "Editor Session";

            protected override void DeleteAll()
            {
                foreach (var p in m_table.Rows)
                {
                    DeleteKey(p);
                }
            }

            protected override void DeleteKey(Pref pref)
            {
                switch (pref.Type)
                {
                    case PrefType.Int: SessionState.EraseInt(pref.Key); break;
                    case PrefType.Float: SessionState.EraseFloat(pref.Key); break;
                    case PrefType.Bool: SessionState.EraseBool(pref.Key); break;
                    case PrefType.String: SessionState.EraseString(pref.Key); break;
                    case PrefType.Vector3: SessionState.EraseVector3(pref.Key); break;
                    case PrefType.IntArray: SessionState.EraseIntArray(pref.Key); break;
                    default: break;
                }
            }

            protected override string GetValueText(Pref pref)
            {
                switch (pref.Type)
                {
                    case PrefType.Int: return SessionState.GetInt(pref.Key, 0).ToString();
                    case PrefType.Float: return SessionState.GetFloat(pref.Key, 0.0f).ToString();
                    case PrefType.Bool: return SessionState.GetBool(pref.Key, false).ToString();
                    case PrefType.String: return SessionState.GetString(pref.Key, "");
                    case PrefType.Vector3: return SessionState.GetVector3(pref.Key, default).ToString();
                    case PrefType.IntArray: return SessionState.GetIntArray(pref.Key, default).ToString();
                    default: return "";
                }
            }

            protected override void SetValue(Pref pref, string value)
            {
                switch (pref.Type)
                {
                    case PrefType.Int:
                        if (!int.TryParse(value, out var v)) m_message = "Invalid Value";
                        else SessionState.SetInt(pref.Key, v);
                        break;
                    case PrefType.Float:
                        if (!float.TryParse(value, out var fv))
                            m_message = "Invalid Value";
                        else
                            SessionState.SetFloat(pref.Key, fv);
                        break;
                    case PrefType.Bool:
                        if (!bool.TryParse(value, out var bv))
                            m_message = "Invalid Value";
                        else
                            SessionState.SetBool(pref.Key, bv);
                        break;
                    case PrefType.String:
                        SessionState.SetString(pref.Key, value);
                        break;
                    case PrefType.Vector3:
                        break;
                    case PrefType.IntArray:
                        break;
                }
            }

            protected override void Save()
            {

            }
        }

        class PlayerPrefsDrawer : ToolbarItem
        {
            public override string name => "Player (In Editor)";

            public override void OnGUI()
            {

            }
        }
    }
}*/