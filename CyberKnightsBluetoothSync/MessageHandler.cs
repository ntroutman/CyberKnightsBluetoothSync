using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberKnightsBluetoothSync
{
    // File Pusher Protocol
    // The file pusher protocol is a simple order dependent protocol for copying files
    // over bluetooth. Files and directories are relative to a virtual root on the 
    // recieving server. 
    //
    // 1 byte - Message Type
    // variable - Dependent on message type
    class MessageHandler
    {
        private string root;

        public MessageHandler(String root)
        {
            this.root = root;
        }

        public IMessage Read(DataStreamReader buffer)
        {
            int type = buffer.ReadInt1();
            switch (type)
            {
                case FileMessage.TYPE:
                    return new FileMessage(root, buffer).Read();
                case DirectoryMessage.TYPE:
                    return new DirectoryMessage(buffer).Read();
                case StopMessage.TYPE:
                    return new StopMessage(buffer).Read();
                default:
                    throw new Exception("Unknown message type: " + type);
            }
        }
    }

    interface IMessage
    {

    }

    // File Message Type
    // 1 byte - Message Type (FILE = 0x01)
    // 2 byte - Filename Length
    // n byte - Filename
    // 2 byte - Container Name (Target Directory) Length
    // n byte - Container Name (Target Directory)
    // 1 byte - Compression: 0 = None
    // 4 byte - File length
    // n byte - File contents
    // 8 byte - CRC
    class FileMessage : IMessage
    {
        internal const int TYPE = 0x1;
        private DataStreamReader buffer;
        private int compression;
        private string containerName;
        private int crc;
        private string filename;
        private int length;
        private string root;

        public FileMessage(String root, DataStreamReader buffer)
        {
            this.root = root;
            this.buffer = buffer;
        }

        internal FileMessage Read()
        {
            this.filename = buffer.ReadUTF8();
            this.containerName = buffer.ReadUTF8();
            this.compression = buffer.ReadInt1();

            this.length = buffer.ReadInt32();
            String containerPath = Path.Combine(root, containerName);
            Directory.CreateDirectory(containerPath);
            FileStream fout = new FileStream(Path.Combine(containerPath, filename), FileMode.OpenOrCreate);
            buffer.ReadBytes(fout, this.length);
            fout.Close();
            this.crc = buffer.ReadInt32();
            return this;
        }

        public override String ToString() {
            return "Type: FILE\n" +
            "Filename: " + filename + "\n" +
            "Container: " + containerName + "\n" +
            "Compression: " + compression + "\n" +
            "File Length: " + length + "\n" + 
            "CRC: " + crc;
        }
    }

    // Directory Message Type (UN-USED)
    // 1 byte - Message Type (DIRECTORY = 0x02)
    // 2 byte - Directory Name Length
    // n byte - Directory Name
    // 2 byte - Child Count
    class DirectoryMessage : IMessage
    {
        internal const int TYPE = 0x2;
        private DataStreamReader buffer;
        private string containerName;
        private int childCount;

        public DirectoryMessage(DataStreamReader buffer)
        {            
            this.buffer = buffer;
        }

        internal DirectoryMessage Read()
        {
            this.containerName = buffer.ReadUTF8();
            this.childCount = buffer.ReadInt16();
            return this;
        }

        public override String ToString()
        {
            return "Type: DIRECTORY\n" +
            "Container: " + containerName + "\n" +
            "ChildCount: " + childCount;            
        }
    }

    // Stop Message Type
    // 1 byte - Message Type (STOP = 0x03)
    class StopMessage : IMessage
    {
        internal const int TYPE = 0x3;
        private DataStreamReader buffer;

        public StopMessage(DataStreamReader buffer)
        {            
            this.buffer = buffer;
        }

        internal StopMessage Read()
        {
            return this;
        }

        public override String ToString()
        {
            return "Type: STOP";
        }
    }


}
