using System;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//------------------------------------------------------------------------------------------------------------------
/// <log>
/// 2020-03-31 화 오후 4:38:06 : 네임스페이스 변경
/// </log>
//------------------------------------------------------------------------------------------------------------------
namespace UnityCommon
{
	/// <summary>
	/// Inspector ReadOnly
	/// </summary>
	public class ReadOnlyAttribute : PropertyAttribute
	{
		public readonly bool runtimeOnly;

		public ReadOnlyAttribute(bool runtimeOnly = false)
		{
			this.runtimeOnly = runtimeOnly;
		}
	}

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(ReadOnlyAttribute), true)]
	class ReadOnlyAttributeDrawer : PropertyDrawer
	{
		// Necessary since some properties tend to collapse smaller than their content
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		// Draw a disabled property field
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
            bool temp = GUI.enabled;

            if (((ReadOnlyAttribute)attribute).runtimeOnly)
            {
                // only disable at runtime, editor:enable
                GUI.enabled = !Application.isPlaying;
            }
            else
            {
                GUI.enabled = false;
            }

			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = temp;
		}
	}
	#endif
}