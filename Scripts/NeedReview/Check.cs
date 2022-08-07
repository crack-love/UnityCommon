#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.IO;
using System.Reflection;
using System.Linq;

/// <summary>
/// 2021-04-04 일 오후 2:21:33, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    /// <summary>
    /// Exception handler
    /// </summary>
    public static class Check
    {
        static string DefaultWhenMessage = "Condition Fail";
        static string DefaultNullMessage = "Object is Null";
        static string DefaultThrowMessage = "Exception Thrown";

        public static void When(bool condition)
        {
            if (condition)
            {
                var e = CreateException(DefaultWhenMessage);

                HandleException(e);
            }
        }

        public static void When(bool condition, object msg)
        {
            if (condition)
            {
                var e = CreateException(msg);

                HandleException(e);
            }
        }

        public static void Null(object o)
        {
            if (o == null)
            {
                var e = CreateException(DefaultNullMessage);

                HandleException(e);
            }
        }

        public static void Null(object o, object msg)
        {
            if (o == null)
            {
                var e = CreateException(msg);

                HandleException(e);
            }
        }

        public static void Throw(Exception e)
        {
            e = CreateException(e.Message);

            HandleException(e);
        }

        public static void Throw(object msg)
        {
            var e = CreateException(msg);

            HandleException(e);
        }

        static void HandleException(Exception e)
        {
            throw e;
        }

        static Exception CreateException(object msg)
        {
            return new CheckException(msg.ToString());
        }

        class CheckException : Exception
        {
            string m_message;

            public CheckException(string msg = "An exception has thrown")
            {
                m_message = msg;
            }

            public override string Message => m_message;

            public override string StackTrace
            {
                get
                {
                    var src = base.StackTrace;

                    while (src != null)
                    {
                        var start = src.IndexOf(" at UnityCheck.");
                        if (start < 0)
                        {
                            break;
                        }

                        var carriageReturn = src.IndexOf("\r", start);
                        var lineFeed = src.IndexOf("\n", start);
                        var length = (carriageReturn < 0 ? 0 : 1) + (lineFeed < 0 ? 0 : 1);
                        var end = Mathf.Max(carriageReturn, lineFeed);

                        if (length < 0)
                        {
                            break;
                        }

                        src = src.Remove(start, end - start + length);
                    }

                    return src;
                }
            }
        }
    }
}