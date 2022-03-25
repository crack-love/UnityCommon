/*#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor Inspector GUI Extension
/// </summary>
public static class EditorGUIEx
{
    static GUIStyle m_helpStyle;
    static GUIStyle m_labelStyle;
    static GUIStyle m_buttonStyle;
    static GUIStyle m_boxStyle;
    static GUIStyle m_toggleStyle;
    static GUIStyle m_textAreaStyle;
    static GUIStyle m_textFieldStyle;
    static float m_defaultHeaderSpace;

    public static float HeaderSpace
    {
        get => m_defaultHeaderSpace;
        set => m_defaultHeaderSpace = value;
    }

    static EditorGUIEx()
    {
        m_defaultHeaderSpace = 15f;

        // Help style
        m_helpStyle = new GUIStyle(GUI.skin.GetStyle("helpbox"));
        m_helpStyle.padding = new RectOffset(8, 8, 8, 8);
        m_helpStyle.margin = new RectOffset(5, 5, 5, 3);
        m_helpStyle.alignment = TextAnchor.MiddleLeft;

        m_labelStyle = new GUIStyle(GUI.skin.label);
        m_buttonStyle = new GUIStyle(GUI.skin.button);
        m_boxStyle = new GUIStyle(GUI.skin.box);
        m_toggleStyle = new GUIStyle(GUI.skin.toggle);
        m_toggleStyle.fixedWidth = 18;
        m_textAreaStyle = new GUIStyle(GUI.skin.textArea);
        m_textFieldStyle = new GUIStyle(EditorStyles.textField);
    }

    public static GUIStyle GetGUIStyle(EGUIStyle style, GUIStyle defaultStyle)
    {
        switch (style)
        {
            default:
            case EGUIStyle.Default: return defaultStyle;
            case EGUIStyle.Label: return m_labelStyle;
            case EGUIStyle.Help: return m_helpStyle;
            case EGUIStyle.Button: return m_buttonStyle;
            case EGUIStyle.Box: return m_boxStyle;
            case EGUIStyle.Toggle: return m_toggleStyle;
            case EGUIStyle.TextArea: return m_textAreaStyle;
            case EGUIStyle.TextField: return m_textFieldStyle;
        }
    }

    public static bool Button(string label, params GUILayoutOption[] options)
    {
        return Button(label, EGUIStyle.Button, options);
    }

    public static bool Button(string label, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        return GUILayout.Button(label, guiStyle, options);
    }

    public static void Button(string label, Action action, params GUILayoutOption[] options)
    {
        Button(label, action, EGUIStyle.Button, options);
    }

    public static void Button(string label, Action action, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        if (GUILayout.Button(label, guiStyle, options))
        {
            action();
        }
    }

    public static void Button<T>(string label, Action<T> action, T state, params GUILayoutOption[] options)
    {
        Button(label, action, state, EGUIStyle.Button, options);
    }

    public static void Button<T>(string label, Action<T> action, T state, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        if (GUILayout.Button(label, guiStyle, options))
        {
            action(state);
        }
    }

    public static bool Button<R>(string label, out R res, Func<R> func, params GUILayoutOption[] options)
    {
        return Button(label, out res, func, EGUIStyle.Button, options);
    }

    public static bool Button<R>(string label, out R res, Func<R> func, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        if (GUILayout.Button(label, guiStyle, options))
        {
            res = func();
            return true;
        }

        res = default;
        return false;
    }

    public static bool Button<T, R>(string label, out R res, Func<T, R> func, T state, params GUILayoutOption[] options)
    {
        return Button(label, out res, func, state, EGUIStyle.Button, options);
    }

    public static bool Button<T, R>(string label, out R res, Func<T, R> func, T state, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        if (GUILayout.Button(label, guiStyle, options))
        {
            res = func(state);
            return true;
        }

        res = default;
        return false;
    }

    public static bool ToggleRight(string label, bool value, params GUILayoutOption[] options)
    {
        return ToggleRight(label, value, EGUIStyle.Toggle, options);
    }

    public static bool ToggleRight(string label, bool value, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        GUILayout.BeginHorizontal(options);

        if (GUILayout.Button(label, m_labelStyle))
        {
            value = !value;
        }

        value = EditorGUILayout.Toggle(value, guiStyle);

        GUILayout.EndHorizontal();

        return value;
    }

    public static bool ToggleLeft(string label, bool value, params GUILayoutOption[] options)
    {
        return ToggleLeft(label, value, EGUIStyle.Toggle, options);
    }

    public static bool ToggleLeft(string label, bool value, EGUIStyle style, params GUILayoutOption[] options)
    {
        var guiStyle = GetGUIStyle(style, m_buttonStyle);

        EditorGUILayout.BeginHorizontal(options);

        value = EditorGUILayout.Toggle(value, guiStyle);

        if (GUILayout.Button(label, m_labelStyle))
        {
            value = !value;
        }

        EditorGUILayout.EndHorizontal();

        return value;
    }

    public static void Help(string text, MessageType type = MessageType.Info)
    {
        EditorGUILayout.HelpBox(text, type, false);
    }

    public static void HelpWide(string text, MessageType type = MessageType.Info)
    {
        EditorGUILayout.HelpBox(text, type, true);
    }

    public static void HeaderNotBold(string text, float space = -1)
    {
        if (space >= -0.05)
        {
            EditorGUILayout.Space(space);
        }
        else
        {
            EditorGUILayout.Space(m_defaultHeaderSpace);
        }

        EditorGUILayout.LabelField(text, m_labelStyle);
    }

    public static void Header(string text, float space = -1)
    {
        if (space >= -0.05)
        {
            EditorGUILayout.Space(space);
        }
        else
        {
            EditorGUILayout.Space(m_defaultHeaderSpace);
        }

        EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
    }
}

public enum EGUIStyle
{
    Default,
    Label,
    Box,
    Button,
    Help,
    Toggle,
    TextArea,
    TextField,
}
#endif*/