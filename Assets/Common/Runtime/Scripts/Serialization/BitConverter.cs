// Mono.Security.BitConverterLE.cs
//  Like System.BitConverter but always little endian
// Author:
//   Bernie Solomon
//
// Modify
//  Original method to bytes allocating garbage so add some code it not
//  happen using parameter buffer
//  YGK

namespace Common
{
    /// <summary>
    /// Converting between primitive with byte[] always LittleEndian
    /// </summary>
    internal static class BitConverter
    {
        public const int CharByte = 2;
        public const int ShortByte = 2;
        public const int UShortByte = 2;
        public const int IntByte = 4;
        public const int UIntByte = 4;
        public const int FloatByte = 4;
        public const int LongByte = 8;
        public const int ULongByte = 8;
        public const int DoubleByte = 8;
        public const int DecimalByte = 16;

        public static readonly bool IsLittleEndian = System.BitConverter.IsLittleEndian;
        
        unsafe static void GetBytes(byte* src, byte[] dst, int size)
        {
            if (IsLittleEndian)
            {
                for (int i = 0; i < size; ++i)
                {
                    dst[i] = src[i];
                }
            }
            else
            {
                for (int i = 0; i < size; ++i)
                {
                    dst[i] = src[size - i];
                }
            }
        }

        unsafe static int GetBytes(byte* src, byte[] dst, int start, int size)
        {
            if (IsLittleEndian)
            {
                for (int i = 0; i < size; ++i)
                {
                    dst[i] = src[start + i];
                }
            }
            else
            {
                for (int i = 0; i < size; ++i)
                {
                    dst[i] = src[start + size - i];
                }
            }

            return start + size;
        }

        unsafe public static void GetBytes(bool value, byte[] buffer)
        {
            buffer[0] = value ? (byte)1 : (byte)0;
        }

        unsafe public static int GetBytes(bool value, byte[] buffer, int start)
        {
            buffer[start] = value ? (byte)1 : (byte)0;

            return start + 1;
        }

        unsafe public static void GetBytes(char value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, CharByte);
        }

        unsafe public static int GetBytes(char value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, CharByte);
        }

        unsafe public static void GetBytes(short value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, ShortByte);
        }

        unsafe public static int GetBytes(short value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, ShortByte);
        }

        unsafe public static void GetBytes(ushort value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, UShortByte);
        }

        unsafe public static int GetBytes(ushort value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, UShortByte);
        }

        unsafe public static void GetBytes(int value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, IntByte);
        }

        unsafe public static int GetBytes(int value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, IntByte);
        }

        unsafe public static void GetBytes(uint value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, UIntByte);
        }

        unsafe public static int GetBytes(uint value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, UIntByte);
        }

        unsafe public static void GetBytes(long value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, LongByte);
        }

        unsafe public static int GetBytes(long value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, LongByte);
        }

        unsafe public static void GetBytes(ulong value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, ULongByte);
        }

        unsafe public static int GetBytes(ulong value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, ULongByte);
        }

        unsafe public static void GetBytes(float value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, FloatByte);
        }

        unsafe public static int GetBytes(float value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, FloatByte);
        }

        unsafe public static void GetBytes(double value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, DoubleByte);
        }

        unsafe public static int GetBytes(double value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, DoubleByte);
        }

        unsafe public static void GetBytes(decimal value, byte[] buffer)
        {
            GetBytes((byte*)&value, buffer, DecimalByte);
        }

        unsafe public static int GetBytes(decimal value, byte[] buffer, int start)
        {
            return GetBytes((byte*)&value, buffer, start, DecimalByte);
        }

        // ------------------------------------------------------------------------------------

        unsafe static void FromBytes(byte* dst, byte[] src, int size)
        {
            for (int i = 0; i < size; ++i)
            {
                dst[i] = src[i];
            }
        }

        unsafe static int FromBytes(byte* dst, byte[] src, int start, int size)
        {
            for (int i = 0; i < size; ++i)
            {
                dst[i] = src[start + i];
            }

            return start + size;
        }

        unsafe public static bool ToBoolean(byte[] value)
        {
            return value[0] != 0;
        }

        unsafe public static bool ToBoolean(byte[] value, ref int start)
        {
            var res = value[start] != 0;

            start += 1;

            return res;
        }

        unsafe public static char ToChar(byte[] value)
        {
            char res;

            FromBytes((byte*)&res, value, CharByte);

            return res;
        }

        unsafe public static char ToChar(byte[] value, ref int start)
        {
            char res;

            start = FromBytes((byte*)&res, value, start, CharByte);

            return res;
        }

        unsafe public static short ToInt16(byte[] value)
        {
            short res;

            FromBytes((byte*)&res, value, ShortByte);

            return res;
        }

        unsafe public static short ToInt16(byte[] value, ref int start)
        {
            short res;

            start = FromBytes((byte*)&res, value, start, ShortByte);

            return res;
        }

        unsafe public static ushort ToUInt16(byte[] value)
        {
            ushort res;

            FromBytes((byte*)&res, value, UShortByte);

            return res;
        }

        unsafe public static ushort ToUInt16(byte[] value, ref int start)
        {
            ushort res;

            start = FromBytes((byte*)&res, value, start, UShortByte);

            return res;
        }

        unsafe public static int ToInt32(byte[] value)
        {
            int res;

            FromBytes((byte*)&res, value, IntByte);

            return res;
        }

        unsafe public static int ToInt32(byte[] value, ref int start)
        {
            int res;

            start = FromBytes((byte*)&res, value, start, IntByte);

            return res;
        }

        unsafe public static uint ToUInt32(byte[] value)
        {
            uint res;

            FromBytes((byte*)&res, value, UIntByte);

            return res;
        }

        unsafe public static uint ToUInt32(byte[] value, ref int start)
        {
            uint res;

            start = FromBytes((byte*)&res, value, start, UIntByte);

            return res;
        }

        unsafe public static long ToInt64(byte[] value)
        {
            long res;

            FromBytes((byte*)&res, value, LongByte);

            return res;
        }

        unsafe public static long ToInt64(byte[] value, ref int start)
        {
            long res;

            start = FromBytes((byte*)&res, value, start, LongByte);

            return res;
        }

        unsafe public static ulong ToUInt64(byte[] value)
        {
            ulong res;

            FromBytes((byte*)&res, value, ULongByte);

            return res;
        }

        unsafe public static ulong ToUInt64(byte[] value, ref int start)
        {
            ulong res;

            start = FromBytes((byte*)&res, value, start, ULongByte);

            return res;
        }

        unsafe public static float ToSingle(byte[] value)
        {
            float res;

            FromBytes((byte*)&res, value, FloatByte);

            return res;
        }

        unsafe public static float ToSingle(byte[] value, ref int start)
        {
            float res;

            start = FromBytes((byte*)&res, value, start, FloatByte);

            return res;
        }

        unsafe public static double ToDouble(byte[] value)
        {
            double res;

            FromBytes((byte*)&res, value, DoubleByte);

            return res;
        }

        unsafe public static double ToDouble(byte[] value, ref int start)
        {
            double res;

            start = FromBytes((byte*)&res, value, start, DoubleByte);

            return res;
        }

        unsafe public static decimal ToDecimal(byte[] value)
        {
            decimal res;

            FromBytes((byte*)&res, value, DecimalByte);

            return res;
        }

        unsafe public static decimal ToDecimal(byte[] value, ref int start)
        {
            decimal res;

            start = FromBytes((byte*)&res, value, start, DecimalByte);

            return res;
        }
    }
}