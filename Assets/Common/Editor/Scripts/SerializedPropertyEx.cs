/*using System;
using UnityEditor;

/// <summary>
/// 2020-06-10 수 오후 4:21:56, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace CommonEditor
{
    public static class SerializedPropertyEx
    {
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        public static object GetObjectOfProperty(this SerializedProperty prop)
        {
            if (prop == null) return null;
            
            // System object
            object obj = prop.serializedObject.targetObject;

            // remove array property from path
            var path = prop.propertyPath.Replace(".Array.data[", "[");

            // Find object
            // Start at root
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                // is Array Element
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));

                    obj = obj.GetValueReflectionElement(elementName, index);
                }
                // OR
                else
                {
                    obj = obj.GetValueReflection(element);
                }
            }

            return obj;
        }

    }
}*/