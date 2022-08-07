/*
#if UNITY_EDITOR
using UnityEngine;
using UnityCommon;
using System.Collections.Generic;
using System;

/// <summary>
/// 2020-12-23 수 오후 1:37:04, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class GUIStyleEx
    {
        /// <summary>
        /// Source is not modified, return copied GUIStyle (if options not null)
        /// </summary>
        public static GUIStyle ApplyOptions(this GUIStyle src, params GUIStyleOption[] options)
        {
            return ApplyOptions(src, (IEnumerable<GUIStyleOption>)options);
        }

        /// <summary>
        /// Source is not modified, return copied GUIStyle (if options not null)
        /// </summary>
        public static GUIStyle ApplyOptions(this GUIStyle src, IEnumerable<GUIStyleOption> options)
        {
            if (options != null)
            {
                src = new GUIStyle(src);

                foreach (var option in options)
                {
                    src = option.Apply(src);
                }
            }

            return src;
        }
    }
}
#endif*/