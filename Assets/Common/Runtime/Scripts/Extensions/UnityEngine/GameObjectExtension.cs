using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2022-03-25 오후 7:00:09, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace Common
{
    public static class GameObjectExtension
    {
        readonly static List<Component> s_buffer = new List<Component>();
        readonly static Stack<Transform> s_transStack = new Stack<Transform>();

        /// <summary>
        /// Get all components and return at index
        /// </summary>
        public static Component GetComponentAt(this GameObject src, int idx)
        {
            return GetComponentAt(src, typeof(Component), idx);
        }
        public static T GetComponentAt<T>(this GameObject src, int idx) where T : Component
        {
            return (T)GetComponentAt(src, typeof(T), idx);
        }

        public static Component GetComponentAt(this GameObject src, Type type, int idx)
        {
            s_buffer.Clear();
            src.GetComponents(type, s_buffer);
            var res = s_buffer[idx];
            s_buffer.Clear();

            return res;
        }

        public static IEnumerable<Component> GetComponentsEnumerable(this GameObject src, bool includeInActive = false)
        {
            return GetComponentsEnumerable(src, typeof(Component), includeInActive);
        }

        public static IEnumerable<T> GetComponentsEnumerable<T>(this GameObject src, bool includeInActive = false)
        {
            return (IEnumerable<T>)GetComponentsEnumerable(src, typeof(T), includeInActive);
        }

        public static IEnumerable<Component> GetComponentsEnumerable(this GameObject src, Type type, bool includeInActive = false)
        {
            s_buffer.Clear();
            src.transform.GetComponents(type, s_buffer);

            foreach (var cmp in s_buffer)
            {
                if (!includeInActive && !cmp.gameObject.activeSelf)
                    continue;

                yield return cmp;
            }

            s_buffer.Clear();
        }

        public static IEnumerable<Component> GetComponentsInChildrenEnumerable(this GameObject src, bool includeInActive = false)
        {
            return GetComponentsInChildrenEnumerable(src, typeof(Component), includeInActive);
        }

        public static IEnumerable<T> GetComponentsInChildrenEnumerable<T>(this GameObject src, bool includeInActive = false)
        {
            return (IEnumerable<T>)GetComponentsInChildrenEnumerable(src, typeof(T), includeInActive);
        }

        public static IEnumerable<Component> GetComponentsInChildrenEnumerable(this GameObject src, Type type, bool includeInActive = false)
        {
            var stack = s_transStack;
            var buffer = s_buffer;
            stack.Push(src.transform);

            while (stack.Count > 0)
            {
                Transform trans = stack.Pop();

                buffer.Clear();
                trans.GetComponents(type, buffer);
                
                foreach(var cmp in buffer)
                {
                    if (!includeInActive && !cmp.gameObject.activeSelf)
                        continue;

                    yield return cmp;
                }

                int childSize = trans.childCount;
                for (int i = 0; i< childSize; ++i)
                {
                    stack.Push(trans.GetChild(i));
                }
            }

            buffer.Clear();
        }
    }

}