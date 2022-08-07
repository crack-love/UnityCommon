using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Assertions;

/// <summary>
/// 2021-04-04 일 오후 2:07:43, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace Common
{
    /// <summary>
    /// Custom Binary Reader not allocating garbage
    /// </summary>
    // System BinaryReader가 Garabage를 발생시키므로 새로 작성함 (.Net Core 참고)
    public class BinaryReader : IDisposable
    {
        const int MinBufferSize = 16; // 16 from Decimal byte size
        const int MassBufferSize = 256;

        // todo : no encoding decoding? when field setted null
        //        encoding/decoding lower performance
        Stream m_stream;
        readonly byte[] m_buffer;
        readonly Encoding m_encoding;
        readonly Decoder m_decoder; 
        readonly bool m_isChar2Byte;

        // lazy allocates
        StringBuilder m_sb;
        byte[] m_massBuffer;
        char[] m_massCharBuffer; 
        char[] m_singleCharBuffer;

        public BinaryReader() : this(Stream.Null)
        {
        }
        public BinaryReader(Stream stream) : this(stream, new UTF8Encoding(false, true))
        {
        }

        public BinaryReader(Stream stream, Encoding encoding)
        {
            Assert.IsNotNull(stream);
            Assert.IsNotNull(encoding);

            m_stream = stream;
            m_encoding = encoding;
            m_decoder = encoding.GetDecoder();
            m_isChar2Byte = encoding is UnicodeEncoding;
            var bufferSize = Math.Max(encoding.GetMaxByteCount(1), MinBufferSize);
            m_buffer = new byte[bufferSize];
        }

        void ValidateMassBufferAndStringBuilder()
        {
            if (m_massBuffer == null)
            {
                var massBufferCharSize = m_encoding.GetMaxCharCount(MassBufferSize);
                m_massBuffer = new byte[MassBufferSize];
                m_massCharBuffer = new char[massBufferCharSize];
            }

            m_sb ??= new StringBuilder();
            m_sb.Clear();
        }

        /// <summary>
        /// Byte Position
        /// </summary>
        public long Position
        {
            get => m_stream.Position;
        }

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
        /// Return -1 when eof
        /// </summary>
        public int PeekChar()
        {
            if (!m_stream.CanSeek)
            {
                return -1;
            }
            else
            {
                long origPos = m_stream.Position;
                int res = InternalReadOneChar();
                m_stream.Position = origPos;

                return res;
            }
        }

        public byte ReadByte()
        {
            FillBufferSingle();
            return m_buffer[0];
        }

        public sbyte ReadSByte()
        {
            FillBufferSingle();
            return (sbyte)m_buffer[0];
        }

        /// <summary>
        /// return value can be -1 as eof
        /// </summary>
        public int ReadChar()
        {
            int value = InternalReadOneChar();
            return value;
        }

        public bool ReadBoolean()
        {
            FillBufferSingle();
            return BitConverter.ToBoolean(m_buffer);
        }

        public short ReadInt16()
        {
            FillBuffer(BitConverter.ShortByte);
            return BitConverter.ToInt16(m_buffer);
        }

        public ushort ReadUInt16()
        {
            FillBuffer(BitConverter.UShortByte);
            return BitConverter.ToUInt16(m_buffer);
        }

        public int ReadInt32()
        {
            FillBuffer(BitConverter.IntByte);
            return BitConverter.ToInt32(m_buffer);
        }

        public uint ReadUInt32()
        {
            FillBuffer(BitConverter.UIntByte);
            return BitConverter.ToUInt32(m_buffer);
        }

        public long ReadInt64()
        {
            FillBuffer(BitConverter.LongByte);
            return BitConverter.ToInt64(m_buffer);
        }

        public ulong ReadUInt64()
        {
            FillBuffer(BitConverter.ULongByte);
            return BitConverter.ToUInt64(m_buffer);
        }

        public float ReadSingle()
        {
            FillBuffer(BitConverter.FloatByte);
            return BitConverter.ToSingle(m_buffer);
        }

        public double ReadDouble()
        {
            FillBuffer(BitConverter.DoubleByte);
            return BitConverter.ToDouble(m_buffer);
        }

        public decimal ReadDecimal()
        {
            FillBuffer(BitConverter.DecimalByte);
            return BitConverter.ToDecimal(m_buffer);
        }

        /// <summary>
        /// Read string via decoding
        /// </summary>
        public string ReadString()
        {
            // read remain byte
            var remainByte = Read7BitEncodedInt();
            Assert.IsTrue(remainByte >= 0);
            if (remainByte == 0)
            {
                return string.Empty;
            }

            // init
            ValidateMassBufferAndStringBuilder();
            m_decoder.Reset();

            // read loop
            var readingByte = 0;
            var readingChar = 0;
            while (remainByte > 0)
            {
                readingByte = Math.Min(remainByte, MassBufferSize);

                // read
                readingByte = m_stream.Read(m_massBuffer, 0, readingByte);
                if (readingByte <= 0)
                {
                    throw new Exception("End of stream but reamin count exist");
                }

                // decode
                readingChar = m_decoder.GetChars(m_massBuffer, 0, readingByte, m_massCharBuffer, 0, false);

                m_sb.Append(m_massCharBuffer, 0, readingChar);
                remainByte -= readingByte;
            }

            return m_sb.ToString();
        }

        public int ReadChars(char[] buffer, int index, int count)
        {
            Assert.IsNotNull(buffer);
            Assert.IsFalse(index < 0 || count < 0);

            ValidateMassBufferAndStringBuilder();
            m_decoder.Reset();

            var charRemain = count;

            while (charRemain > 0)
            {
                var readByteCnt = m_isChar2Byte ? charRemain * 2 : charRemain;
                readByteCnt = Math.Min(readByteCnt, MassBufferSize);

                // read
                readByteCnt = m_stream.Read(m_massBuffer, 0, readByteCnt);
                if (readByteCnt <= 0)
                {
                    // eof
                    return count - charRemain;
                }

                // decode
                var decodeCharCnt = 0;
                unsafe
                {
                    fixed (byte* pMass = m_massBuffer)
                    {
                        fixed (char* pDst = buffer)
                        {
                            decodeCharCnt = m_decoder.GetChars(pMass, readByteCnt, pDst + index, charRemain, false);
                        }
                    }
                }

                charRemain -= decodeCharCnt;
                index += decodeCharCnt;
            }

            Assert.IsFalse(charRemain < 0);

            return count - charRemain;
        }

        public int ReadBytes(byte[] buffer, int index, int count)
        {
            Assert.IsNotNull(buffer);
            Assert.IsFalse(index < 0 || count < 0 || buffer.Length < count + index);

            return m_stream.Read(buffer, index, count);
        }

        public long ReadBytes(IList<byte> buffer, long count)
        {
            Assert.IsNotNull(buffer);
            Assert.IsFalse(count < 0);

            long readByte = 0;
            while (readByte < count && m_stream.CanRead)
            {
                buffer.Add((byte)m_stream.ReadByte());
                readByte += 1;
            }
            return readByte;
        }

        void FillBufferSingle()
        {
            int cnt = m_stream.Read(m_buffer, 0, 1);

            if (cnt == -1)
                throw new EndOfStreamException();
        }

        void FillBuffer(int cnt)
        {
            int readByte = 0;
            int currByte = 0;

            do
            {
                currByte = m_stream.Read(m_buffer, readByte, cnt);

                //eof
                if (currByte <= 0 && cnt > 0)
                    throw new EndOfStreamException();

                readByte += currByte;
                cnt -= currByte;

            }
            while (cnt > 0);
        }

        int InternalReadOneChar()
        {
            int charsRead = 0;
            int byteCnt = 0;

            m_singleCharBuffer ??= new char[1];

            m_decoder.Reset();

            while (charsRead == 0)
            {
                // We really want to know what the minimum number of bytes per char
                // is for our encoding.  Otherwise for UnicodeEncoding we'd have to
                // do ~1+log(n) reads to read n characters.
                // Assume 1 byte can be 1 char unless m_2BytesPerChar is true.
                byteCnt = m_isChar2Byte ? 2 : 1;

                // read first byte
                int b = m_stream.ReadByte();
                m_buffer[0] = (byte)b;
                if (b == -1)
                {
                    byteCnt = 0;
                }

                // read second byte
                if (byteCnt == 2)
                {
                    b = m_stream.ReadByte();
                    m_buffer[1] = (byte)b;
                    if (b == -1)
                    {
                        byteCnt = 1;
                    }
                }

                // eof
                if (byteCnt == 0)
                {
                    return -1;
                }

                charsRead = m_decoder.GetChars(m_buffer, 0, byteCnt, m_singleCharBuffer, 0, false);

                Assert.IsFalse(charsRead > 1);
            }

            return m_singleCharBuffer[0];
        }

        int Read7BitEncodedInt()
        {
            // Read out an Int32 7 bits at a time.  The high bit
            // of the byte when on means to continue reading more bytes.
            int count = 0;
            int shift = 0;
            byte b;
            do
            {
                // Check for a corrupted stream.  Read a max of 5 bytes.
                // In a future version, add a DataFormatException.
                if (shift == 5 * 7)  // 5 bytes max per Int32
                    throw new FormatException("Bad7BitInt32");

                // ReadByte handles end of stream cases for us.
                b = ReadByte();
                count |= (b & 0x7F) << shift;
                shift += 7; // next 7 bit
            } while ((b & 0x80) != 0);

            return count;
        }
    }
}