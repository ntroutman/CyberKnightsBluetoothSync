using System;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Net.Sockets;
using System.IO;
using CyberKnightsBluetoothSync;

public class BluetoothFileServer
{
    BluetoothClient client;
    BluetoothListener listener;
    private string root;

    public BluetoothFileServer(String root)
	{
        this.client = new BluetoothClient();
        this.root = root;
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

       
        Stream stream = new BufferedStream(client.GetStream());
        DataStreamReader buffer = new DataStreamReader(stream);
        MessageHandler handler = new MessageHandler(root);
        while (true)  {
            IMessage msg = handler.Read(buffer);
            Console.WriteLine(msg.ToString());
            if (msg == null)
            {
                break;
            }
        }
       
        client.Close();
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
