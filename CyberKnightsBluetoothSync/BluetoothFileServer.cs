using System;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Net.Sockets;
using System.IO;
using CyberKnightsBluetoothSync;
using System.Threading;

namespace CyberKnightsBluetoothSync
{
    /**
     * An implementation of a bluetooth server that can recieve files and folders
     * from a client.
     */
    public class BluetoothFileServer
    {
        BluetoothClient client;
        BluetoothListener listener;
        private string root;
        private Action<String> updateHandler;

        public BluetoothFileServer(Action<String> handler, String root)
        {
            this.client = new BluetoothClient();
            this.updateHandler = handler;
            this.root = root;
        }

        public void Listen()
        {
            updateHandler("Listening For Clients. Root Directory: " + root);

            // begin listening for devices
            // all callers will have to be paired ahead of time
            listener = new BluetoothListener(new Guid("2d31ac7d-0d4a-48dd-8136-2f6a9b71a3f4"));
            listener.Start();

            listener.BeginAcceptBluetoothClient(this.BluetoothListenerAcceptClientCallback, listener);
        }

        void BluetoothListenerAcceptClientCallback(IAsyncResult result)
        {
            BluetoothClient client = GetClient(result);

            updateHandler("Connected To: " + client.RemoteMachineName);

            
            
            DataStream stream = new DataStream(client.GetStream());
            MessageHandler handler = new MessageHandler(root, stream);
            KeepAlive(client, handler);

            // read forever
            while (true)
            {
                Console.WriteLine("Accept Callback: Reading Message");
                IMessage msg = handler.Read();
                if (msg != null)
                {
                    if (msg is StopMessage)
                    {
                        Console.WriteLine("Recieved Stop Message exiting.");
                        return;
                    }
                    updateHandler(msg.ToString());
                }

                if (msg == null)
                {
                    break;
                }
            }

            client.Close();
        }

        // There is some weird bluetooth issues that require
        // the connection to be active in both directions.
        private void KeepAlive(BluetoothClient client, MessageHandler handler)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                KeepAliveMessage keepAliveMessage = new KeepAliveMessage(handler);
                while (client.Connected)
                {
                    try
                    {
                        keepAliveMessage.Send();
                        Thread.Sleep(500);
                    }
                    catch (IOException e)
                    {
                        return;
                    }
                }
            }).Start();
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
}