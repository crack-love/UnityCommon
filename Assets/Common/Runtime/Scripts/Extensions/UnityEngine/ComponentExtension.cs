using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ComponentExtension
    {
        readonly static List<Component> s_buffer = new List<Component>();

        /// <summary>
        /// Get component at index
        /// </summary>
        public static Component GetComponentAt(this Component src, int idx)
        {
            return src.gameObject.GetComponentAt(idx);
        }

        public static Component GetComponentAt(this Component src, Type type, int idx)
        {
            return src.gameObject.GetComponentAt(type, idx);
        }

        /// <summary>
        /// Get component at index where T types
        /// </summary>
        public static T GetComponentAt<T>(this Component src, int idx) where T : Component
        {
            return src.gameObject.GetComponentAt<T>(idx);
        }


        /// <summary>
        /// Get all components and find match index
        /// </summary>
        public static int GetComponentIndex(this Component src)
        {
            s_buffer.Clear();
            src.GetComponents(s_buffer);

            int size = s_buffer.Count;
            for (int i = 0; i < size; ++i)
            {
                if (s_buffer[i] == src)
                {
                    s_buffer.Clear();
                    return i;
                }
            }

            s_buffer.Clear();
            return -1;
        }

        public static IEnumerable<Component> GetComponentsEnumerable(this Component src, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsEnumerable(includeInActive);
        }

        public static IEnumerable<T> GetComponentsEnumerable<T>(this Component src, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsEnumerable<T>(includeInActive);
        }

        public static IEnumerable<Component> GetComponentsEnumerable(this Component src, Type type, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsEnumerable(type, includeInActive);
        }

        public static IEnumerable<Component> GetComponentsInChildrenEnumerable(this Component src, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsInChildrenEnumerable(includeInActive);
        }

        public static IEnumerable<T> GetComponentsInChildrenEnumerable<T>(this Component src, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsInChildrenEnumerable<T>(includeInActive);
        }

        public static IEnumerable<Component> GetComponentsInChildrenEnumerable(this Component src, Type type, bool includeInActive = false)
        {
            return src.gameObject.GetComponentsInChildrenEnumerable(type, includeInActive);
        }
    }
}