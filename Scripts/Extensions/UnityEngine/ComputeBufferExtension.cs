using UnityEngine;

namespace UnityCommon
{
    public static class ComputeBufferExtension
    {
        // tools
        static readonly ComputeBuffer cntBuffer = new ComputeBuffer(1, 4, ComputeBufferType.Raw); // -2,147,483,648 ~ 2,147,483,647
        static readonly int[] cntArr = { 0 };

        public static int GetCount(this ComputeBuffer buffer)
        {
            ComputeBuffer.CopyCount(buffer, cntBuffer, 0);
            cntBuffer.GetData(cntArr);
            return cntArr[0];
        }

        /// <summary>
        /// return count (buffer is Countable buffer)
        /// </summary>
        public static int GetData<T>(this ComputeBuffer buffer, T[] desBuffer)
        {
            int cnt = GetCount(buffer);

            // get data
            buffer.GetData(desBuffer, 0, 0, cnt);

            // return count
            return cnt;
        }

        /// <summary>
        /// (buffer is Countable buffer)
        /// </summary>
        public static T[] GetData<T>(this ComputeBuffer buffer)
        {
            int cnt = GetCount(buffer);
            T[] res = new T[cnt];
            buffer.GetData(res, 0, 0, cnt);
            return res;
        }
    }
}
