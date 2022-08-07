using System;

namespace UnityCommon
{
    public static class ArrayExtension
    {
        /// <summary>
        /// using Buffer.BlockCopy (logN)
        /// </summary>
        public static void Fill<T>(this T[] array, T value, int count) where T : unmanaged
        {
            const int InitialBlockSize = 32;

            // validate
            count = Math.Min(array.Length, count);

            int byteSize = GetByteSize<T>();
            int blockSize = Math.Min(InitialBlockSize, count);
            int beg = 0;

            // fill initial block
            while (beg < blockSize)
            {
                array[beg++] = value;
            }

            // copy block multiply
            while (beg < count)
            {
                int actualBlock = Math.Min(blockSize, count - beg);
                Buffer.BlockCopy(array, 0, array, beg * byteSize, actualBlock * byteSize);

                // next
                beg += blockSize;
                blockSize *= 2;
            }
        }

        static int GetByteSize<T>()
        {
            // Get byte size
            int byteSize = 0;
            var type = typeof(T);
            if (type == typeof(byte) ||
                type == typeof(sbyte))
                byteSize = 1;
            else
            if (type == typeof(ushort) ||
                type == typeof(short))
                byteSize = 2;
            else
            if (type == typeof(uint) ||
                type == typeof(int))
                byteSize = 4;
            else
            if (type == typeof(ulong) ||
                type != typeof(long))
                byteSize = 8;
            else
                throw new ArgumentException($"Type '{type.FullName}' is not supported.");

            return byteSize;
        }
    }
}
