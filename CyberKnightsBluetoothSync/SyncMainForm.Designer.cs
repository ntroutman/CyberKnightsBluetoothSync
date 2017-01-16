namespace CyberKnightsBluetoothSync
{
    partial class SyncMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DevicesList = new System.Windows.Forms.ListBox();
            this.FindDevicesButton = new System.Windows.Forms.Button();
            this.ListenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DevicesList
            // 
            this.DevicesList.FormattingEnabled = true;
            this.DevicesList.Location = new System.Drawing.Point(12, 12);
            this.DevicesList.Name = "DevicesList";
            this.DevicesList.Size = new System.Drawing.Size(284, 108);
            this.DevicesList.TabIndex = 0;
            this.DevicesList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DevicesList_MouseDoubleClick);
            // 
            // FindDevicesButton
            // 
            this.FindDevicesButton.Location = new System.Drawing.Point(188, 129);
            this.FindDevicesButton.Name = "FindDevicesButton";
            this.FindDevicesButton.Size = new System.Drawing.Size(108, 23);
            this.FindDevicesButton.TabIndex = 1;
            this.FindDevicesButton.Text = "Find Devices";
            this.FindDevicesButton.UseVisualStyleBackColor = true;
            this.FindDevicesButton.Click += new System.EventHandler(this.FindDevicesButton_Click);
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(13, 127);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(75, 23);
            this.ListenButton.TabIndex = 2;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // SyncMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 164);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.FindDevicesButton);
            this.Controls.Add(this.DevicesList);
            this.Name = "SyncMainForm";
            this.Text = "CyberKnights Bluetooth Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox DevicesList;
        private System.Windows.Forms.Button FindDevicesButton;
        private System.Windows.Forms.Button ListenButton;
    }
}

