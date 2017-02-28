/*
 * MainForm.cs
 * Written by Claude Abounegm
 */

using Android;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ADBWinForms
{
    public partial class MainForm : Form
    {
        Adb ADB = Adb.Instance;
        Screenshot screen = null;

        public MainForm()
        {
            InitializeComponent();

            // This application is a proof of concept, and is not thread safe. Thus should not be used
            // in any professional development environment.
            Form.CheckForIllegalCrossThreadCalls = false;

            ADB.DeviceConnectionChanged += ADB_DeviceConnectionChanged;
            ADB.DeviceAdded += ADB_DeviceAdded;
            ADB.UpdateDevices();
        }

        void ADB_DeviceConnectionChanged(Device device, Android.ConnectionState oldState, Android.ConnectionState newState)
        {
            SetStatus(device);
        }

        void ADB_DeviceAdded(Device device)
        {
            if (!devicesList.Items.Contains(device.SerialNumber))
                devicesList.Items.Add(device.SerialNumber);
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            devicesList.SelectedIndex = -1;

            foreach (var x in ADB.UpdateDevices())
                if (!devicesList.Items.Contains(x.SerialNumber))
                    devicesList.Items.Add(x.SerialNumber);

            if (devicesList.Items.Count > 0)
                devicesList.SelectedIndex = 0;
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    toolStripStatusLabel.Text = "Disconnecting...";
                    var device = ADB[devicesList.SelectedItem.ToString()];
                    if (device != null)
                        device.Disconnect();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ADB.NET Example", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { toolStripStatusLabel.Text = "Ready..."; }
            });
        }

        private void SetStatus(Device device)
        {
            if (device == null || devicesList.SelectedItem == null || devicesList.SelectedItem.ToString() != device.SerialNumber)
                return;

            statusLabel.Text = device.ConnectionState.ToString();
            modeNumber.Text = device.ModelType;
            snLabel.Text = device.SerialNumber;
            disconnectBtn.Visible = (device.ConnectionState != Android.ConnectionState.Disconnected && device.ConnectionType == ConnectionType.LAN);
            commandsBox.Enabled = (device.ConnectionState == Android.ConnectionState.Online);
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    toolStripStatusLabel.Text = "Connecting...";
                    if (!ADB.Connect(IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", ip1.Text, ip2.Text, ip3.Text, ip4.Text)), int.Parse(port.Text)))
                        MessageBox.Show("Connection to the device has failed.", "Android.NET Example", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ADB.NET Example", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { toolStripStatusLabel.Text = "Ready..."; }
            });
        }

        private void devicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devicesList.SelectedItem != null)
                SetStatus(ADB[devicesList.SelectedItem.ToString()]);
        }

        private void lockBtn_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                toolStripStatusLabel.Text = "Locking/Unlocking device...";

                var device = ADB[devicesList.SelectedItem.ToString()];
                device.ExecuteShellCommand(true, "input keyevent KEYCODE_POWER");

                toolStripStatusLabel.Text = "Ready...";
            });
        }

        private void screenshotBtn_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                toolStripStatusLabel.Text = "Taking a screenshot...";

                var device = ADB[devicesList.SelectedItem.ToString()];

                if (screen == null || screen.IsDisposed)
                    screen = new Screenshot();

                using (var mem = new MemoryStream(device.ExecuteShellCommand(false, "screencap", "-p")))
                    screen.SetImage(Image.FromStream(mem));

                if (!screen.Visible)
                    this.Invoke((Action)delegate { screen.Show(this); });

                toolStripStatusLabel.Text = "Ready...";
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                toolStripStatusLabel.Text = "Locking/Unlocking device...";

                var device = ADB[devicesList.SelectedItem.ToString()];
                device.test(true, "input keyevent KEYCODE_POWER");

                toolStripStatusLabel.Text = "Ready...";
            });
        }
    }
}