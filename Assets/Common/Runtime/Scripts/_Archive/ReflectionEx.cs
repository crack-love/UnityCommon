using System;
using System.Reflection;
using UnityEngine;

namespace Common.Reflection
{
    public static class ReflectionEx
    {
        public static bool HasSingleParameter(this MethodInfo info)
        {
            return info.GetParameters().Length == 1;
        }

        public static bool HasSingleParameterOfEqualsOrDerived(this MethodInfo info, Type type)
        {
            var pars = info.GetParameters();
            
            if (pars.Length > 1 || pars.Length <= 0) return false;
            if (pars[0].ParameterType.Equals(type)) return true;
            if (pars[0].ParameterType.IsSubclassOf(type)) return true;
            
            return false;
        }

        /// <summary>
        /// (Reflection) Field or Property
        /// </summary>
        public static object GetValueReflection(this object src, string name)
        {
            if (src == null) return null;

            var type = src.GetType();

            while (type != null)
            {
                var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (field != null) return field.GetValue(src);

                var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (property != null) return property.GetValue(src, null);

                type = type.BaseType;
            }

            return null;
        }

        /// <summary>
        /// (Reflection) IEnumerable TODO:IEnumerable => IList
        /// </summary>
        public static object GetValueReflectionElement(this object src, string name, int idx)
        {
            var enumerable = GetValueReflection(src, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;

            var enumerator = enumerable.GetEnumerator();

            for (int i = 0; i <= idx; ++i)
            {
                if (!enumerator.MoveNext())
                    return null;
            }

            return enumerator.Current;
        }



        /// <summary>
        /// (reflection) Self as root, findChild, null ? addComponent to self, not recursive
        /// </summary>
        public static void ValidateComponentsReflection(this Component selfAsRoot)
        {
            ValidateComponentsReflection<Component>(selfAsRoot, selfAsRoot, true, true, false);
        }

        /// <summary>
        /// (reflection) Self as root, findChild, null ? addComponent to self, not recursive, Specify type
        /// </summary>
        public static void ValidateComponentsReflection<T>(this Component selfAsRoot) where T : Component
        {
            ValidateComponentsReflection<T>(selfAsRoot, selfAsRoot, true, true, false);
        }

        /// <summary>
        /// (reflection) Specify type
        /// </summary>
        public static void ValidateComponentsReflection<T>(this Component self, Component findingRoot, bool findChild, bool addComponent, bool recursiveBinding) where T : Component
        {
            Component c;

            // search fields
            foreach (var field in self.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // found field of type T
                if (field.FieldType.IsSubclassOf(typeof(T)) | field.FieldType == typeof(T))
                {
                    // finding exist componnt
                    if (findChild)
                    {
                        c = findingRoot.GetComponentInChildren<T>();
                    }
                    else
                    {
                        c = findingRoot.GetComponent(field.FieldType);
                    }

                    // not found component
                    if (!c)
                    {
                        if (addComponent)
                        {
                            c = self.gameObject.AddComponent<T>();
                        }
                    }

                    field.SetValue(self, c);

                    // recursive
                    if (recursiveBinding)
                    {
                        ValidateComponentsReflection<T>(c, findingRoot, findChild, addComponent, recursiveBinding);
                    }
                }
            }
        }
    }
}
