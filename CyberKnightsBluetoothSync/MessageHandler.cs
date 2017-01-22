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
    //
    // Message Types:
    // 1 - File
    // 2 - Directory
    // 3 - Stop
    // 4 - Keep Alive
    // 5 - File Ack
    class MessageHandler
    {
        internal DataStream Stream;
        internal string RootPath;

        public MessageHandler(String root, DataStream stream)
        {
            this.RootPath = root;
            this.Stream = stream;
        }

        public IMessage Read()
        {
            Console.WriteLine("Message Handler Reading Message");
            try
            {
                int type = Stream.ReadInt1();
                switch (type)
                {
                    case FileMessage.TYPE:
                        return new FileMessage(this).Read();
                    case DirectoryMessage.TYPE:
                        return new DirectoryMessage(this).Read();
                    case StopMessage.TYPE:
                        return new StopMessage(this).Read();
                    default:
                        Console.WriteLine("Unknown Message Type: " + type);
                        return null;
                }
            } catch (EndOfStreamException e)
            {
                // no message left read
                return null;
            }
        }
    }

    abstract class IMessage
    {
        protected MessageHandler Handler;

        protected IMessage(MessageHandler handler)
        {
            this.Handler = handler;
        }
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
        private int compression;
        private string containerName;
        private long crc;
        private string filename;
        private int length;
        

        public FileMessage(MessageHandler handler) : base(handler)
        {
           
        }

        internal FileMessage Read()
        {
            Console.WriteLine("Reading File");
            DataStream buffer = Handler.Stream;
            this.filename = buffer.ReadUTF8();
            Console.WriteLine("Filename: {0}", filename);
            this.containerName = buffer.ReadUTF8();
            Console.WriteLine("ContainerName: {0}", containerName);
            // the client will use "/" to indicate writing files into
            // the root of the remote container, but Path.Combine
            // doesn't like it and instead of combining just replaces
            // the left-hand side with nothing, meaning files get writen
            // to C:\, so we will remove the leading slash
            if (this.containerName.StartsWith("/"))
            {
                containerName = this.containerName.Substring(1);
            }

            this.compression = buffer.ReadInt1();
            Console.WriteLine("Compression: {0}", compression);
            this.length = buffer.ReadInt32();
            Console.WriteLine("File Length: {0}", length);

            String containerPath = Path.Combine(Handler.RootPath, containerName);
            Directory.CreateDirectory(containerPath);

            String destinationFile = Path.GetFullPath(Path.Combine(Handler.RootPath, containerPath, filename));
            Console.WriteLine("Writing to Local File: {0}", destinationFile);
            FileStream fout = new FileStream(destinationFile, FileMode.OpenOrCreate);            
            buffer.ReadBytes(fout, this.length);
            fout.Close();
            this.crc = buffer.ReadInt64();
            Console.WriteLine("CRC: {0}", crc);
            Console.WriteLine("Sending File Ack");
            new FileAckMessage(Handler).Send();
            return this;
        }

        public override String ToString() {
            return "Type: FILE\n" +
            "  Filename: " + filename + "\n" +
            "  Container: " + containerName + "\n" +
            "  Compression: " + compression + "\n" +
            "  File Length: " + length + "\n" + 
            "  CRC: " + crc;
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

        private string containerName;
        private int childCount;
        private List<IMessage> Children { get; }

        public DirectoryMessage(MessageHandler handler) : base(handler)
        {            
            this.Children = new List<IMessage>();
        }

        internal DirectoryMessage Read()
        {
            Console.WriteLine("Reading Directory");
            DataStream buffer = Handler.Stream;
            this.containerName = buffer.ReadUTF8();
            Console.WriteLine("Container Name: {0}", containerName);
            this.childCount = buffer.ReadInt16();
            Console.WriteLine("Child Count [{0}]: {1}", containerName, childCount);
            for (int i = 0; i < childCount; i++)
            {
                Console.WriteLine("Reading Child [{0}]: {1}/{2}", containerName, (i+1), childCount);
                this.Children.Add(Handler.Read());
            }
            return this;
        }

        public override String ToString()
        {
            return "Type: DIRECTORY\n" +
            "  Container: " + containerName + "\n" +
            "  ChildCount: " + childCount;            
        }
    }

    // Stop Message Type
    // 1 byte - Message Type (STOP = 0x03)
    class StopMessage : IMessage
    {
        internal const int TYPE = 0x3;
       
        public StopMessage(MessageHandler handler) : base(handler)
        {            
            
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
    
    class KeepAliveMessage : IMessage
    {
        internal const int TYPE = 0x4;

        public KeepAliveMessage(MessageHandler handler) : base(handler)
        {

        }

        internal KeepAliveMessage Send()
        {
            Handler.Stream.WriteInt1(TYPE);
            Handler.Stream.Flush();
            return this;
        }
    }

    class FileAckMessage : IMessage
    {
        internal const int TYPE = 0x5;

        public FileAckMessage(MessageHandler handler) : base(handler)
        {

        }

        internal FileAckMessage Send()
        {
            Handler.Stream.WriteInt1(TYPE);
            Handler.Stream.Flush();
            return this;
        }
    }


}
