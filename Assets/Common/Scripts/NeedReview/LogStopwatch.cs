using UnityEngine;
using UnityCommon;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// 2021-01-15 금 오후 10:13:11, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    public class LogStopwatch
    {
        static Stopwatch sw = new Stopwatch();

        public static void BeginStopwatch()
        {
            sw.Restart();
        }

        public static void EndStopwatch(string msg)
        {
            sw.Stop();

            Debug.Log($"{sw.Elapsed} elapsed : {msg}");
        }
    }
}