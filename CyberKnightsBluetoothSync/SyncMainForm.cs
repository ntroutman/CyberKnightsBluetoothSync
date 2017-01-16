using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace CyberKnightsBluetoothSync
{
    public partial class SyncMainForm : Form
    {
        public SyncMainForm()
        {
            InitializeComponent();
            Console.WriteLine(BluetoothRadio.PrimaryRadio.SoftwareManufacturer);
        }

        private void FindDevicesButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Listing Devices...");
            DevicesList.Items.Clear();            

            var cli = new BluetoothClient();
            Console.WriteLine("Aquired Client...");
            BluetoothDeviceInfo[] peers = cli.DiscoverDevices();
            Console.WriteLine("Found Devices: " + peers.Length);
            foreach(BluetoothDeviceInfo device in peers) {
                DevicesList.Items.Add(device);
            }
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
             new BluetoothFileServer().Listen();
            //var lsnr = new BluetoothListener(new Guid("2d31ac7d-0d4a-48dd-8136-2f6a9b71a3f4"));
            //lsnr.Start();
            //BluetoothClient conn = lsnr.AcceptBluetoothClient();
            //Console.WriteLine(conn.RemoteMachineName);
        }

        private void foo() { 
            DevicesList.Items.Clear();

            //var lsnr = new BluetoothListener(BluetoothService.SerialPort);
            //lsnr.Start();
            //BluetoothClient conn = lsnr.AcceptBluetoothClient();
            //Console.WriteLine(conn.RemoteMachineName);

            Console.WriteLine("Listening for BlueTooth Serial");
            var lsnr = new ObexListener(ObexTransport.Bluetooth);
            lsnr.Start();
            // For each connection
            ObexListenerContext ctx = lsnr.GetContext();
            ObexListenerRequest req = ctx.Request;
            DevicesList.Items.Add(req.RawUrl + ": " + req.Headers);

            
            lsnr.Stop();
        }

        private void DevicesList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BluetoothDeviceInfo device = (BluetoothDeviceInfo)DevicesList.SelectedItem;
            BluetoothEndPoint endpoint = new BluetoothEndPoint(device.DeviceAddress, BluetoothService.ObexFileTransfer);
            BluetoothClient client = new BluetoothClient();
            client.Connect(endpoint);
           
        }
    }
}
