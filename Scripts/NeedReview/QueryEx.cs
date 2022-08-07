#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 2021-04-05 월 오후 12:40:41, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public static class QueryEx
    {
        /// <summary>
        /// Source, destination Type have to be clarified
        /// </summary>
        public static IEnumerable<TD> Cast<TS, TD>(this IEnumerable<TS> src)
        {
            return Query.Cast<TS, TD>(src);
        }

        /// <summary>
        /// Source, destination Type have to be clarified
        /// </summary>
        public static IEnumerable<TD> Cast<TS, TD>(this IList<TS> src)
        {
            return Query.Cast<TS, TD>(src);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> src, Func<T, bool> predicter)
        {
            return Query.Where<T>(src, predicter);
        }

        public static IEnumerable<T> Where<T>(this IList<T> src, Func<T, bool> predicter)
        {
            return Query.Where<T>(src, predicter);
        }

        public static IEnumerable<TD> Select<TS, TD>(this IEnumerable<TS> src, Func<TS, TD> selecter)
        {
            return Query.Select(src, selecter);
        }

        public static IEnumerable<TD> Select<TS, TD>(this IList<TS> src, Func<TS, TD> selecter)
        {
            return Query.Select(src, selecter);
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> src)
        {
            return Query.Distinct(src);
        }

        public static IEnumerable<T> Distinct<T>(this IList<T> src)
        {
            return Query.Distinct(src);
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> src, Func<T, T, bool> distinction)
        {
            return Query.Distinct(src, distinction);
        }

        public static IEnumerable<T> Distinct<T>(this IList<T> src, Func<T, T, bool> distinction)
        {
            return Query.Distinct(src, distinction);
        }
    }
}