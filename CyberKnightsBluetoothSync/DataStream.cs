using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberKnightsBluetoothSync
{
    /**
     * Handles reading data from a stream written by Java in Network Byte Order.
     * This also internally handles any buffering needed to ensure multi-byte reads
     * only return when the full payload has been read.
     */
    class DataStream
    { 
        private byte[] Buffer = new byte[1024];
        private Stream Stream;

        public DataStream(Stream stream)
        {
            this.Stream = stream;

        }

        private byte[] ReadExactly(int count)
        {
            if (count >= Buffer.Length)
            {
                Buffer = new byte[count * 2];
            }

            int offset = 0;
            while (offset < count)
            {
                int read = Stream.Read(Buffer, offset, count - offset);
                Console.WriteLine("    Read {0} @ {1}/{2}: [{3}]", read, (offset + read), count, String.Join(" ", Buffer.Take(count).ToArray()));
                if (read == 0)
                    throw new System.IO.EndOfStreamException();
                offset += read;
            }
            System.Diagnostics.Debug.Assert(offset == count);
            return Buffer;
        }

        public int ReadInt1()
        {
            return ReadExactly(1)[0];
        }

        public void WriteInt1(int v)
        {
            Stream.WriteByte((byte) v);
        }

        public int ReadInt16()
        {
            byte[] bytes = ReadExactly(2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 2);
            }
            int v = BitConverter.ToInt16(bytes, 0);
            return v;
        }

        public int ReadInt32()
        {
            byte[] bytes = ReadExactly(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 4);
            }
            int v = BitConverter.ToInt32(bytes, 0);
            return v;
        }

        public long ReadInt64()
        {
            byte[] bytes = ReadExactly(8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 8);
            }
            long v = BitConverter.ToInt64(bytes, 0);
            return v;
        }

        public String ReadUTF8()
        {
            int len = ReadInt16();
            String s = System.Text.Encoding.UTF8.GetString(ReadExactly(len), 0, len);
            return s;
        }

        public byte[] ReadBytes(int len)
        {
            byte[] sub = new byte[len];
            Array.Copy(ReadExactly(len), 0, sub, 0, len);
            return sub;
        }

        public void ReadBytes(Stream destinationStream, int len)
        {
            int offset = 0;
            while (offset < len)
            {
                int read = Stream.Read(Buffer, 0, Math.Min(Buffer.Length, len - offset));
                destinationStream.Write(Buffer, 0, read);
                if (read == 0)
                    throw new System.IO.EndOfStreamException();
                offset += read;
            }
            System.Diagnostics.Debug.Assert(offset == len);
        }

        internal void Flush()
        {
            Stream.Flush();
        }
    }
}
