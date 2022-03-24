using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 2022-03-20 오후 11:56:31, 4.0.30319.42000, DESKTOP-PD5J20B, maste
/// </summary> 
namespace CommonEditor
{
    class ComparisonComparer<T> : Comparer<T>
    {
        readonly Comparison<T> m_cmparison;

        public ComparisonComparer(Comparison<T> comparison)
        {
            m_cmparison = comparison;
        }

        public override int Compare(T x, T y)
        {
            return m_cmparison(x, y);
        }
    }
}