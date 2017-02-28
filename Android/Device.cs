/*
 * Device.cs
 * Written by Claude Abounegm
 */

using Android.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Android
{
    public class Device
    {
        private ConnectionState _state;
        private bool _hasRoot = false;

        public ConnectionType ConnectionType { get; private set; }
        public ConnectionState ConnectionState
        {
            get { return _state; }
            private set
            {
                if (_state != value)
                    Adb.Instance.OnDeviceConnectionChanged(this, _state, _state = value);
            }
        }
        public string SerialNumber { get; private set; }
        public string ProductType { get; private set; }
        public string ModelType { get; private set; }
        public string DeviceType { get; private set; }

        /// <summary>
        /// Gets whether the device has root permissions.
        /// </summary>
        public bool HasRoot
        {
            get
            {
                if (!_hasRoot)
                {
                    try
                    {
                        string result = this.ExecuteShellCommand(false, "su", "-v").ToString(AdbSocket._encoding);
                        _hasRoot = !(result.Contains("not found") || result.Contains("permission denied"));
                    }
                    catch { } // _hasRoot is already false.
                }

                return _hasRoot;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="m"></param>
        internal Device(string sn, Match m)
        {
            SerialNumber = sn;

            if (sn.StartsWith("emulator"))
                ConnectionType = Android.ConnectionType.Emulator;
            else if (Regex.IsMatch(sn, @"^(?:\d{1,3}\.){3}\d{1,3}:\d+"))
                ConnectionType = Android.ConnectionType.LAN;
            // else ConnectionType is already USB.

            _state = ConnectionState.Disconnected;
            if (m != null)
                UpdateDeviceInfo(m);
        }

        internal void UpdateDeviceInfo(Match m)
        {
            SetConnectionState(m.Result("$2"));
            ProductType = m.Result("$3");
            ModelType = m.Result("$4");
            DeviceType = m.Result("$5");
        }
        internal void SetConnectionState(string cs)
        {
            ConnectionState s;
            if (cs == "device")
                s = ConnectionState.Online;
            else if (!Enum.TryParse<ConnectionState>(cs, true, out s))
                s = ConnectionState.Unknown;

            ConnectionState = s;
        }
        internal void SetConnectionState(ConnectionState cs)
        {
            ConnectionState = cs;
        }

        public void Reboot(string rebootOption)
        {
            rebootOption = rebootOption.ToLower();
            using (var s = new AdbSocket(true, TransportType.any, SerialNumber))
            {
                if (rebootOption == "normal")
                    rebootOption = "";

                s.SendService(string.Format("reboot:{0}", rebootOption));
                s.ReceiveData(true, false);
            }
        }
        public void Reboot(RebootOptions option)
        {
            Reboot(option.ToString());
        }
        public void Reboot()
        {
            Reboot(RebootOptions.Normal);
        }

        public void test(bool rootShell, string command, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("command is null or empty.", "command");

            if (args.Length > 0)
                command = string.Format("{0} {1}", command, string.Join(" ", args));

            if (rootShell)
            {
                if (!this.HasRoot)
                    throw new Exception("Device does not have root permissions.");

                command = string.Format("su -c \"{0}\"", command);
            }

            var x = new byte[1];
            using (var socket = AdbSocket.ConnectWithService(this, true, string.Format("shell:{0}", command)))
            {
                socket.ReadStatus().ThrowOnError();
                socket.ReceiveBytes(ref x, x.Length);
            }
        }

        public byte[] ExecuteShellCommand(bool rootShell, string command, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("command is null or empty.", "command");

            if (args.Length > 0)
                command = string.Format("{0} {1}", command, string.Join(" ", args));

            if (rootShell)
            {
                if (!this.HasRoot)
                    throw new Exception("Device does not have root permissions.");

                command = string.Format("su -c \"{0}\"", command);
            }

            using (var socket = AdbSocket.ConnectWithService(this, true, string.Format("shell:{0}", command)))
            {
                socket.ReadStatus().ThrowOnError();

                int readBytes = 0;
                List<byte> bytes = new List<byte>(16384);
                byte[] buffer = new byte[16384];

                do
                {
                    readBytes = socket.ReceiveBytes(ref buffer, buffer.Length);
                    bytes.AddRange(buffer.Take(readBytes));
                } while (readBytes != 0);

                int newLength = 0;
                for (int i = 0; i < bytes.Count; ++i)
                    bytes[newLength++] = ((i + 1) < bytes.Count && bytes[i] == 0x0D && bytes[i + 1] == 0x0A) ? bytes[++i] : bytes[i];
                return bytes.Take(newLength).ToArray();
            }
        }

        /*
        public byte[] ExecuteShellCommand(bool rootShell, string command, params string[] args)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("command is null or empty.", "command");

            if (args.Length > 0)
                command = string.Format("{0} {1}", command, string.Join(" ", args));

            if (rootShell)
            {
                if (!this.HasRoot)
                    throw new Exception("Device does not have root permissions.");

                command = string.Format("su -c \"{0}\"", command);
            }

            using (var socket = AdbSocket.ConnectWithService(this, true, string.Format("shell:{0}", command)))
            {
                socket.ReadStatus().ThrowOnError();

                using (MemoryStream stream = new MemoryStream())
                {
                    int readBytes = 0;
                    byte[] buffer = new byte[16384];

                    do
                    {
                        readBytes = socket.ReceiveBytes(ref buffer, buffer.Length);
                        stream.Write(buffer, 0, readBytes);
                    } while (readBytes != 0);

                    var newArray = stream.ToArray();
                    if (newArray.Length == 0)
                        return newArray;

                    int newLength = 0;
                    for (int i = 0; i < newArray.Length; ++i)
                        newArray[newLength++] = ((i + 1) < newArray.Length && newArray[i] == 0x0D && newArray[i + 1] == 0x0A) ? newArray[++i] : newArray[i];
                    return (newArray.Length == newLength) ? newArray : newArray.Take(newLength).ToArray();
                }
            }
        }*/

        /// <summary>
        /// Disconnects the device from ADB if the device is connected on the LAN.
        /// </summary>
        /// <returns>True on success; otherwise, false.</returns>
        public bool Disconnect()
        {
            if (ConnectionType == Android.ConnectionType.LAN)
            {
                using (var s = AdbSocket.ConnectWithService(this, false, string.Format("host:disconnect:{0}", this.SerialNumber)))
                {
                    if (s != null)
                    {
                        string str = s.ReceiveData(true, true)
                                      .ToString(AdbSocket._encoding);
                        Adb.Instance.UpdateDevices();

                        return !str.Contains("unable");
                    }
                }
            }

            return false;
        }
    }
}