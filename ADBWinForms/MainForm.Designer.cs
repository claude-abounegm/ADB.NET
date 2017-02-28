namespace ADBWinForms
{
    partial class MainForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            this.devicesList = new System.Windows.Forms.ListBox();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.wifiBox = new System.Windows.Forms.GroupBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.port = new System.Windows.Forms.TextBox();
            this.ip4 = new System.Windows.Forms.TextBox();
            this.ip3 = new System.Windows.Forms.TextBox();
            this.ip2 = new System.Windows.Forms.TextBox();
            this.ip1 = new System.Windows.Forms.TextBox();
            this.detailsBox = new System.Windows.Forms.GroupBox();
            this.commandsBox = new System.Windows.Forms.GroupBox();
            this.lockBtn = new System.Windows.Forms.Button();
            this.screenshotBtn = new System.Windows.Forms.Button();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.snLabel = new System.Windows.Forms.Label();
            this.modeNumber = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            this.wifiBox.SuspendLayout();
            this.detailsBox.SuspendLayout();
            this.commandsBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(105, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(130, 17);
            label1.TabIndex = 2;
            label1.Text = "Connected Devices";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(149, 20);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(12, 17);
            label5.TabIndex = 20;
            label5.Text = ":";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(113, 20);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(12, 17);
            label4.TabIndex = 18;
            label4.Text = ".";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(78, 20);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(12, 17);
            label3.TabIndex = 16;
            label3.Text = ".";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(43, 20);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(12, 17);
            label2.TabIndex = 14;
            label2.Text = ".";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(49, 77);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(106, 17);
            label8.TabIndex = 3;
            label8.Text = "Serial Number: ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(63, 54);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(90, 17);
            label7.TabIndex = 2;
            label7.Text = "Model Type: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(24, 30);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(131, 17);
            label6.TabIndex = 0;
            label6.Text = "Connection Status: ";
            // 
            // devicesList
            // 
            this.devicesList.FormattingEnabled = true;
            this.devicesList.ItemHeight = 16;
            this.devicesList.Location = new System.Drawing.Point(45, 32);
            this.devicesList.Name = "devicesList";
            this.devicesList.Size = new System.Drawing.Size(246, 244);
            this.devicesList.TabIndex = 0;
            this.devicesList.SelectedIndexChanged += new System.EventHandler(this.devicesList_SelectedIndexChanged);
            // 
            // refreshBtn
            // 
            this.refreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshBtn.Location = new System.Drawing.Point(126, 279);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(75, 28);
            this.refreshBtn.TabIndex = 1;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // wifiBox
            // 
            this.wifiBox.BackColor = System.Drawing.Color.White;
            this.wifiBox.Controls.Add(this.connectBtn);
            this.wifiBox.Controls.Add(this.port);
            this.wifiBox.Controls.Add(this.ip4);
            this.wifiBox.Controls.Add(label5);
            this.wifiBox.Controls.Add(this.ip3);
            this.wifiBox.Controls.Add(label4);
            this.wifiBox.Controls.Add(this.ip2);
            this.wifiBox.Controls.Add(label3);
            this.wifiBox.Controls.Add(this.ip1);
            this.wifiBox.Controls.Add(label2);
            this.wifiBox.Location = new System.Drawing.Point(19, 318);
            this.wifiBox.Name = "wifiBox";
            this.wifiBox.Size = new System.Drawing.Size(309, 57);
            this.wifiBox.TabIndex = 3;
            this.wifiBox.TabStop = false;
            this.wifiBox.Text = "ADB WiFi";
            // 
            // connectBtn
            // 
            this.connectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectBtn.Location = new System.Drawing.Point(217, 14);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(80, 32);
            this.connectBtn.TabIndex = 4;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // port
            // 
            this.port.BackColor = System.Drawing.Color.White;
            this.port.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.port.Location = new System.Drawing.Point(158, 21);
            this.port.MaxLength = 4;
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(37, 15);
            this.port.TabIndex = 21;
            this.port.Text = "5555";
            this.port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip4
            // 
            this.ip4.BackColor = System.Drawing.Color.White;
            this.ip4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip4.Location = new System.Drawing.Point(121, 21);
            this.ip4.MaxLength = 3;
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(29, 15);
            this.ip4.TabIndex = 19;
            this.ip4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip3
            // 
            this.ip3.BackColor = System.Drawing.Color.White;
            this.ip3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip3.Location = new System.Drawing.Point(86, 21);
            this.ip3.MaxLength = 3;
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(29, 15);
            this.ip3.TabIndex = 17;
            this.ip3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip2
            // 
            this.ip2.BackColor = System.Drawing.Color.White;
            this.ip2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip2.Location = new System.Drawing.Point(51, 21);
            this.ip2.MaxLength = 3;
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(29, 15);
            this.ip2.TabIndex = 15;
            this.ip2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip1
            // 
            this.ip1.BackColor = System.Drawing.Color.White;
            this.ip1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip1.Location = new System.Drawing.Point(16, 21);
            this.ip1.MaxLength = 3;
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(29, 15);
            this.ip1.TabIndex = 13;
            this.ip1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // detailsBox
            // 
            this.detailsBox.Controls.Add(this.commandsBox);
            this.detailsBox.Controls.Add(this.disconnectBtn);
            this.detailsBox.Controls.Add(this.snLabel);
            this.detailsBox.Controls.Add(this.modeNumber);
            this.detailsBox.Controls.Add(label8);
            this.detailsBox.Controls.Add(label7);
            this.detailsBox.Controls.Add(this.statusLabel);
            this.detailsBox.Controls.Add(label6);
            this.detailsBox.Location = new System.Drawing.Point(358, 17);
            this.detailsBox.Name = "detailsBox";
            this.detailsBox.Size = new System.Drawing.Size(441, 350);
            this.detailsBox.TabIndex = 4;
            this.detailsBox.TabStop = false;
            this.detailsBox.Text = "Details";
            // 
            // commandsBox
            // 
            this.commandsBox.Controls.Add(this.lockBtn);
            this.commandsBox.Controls.Add(this.screenshotBtn);
            this.commandsBox.Enabled = false;
            this.commandsBox.Location = new System.Drawing.Point(122, 173);
            this.commandsBox.Name = "commandsBox";
            this.commandsBox.Size = new System.Drawing.Size(220, 100);
            this.commandsBox.TabIndex = 9;
            this.commandsBox.TabStop = false;
            this.commandsBox.Text = "Commands";
            // 
            // lockBtn
            // 
            this.lockBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lockBtn.Location = new System.Drawing.Point(19, 27);
            this.lockBtn.Name = "lockBtn";
            this.lockBtn.Size = new System.Drawing.Size(181, 26);
            this.lockBtn.TabIndex = 7;
            this.lockBtn.Text = "Lock/Unlock Device";
            this.lockBtn.UseVisualStyleBackColor = true;
            this.lockBtn.Click += new System.EventHandler(this.lockBtn_Click);
            // 
            // screenshotBtn
            // 
            this.screenshotBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.screenshotBtn.Location = new System.Drawing.Point(19, 59);
            this.screenshotBtn.Name = "screenshotBtn";
            this.screenshotBtn.Size = new System.Drawing.Size(181, 27);
            this.screenshotBtn.TabIndex = 8;
            this.screenshotBtn.Text = "Take a screenshot";
            this.screenshotBtn.UseVisualStyleBackColor = true;
            this.screenshotBtn.Click += new System.EventHandler(this.screenshotBtn_Click);
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.disconnectBtn.Location = new System.Drawing.Point(338, 319);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(98, 27);
            this.disconnectBtn.TabIndex = 6;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Visible = false;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // snLabel
            // 
            this.snLabel.AutoSize = true;
            this.snLabel.Location = new System.Drawing.Point(154, 77);
            this.snLabel.Name = "snLabel";
            this.snLabel.Size = new System.Drawing.Size(31, 17);
            this.snLabel.TabIndex = 5;
            this.snLabel.Text = "N/A";
            // 
            // modeNumber
            // 
            this.modeNumber.AutoSize = true;
            this.modeNumber.Location = new System.Drawing.Point(154, 54);
            this.modeNumber.Name = "modeNumber";
            this.modeNumber.Size = new System.Drawing.Size(31, 17);
            this.modeNumber.TabIndex = 4;
            this.modeNumber.Text = "N/A";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(154, 30);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(31, 17);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "N/A";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 388);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(809, 25);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(59, 20);
            this.toolStripStatusLabel.Text = "Ready...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 413);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.detailsBox);
            this.Controls.Add(this.wifiBox);
            this.Controls.Add(label1);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.devicesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Android.NET Example";
            this.wifiBox.ResumeLayout(false);
            this.wifiBox.PerformLayout();
            this.detailsBox.ResumeLayout(false);
            this.detailsBox.PerformLayout();
            this.commandsBox.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox devicesList;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.GroupBox wifiBox;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox ip4;
        private System.Windows.Forms.TextBox ip3;
        private System.Windows.Forms.TextBox ip2;
        private System.Windows.Forms.TextBox ip1;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.GroupBox detailsBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label snLabel;
        private System.Windows.Forms.Label modeNumber;
        private System.Windows.Forms.Button disconnectBtn;
        private System.Windows.Forms.GroupBox commandsBox;
        private System.Windows.Forms.Button lockBtn;
        private System.Windows.Forms.Button screenshotBtn;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}

