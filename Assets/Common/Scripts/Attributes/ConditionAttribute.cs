using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//------------------------------------------------------------------------------------------------------------------
/// <log>
/// 2020.03.27 : 네임스페이스 변경. 주석 정리
/// 2022.03.24 : change namespace UnityCommon to Common. add conditional attribute.
/// </log>
//------------------------------------------------------------------------------------------------------------------
namespace Common
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ConditionAttribute : PropertyAttribute
    {
        public bool HideOnFalse;
        public string BooleanFieldName;

		public ConditionAttribute(string booleanFieldName, bool hideOnFalse = true)
        {
            HideOnFalse = hideOnFalse;
            BooleanFieldName = booleanFieldName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionAttribute))]
    class ConditionAttributeDrawer : PropertyDrawer
    {
        public bool _isConditionOK;

        void CheckCondition(SerializedProperty property)
        {
            // 초기화
            var att = attribute as ConditionAttribute;
            var bfName = att.BooleanFieldName;
            bool isPropertyFound = false;
            var propertySearching = property.Copy();
            var propertyBoolean = false;

            // 1. 최상위 타입 루트로 이동
            // 2. 최상위 루트부터 현재 타입까지 서치
            // 3. 현재 타입에서 필드 서치

            // 1. 최상위 타입 루트로 이동
            int length = propertySearching.propertyPath.LastIndexOf('.');
            var path = propertySearching.propertyPath.Substring(0, length);
            propertySearching.Reset();

            // 2. 최상위 루트부터 현재 타입까지 서치
            while (propertySearching.NextVisible(true))
            {
                if (propertySearching.propertyPath == path)
                {
                    break;
                }
            }

            // 3. 현재 타입에서 필드 서치
            while (propertySearching.NextVisible(true))
            {
                if (propertySearching.name == bfName)
                {
                    isPropertyFound = true;
                    propertyBoolean = propertySearching.boolValue;
                    break;
                }
            }

            if (!isPropertyFound)
                throw new Exception("Property not found");

            if (propertyBoolean)
                _isConditionOK = att.HideOnFalse ? true : false;
            else
                _isConditionOK = att.HideOnFalse ? false : true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            CheckCondition(property);

            if (_isConditionOK)
            {
                return base.GetPropertyHeight(property, label);
            }
            else
            {
                return 0;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CheckCondition(property);

            if (_isConditionOK)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                return;
            }
        }
    }
    #endif
}