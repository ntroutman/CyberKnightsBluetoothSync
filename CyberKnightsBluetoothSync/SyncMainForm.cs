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
using System.IO;

namespace CyberKnightsBluetoothSync
{
    public partial class SyncMainForm : Form
    {
        delegate void ActivityListAddItemCallback(string item);

        public SyncMainForm()
        {
            InitializeComponent();
            Console.WriteLine(BluetoothRadio.PrimaryRadio.SoftwareManufacturer);
            TextBox_RootPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
      
        private void Button_Start_Click(object sender, EventArgs e)
        {            
            new BluetoothFileServer(StatusUpdateHandler, TextBox_RootPath.Text).Listen();
        }

        private void StatusUpdateHandler(String update)
        {
            if (this.ActivityList.InvokeRequired)
            {
                ActivityListAddItemCallback d = new ActivityListAddItemCallback(StatusUpdateHandler);
                this.Invoke(d, new object[] { update });
            }
            else
            {
                this.ActivityList.Items.Add(update);
            }
        }

        private void Button_Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !String.IsNullOrWhiteSpace(FolderBrowserDialog.SelectedPath))
            {
                TextBox_RootPath.Text = FolderBrowserDialog.SelectedPath;                
            }
        }
    }
}
