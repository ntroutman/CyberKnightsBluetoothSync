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
            this.ActivityList = new System.Windows.Forms.ListBox();
            this.Button_Start = new System.Windows.Forms.Button();
            this.TextBox_RootPath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_Browse = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ActivityList
            // 
            this.ActivityList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActivityList.FormattingEnabled = true;
            this.ActivityList.Location = new System.Drawing.Point(3, 3);
            this.ActivityList.Margin = new System.Windows.Forms.Padding(10);
            this.ActivityList.Name = "ActivityList";
            this.ActivityList.Size = new System.Drawing.Size(499, 255);
            this.ActivityList.TabIndex = 0;
            // 
            // Button_Start
            // 
            this.Button_Start.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Start.Location = new System.Drawing.Point(413, 5);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(81, 23);
            this.Button_Start.TabIndex = 1;
            this.Button_Start.Text = "Start";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // TextBox_RootPath
            // 
            this.TextBox_RootPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_RootPath.Location = new System.Drawing.Point(80, 5);
            this.TextBox_RootPath.Name = "TextBox_RootPath";
            this.TextBox_RootPath.Size = new System.Drawing.Size(333, 20);
            this.TextBox_RootPath.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TextBox_RootPath);
            this.panel1.Controls.Add(this.Button_Browse);
            this.panel1.Controls.Add(this.Button_Start);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 258);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(499, 33);
            this.panel1.TabIndex = 4;
            // 
            // Button_Browse
            // 
            this.Button_Browse.Dock = System.Windows.Forms.DockStyle.Left;
            this.Button_Browse.Location = new System.Drawing.Point(5, 5);
            this.Button_Browse.Name = "Button_Browse";
            this.Button_Browse.Size = new System.Drawing.Size(75, 23);
            this.Button_Browse.TabIndex = 3;
            this.Button_Browse.Text = "Browse";
            this.Button_Browse.UseVisualStyleBackColor = true;
            this.Button_Browse.Click += new System.EventHandler(this.Button_Browse_Click);
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SyncMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 294);
            this.Controls.Add(this.ActivityList);
            this.Controls.Add(this.panel1);
            this.Name = "SyncMainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "CyberKnights Bluetooth Sync";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ActivityList;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.TextBox TextBox_RootPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button Button_Browse;
    }
}

