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

        private void ListenButton_Click(object sender, EventArgs e)
        {
            new BluetoothFileServer("./").Listen();
        }

    }
}
