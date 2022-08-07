using System;

namespace Common
{
    /// <summary>
    /// 열거형 산술 연산자 
    /// </summary>
    [Serializable]
    public enum Arithmetic
    {
        None, Add, Minus, Multiply, Divide, Mod
    }

    public interface IArithmeticable<T>
    {
        T Compare(Arithmetic oper, T other);
    }

    public static class ArithmeticExtension
    {
        public static T Calculate<T>(this Arithmetic e, T a, T b) where T : IArithmeticable<T>
        {
            return a.Compare(e, b);
        }

        public static U Calculate<T, U>(this Arithmetic e, T a, U b) where T : IArithmeticable<U>
        {
            return a.Compare(e, b);
        }

        public static float Compare(this Arithmetic e, float x, float y)
        {
            return e switch
            {
                Arithmetic.None => x,
                Arithmetic.Add => x + y,
                Arithmetic.Minus => x - y,
                Arithmetic.Multiply => x * y,
                Arithmetic.Divide => y == 0 ? float.MaxValue : x / y,
                Arithmetic.Mod => y == 0 ? 0 : x % y,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
