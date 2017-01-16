using System;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Net.Sockets;

public class BluetoothFileServer
{
    BluetoothClient client;
    BluetoothListener listener;

    public BluetoothFileServer()
	{
        this.client = new BluetoothClient();
    }

    public void Listen()
    {   
        // begin listening for devices
        // all callers will have to be paired ahead of time
        listener = new BluetoothListener(new Guid("2d31ac7d-0d4a-48dd-8136-2f6a9b71a3f4"));
        listener.Start();

        listener.BeginAcceptBluetoothClient(this.BluetoothListenerAcceptClientCallback, listener);
    }

    void BluetoothListenerAcceptClientCallback(IAsyncResult result)
    {
        BluetoothClient client = GetClient(result);

        Console.WriteLine("Connection From: " + client.RemoteMachineName);

        // Protocol
        // 1 byte - Message Type
        //
        // File Message Type
        // 2 byte - Filename Length
        // n byte - Filename
        // 1 byte - Compression: 0 = None
        // 4 byte - File Length
        // n byte - File Contents
        // 2 byte - CRC

        NetworkStream stream = client.GetStream();
        byte[] bytes = new byte[1024];
        int read = stream.Read(bytes, 0, 1024);
        Buffer buffer = new Buffer(bytes);
        int type = buffer.ReadInt1();

        String fname = buffer.ReadUTF8();
        int compression = buffer.ReadInt1();
       
        int flen = buffer.ReadInt32();

        String f = BitConverter.ToString(buffer.ReadBytes(flen));
        int crc = buffer.ReadInt32();
        

        Console.WriteLine("Type: " + type);
        //Console.WriteLine("Filename Length: " + fnamelen);
        Console.WriteLine("Filename: " + fname);
        Console.WriteLine("Compression: " + compression);
        Console.WriteLine("File Length: " + flen);
        Console.WriteLine("File Contents: " + f);
        Console.WriteLine("CRC: " + crc);

        client.Close();
    }

    private class Buffer
    {
        private byte[] bytes;
        private int index;

        public Buffer(byte[] bytes)
        {
            this.bytes = bytes;
            this.index = 0;
        }
        public int ReadInt1()
        {
            return bytes[index += 1];
        }


        public int ReadInt16()
        {
            if (BitConverter.IsLittleEndian) {
                Array.Reverse(bytes, index, 2);
            }
            int v = BitConverter.ToInt16(bytes, index);
            index += 2;
            return v;
        }

        public int ReadInt32()
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, index, 4);
            }
            int v = BitConverter.ToInt32(bytes, index);
            index += 4;
            return v;
        }

        public byte[] ReadBytes(int len)
        {
            byte[] sub = new byte[len];
            Array.Copy(bytes,index, sub, 0, len);
            return sub;
        }

        public String ReadUTF8()
        {
            int len = ReadInt16();
            String s = System.Text.Encoding.UTF8.GetString(bytes, index, len);
            index += len;
            return s;
        }


    }

    private BluetoothClient GetClient(IAsyncResult result)
    {
        BluetoothListener listener = (BluetoothListener)result.AsyncState;

        // continue listening for other broadcasting devices
        listener.BeginAcceptBluetoothClient(this.BluetoothListenerAcceptClientCallback, listener);

        // create a connection to the device that's just been found
        BluetoothClient client = listener.EndAcceptBluetoothClient(result);
        return client;
    }

    public void Stop()
    {

    }
}
