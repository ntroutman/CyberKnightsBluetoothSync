using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberKnightsBluetoothSync
{
    class DataStreamReader
    { 
        private byte[] buffer = new byte[1024];
        private Stream stream;

        public DataStreamReader(Stream stream)
        {
            this.stream = stream;

        }

        private byte[] ReadExactly(int count)
        {
            if (count >= buffer.Length)
            {
                buffer = new byte[count * 2];
            }

            int offset = 0;
            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new System.IO.EndOfStreamException();
                offset += read;
            }
            System.Diagnostics.Debug.Assert(offset == count);
            return buffer;
        }

        public int ReadInt1()
        {
            return ReadExactly(1)[0];
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
            byte[] bytes = ReadExactly(2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 4);
            }
            int v = BitConverter.ToInt32(bytes, 0);
            return v;
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
                int read = stream.Read(buffer, 0, Math.Min(buffer.Length, len - offset));
                destinationStream.Write(buffer, 0, read);
                if (read == 0)
                    throw new System.IO.EndOfStreamException();
                offset += read;
            }
            System.Diagnostics.Debug.Assert(offset == len);
        }

        public String ReadUTF8()
        {
            int len = ReadInt16();
            String s = System.Text.Encoding.UTF8.GetString(ReadExactly(len), 0, len);
            return s;
        }
    }
}
