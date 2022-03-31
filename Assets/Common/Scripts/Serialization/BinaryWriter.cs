using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Assertions;

/// <summary>
/// 2021-04-03 토 오후 9:01:34, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// Custom Binary Writer not allocating garbage
    /// </summary>
    // Reference BinaryReader's Comments
    public class BinaryWriter : IDisposable
    {
        const int MinBufferSize = 16;
        const int MassBufferSize = 256;

        Stream m_stream;
        readonly byte[] m_buffer;
        readonly Encoding m_encoding;
        readonly Encoder m_encoder;
        readonly bool m_isChar2Byte;
        readonly int m_massBufferCharSize;

        // lazy allocates
        byte[] m_massBuffer;

        public BinaryWriter() : this(Stream.Null)
        {
        }

        public BinaryWriter(Stream stream) : this(stream, new UTF8Encoding(false, true))
        {
        }

        public BinaryWriter(Stream stream, Encoding encoding)
        {
            Assert.IsNotNull(stream);
            Assert.IsNotNull(encoding);

            m_stream = stream;
            m_encoding = encoding;
            m_encoder = encoding.GetEncoder();
            m_isChar2Byte = encoding is UnicodeEncoding;
            var bufferSize = Math.Max(m_encoding.GetMaxByteCount(1), MinBufferSize);
            m_buffer = new byte[bufferSize];
            m_massBufferCharSize = m_encoding.GetMaxCharCount(MassBufferSize);
        }

        void ValidateMassBuffer()
        {
            if (m_massBuffer == null)
            {
                m_massBuffer = new byte[MassBufferSize];
            }
        }

        public long Position
        {
            get => m_stream.Position;
        }

        /// <summary>
        /// Close old stream
        /// </summary>
        public void SetStream(Stream stream, bool closeOldStream = true)
        {
            Assert.IsNotNull(stream);

            if (closeOldStream)
            {
                Close();
            }

            m_stream = stream;
        }

        public Stream GetStream()
        {
            return m_stream;
        }

        public long SeekCurrent(long offset)
        {
            return m_stream.Seek(offset, SeekOrigin.Current);
        }

        public long SeekBegin(long offset)
        {
            return m_stream.Seek(offset, SeekOrigin.Begin);
        }

        public long SeekEnd(long offset)
        {
            return m_stream.Seek(offset, SeekOrigin.End);
        }
        public void Close()
        {
            m_stream.Flush();
            m_stream.Close();
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>
        /// Prevent UnityEngine.Object's implicit convertion to bool
        /// </summary>
        public void WriteBool(bool value)
        {
            BitConverter.GetBytes(value, m_buffer);
            m_stream.Write(m_buffer, 0, 1);
        }

        public void Write(byte value)
        {
            m_stream.WriteByte(value);
        }

        public void Write(sbyte value)
        {
            m_stream.WriteByte((byte)value);
        }

        public unsafe void Write(char ch)
        {
            Assert.IsFalse(char.IsSurrogate(ch));

            int len = 0;
            fixed (byte* dst = m_buffer)
            {
                len = m_encoder.GetBytes(&ch, 1, dst, m_buffer.Length, true);
            }
            m_stream.Write(m_buffer, 0, len);
        }

        public void Write(double value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(decimal value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(short value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(ushort value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(int value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(uint value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(long value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(ulong value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        public void Write(float value)
        {
            m_stream.Write(m_buffer, 0, BitConverter.GetBytes(value, m_buffer, 0));
        }

        /// <summary>
        /// Write string via encoding
        /// </summary>
        public unsafe void Write(string value)
        {
            Assert.IsNotNull(value);

            int byteCnt = m_encoding.GetByteCount(value);

            Write7BitEncodedInt(byteCnt);

            fixed (char* pChar = value)
            {
                WritePtr(pChar, 0, value.Length);
            }
        }

        /// <summary>
        /// Write string each element as char value. does not encode/decode
        /// </summary>
        // this method needed??
        public void WriteStringChar(string value)
        {
            // write length??

            for (int i = 0; i < value.Length; ++i)
            {
                BitConverter.GetBytes(value[i], m_buffer);
                Write(m_buffer, 0, 2);
            }
        }

        public void Write(IList<byte> buffer)
        {
            int size = buffer.Count;

            for (int i = 0; i < size; ++i)
            {
                m_stream.WriteByte(buffer[i]);
            }
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int start, int count)
        {
            Assert.IsNotNull(buffer);

            m_stream.Write(buffer, start, count);
        }
        /// <summary>
        /// Write all length
        /// </summary>
        public void Write(char[] chars)
        {
            Assert.IsNotNull(chars);

            Write(chars, 0, chars.Length);
        }

        public unsafe void Write(char[] value, int index, int count)
        {
            Assert.IsNotNull(value);

            fixed (char* pChar = value)
            {
                WritePtr(pChar, index, count);
            }
        }

        unsafe void WritePtr(char* value, int index, int count)
        {
            ValidateMassBuffer();

            int currCharSize = 0;
            int currByteSize = 0;

            while (count > 0)
            {
                fixed (byte* pDst = m_massBuffer)
                {
                    currCharSize = Math.Min(count, m_massBufferCharSize);
                    currByteSize = m_encoder.GetBytes(value + index, currCharSize, pDst, m_massBuffer.Length, true);
                }

                m_stream.Write(m_massBuffer, 0, currByteSize);

                index += currCharSize;
                count -= currCharSize;
            }
        }

        protected void Write7BitEncodedInt(int value)
        {
            // Write out an int 7 bits at a time.  The high bit of the byte,
            // when on, tells reader to continue reading more bytes.

            uint v = (uint)value;   // not support negative numbers

            while (v > 0x80)
            {
                // write 7bits and flag(8th bit, on)
                Write((byte)(v | 0x80));

                v >>= 7;
            }

            // write reamin bits
            if (v > 0)
            {
                Write((byte)v);
            }
        }
    }
}