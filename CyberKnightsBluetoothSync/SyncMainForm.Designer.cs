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
            this.SuspendLayout();
            // 
            // ActivityList
            // 
            this.ActivityList.FormattingEnabled = true;
            this.ActivityList.Location = new System.Drawing.Point(12, 12);
            this.ActivityList.Name = "ActivityList";
            this.ActivityList.Size = new System.Drawing.Size(284, 134);
            this.ActivityList.TabIndex = 0;

            // 
            // SyncMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 164);
            this.Controls.Add(this.ActivityList);
            this.Name = "SyncMainForm";
            this.Text = "CyberKnights Bluetooth Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ActivityList;
    }
}

