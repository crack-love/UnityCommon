using System;
using UnityEngine;

namespace UnityCommon
{
    /// <summary>
    /// 열거형 로직 연산자 
    /// </summary>
    [Flags]
    [Serializable]
    public enum Logic
    {
        None = 0,
        Less = 1,
        LessSame = 2,
        More = 4,
        MoreSame = 8,
        Same = 16,
        Not = 32,
    }

    public interface ILogicable<in T>
    {
        bool Compare(Logic logic, T other);
    }

    public static class LogicExtension
    {
        public static bool Compare<T>(this Logic e, T a, T b) where T : ILogicable<T>
        {
            return a.Compare(e, b);
        }

        public static bool Compare<T, U>(this Logic e, T a, U b) where T : ILogicable<U>
        {
            return a.Compare(e, b);
        }

        public static bool Compare(this Logic e, float a, float b)
        {
            return e switch
            {
                Logic.None => false,
                Logic.Less => a < b,
                Logic.LessSame => a <= b,
                Logic.More => a > b,
                Logic.MoreSame => a >= b,
                Logic.Same => Mathf.Approximately(a, b),
                Logic.Not => !Mathf.Approximately(a, b),
                _ => throw new NotImplementedException(),
            };
        }

        public static bool Calculate(this Logic e, Vector3 a, Vector3 b)
        {
            return e switch
            {
                Logic.None => false,
                Logic.Less => a.x < b.x && a.y < b.y && a.z < b.z,
                Logic.LessSame => a.x <= b.x && a.y <= b.y && a.z <= b.z,
                Logic.More => a.x > b.x && a.y > b.y && a.z > b.z,
                Logic.MoreSame => a.x >= b.x && a.y >= b.y && a.z >= b.z,
                Logic.Same => Mathf.Approximately(a.x, b.x) &&
                    Mathf.Approximately(a.y, b.y) &&
                    Mathf.Approximately(a.z, b.z),
                Logic.Not => !Mathf.Approximately(a.x, b.x) &&
                !Mathf.Approximately(a.y, b.y) &&
                !Mathf.Approximately(a.z, b.z),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
