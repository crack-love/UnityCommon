using System;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 2022-03-24 오후 3:25:19, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common
{
    /// <summary>
    /// 인스펙터 메소드 버튼. 필드 바인딩 필요
    /// </summary>
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        const float k_defaultWidth = 250f;
        const bool k_defaultExtend = false;

        string m_label;

        public string Method { get; }
        public string[] Params { get; }
        /// <summary>
        /// 버튼 텍스트
        /// </summary>
        public string Label 
        {
            get => m_label ?? Method;
            set => m_label = value; 
        }
        /// <summary>
        /// 버튼 가로 길이 확장
        /// </summary>
        public bool Extend { get; set; }
        /// <summary>
        /// 버튼 가로 길이
        /// </summary>
        public float Width { get; set; }

        /// <param name="method">버튼 클릭시 호출할 메서드 이름</param>
        /// <param name="pars">메서드에 인수로 전달되는 필드 이름들 (Instance,Static,Private and aboves)</param>
        public ButtonAttribute(string method, params string[] pars)
        {
            Method = method;
            Label = null;
            Width = k_defaultWidth;
            Extend = k_defaultExtend;
            Params = pars;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    class BtnAttributeDrawer : PropertyDrawer
    {
        GUIContent m_labelContent;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var att = attribute as ButtonAttribute;
            var width = att.Extend ? EditorGUIUtility.currentViewWidth : att.Width;
            m_labelContent ??= new GUIContent(att.Label);

            return GUI.skin.button.CalcHeight(m_labelContent, width);
        }

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            // 기존 프로퍼티 드로우
            // EditorGUILayout.PropertyField(prop);

            var att = attribute as ButtonAttribute;

            // 버튼 위치
            var x = EditorGUIUtility.currentViewWidth / 2f - att.Width / 2f;
            var y = position.y;
            var btnHeight = position.height;
            Rect buttonRect = new Rect(x, y, att.Width, btnHeight);

            // 버튼 드로우
            if (GUI.Button(buttonRect, att.Label))
            {
                // 리플렉션 초기화
                object target = prop.serializedObject.targetObject;
                Type classType = target.GetType();
                Type[] parTypes = new Type[0];
                object[] parValues = new object[0];
                MethodInfo methodInfo = null;
                var pars = att.Params;

                // Get Pars
                if (pars != null)
                {
                    parTypes = new Type[pars.Length];
                    parValues = new object[pars.Length];

                    for (int i = 0; i < pars.Length; ++i)
                    {
                        var field = classType.GetField(pars[i],
                            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        parTypes[i] = field.FieldType;
                        parValues[i] = field.GetValue(target);
                    }
                }

                // Get Method
                methodInfo = classType.GetMethod(att.Method,
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
                    null, parTypes, null);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(target, parValues);
                }
                // Not Found, Log Error
                else
                {
                    string partypes = "{ ";
                    string parvalues = "{ ";

                    foreach (var v in parTypes)
                        partypes += v.ToString() + " ";

                    foreach (var v in parValues)
                        parvalues += v.ToString() + " ";

                    partypes += "}";
                    parvalues += "}";

                    Debug.LogError(string.Format("InspectorButton: Unable to find method name={0} in type={1}, parTypes={2}, parValues={3}",
                        att.Method, classType, partypes, parvalues));
                }
            }
        }
    }
#endif
}