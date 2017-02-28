/*
 * Adb.cs
 * Written by Claude Abounegm
 */

using Android.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Android
{
    /// <summary>
    /// Base class for all ADB communications.
    /// </summary>
    public class Adb
    {
        public delegate void DeviceAddedEventHandler(Device device);
        public delegate void ConnectionChangedEventHandler(Device device, ConnectionState oldState, ConnectionState newState);

        public event DeviceAddedEventHandler DeviceAdded;
        public event ConnectionChangedEventHandler DeviceConnectionChanged;

        private void OnDeviceAdded(Device d)
        {
            if (DeviceAdded != null)
                DeviceAdded(d);
        }
        internal void OnDeviceConnectionChanged(Device d, ConnectionState oldState, ConnectionState newState)
        {
            if (DeviceConnectionChanged != null)
                DeviceConnectionChanged(d, oldState, newState);
        }

        /// <summary>
        /// Retrieves a Device object with the specified serial number.
        /// </summary>
        /// <param name="sn">The serial number of the device. It could be the in the form `xs23dad4f55432`, `XXX.XXX.XXX.XXX:XXXX`, and `emulator-XXXX`</param>
        /// <returns>The device if found. Otherwise, null.</returns>
        public Device this[string sn]
        {
            get
            {
                if (_devices.ContainsKey(sn)) 
                    return _devices[sn];
                return null;
            }
        }

        #region Internal Static
        internal static Encoding _encoding = Encoding.GetEncoding("ISO-8859-1"); // we need to use 8-bit chars, and Encoding.ASCII is 7-bit wide.

        // those fields are here for later compatibility with remote adb
        internal static int _serverPort = 5037;
        internal static IPAddress _serverAddress = IPAddress.Loopback;

        private static Adb _instance;
        public static Adb Instance
        {
            get { return _instance == null ? (_instance = new Adb()) : _instance; }
        }
        #endregion
        #region Private
        /// <summary>
        /// This is the Regex expression to parse the device info.
        /// $1: serial number, $2: connection status, $3: product $4: model, $5: device
        /// </summary>
        private const string REGEX_DEVICE_INFO = @"^([a-z0-9_-]+(?:[.0-9]+:\d{1,5})?)\s+(\w+)(?:\s+product:(\w+))?(?:\s+model:(\w+))?(?:\s+device:(\w+))?$";
        private readonly string[] REGISTRY_LOCATIONS = { 
                                                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Android SDK Tools", 
                                                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Android SDK Tools", 
                                                            @"HKEY_CURRENT_USER\SOFTWARE\Android SDK Tools",
                                                            @"HKEY_CURRENT_USER\SOFTWARE\Wow6432Node\Android SDK Tools"
                                                       };
        private const int SUPPORTED_SERVER_VERSION = 32;

        // devices collection
        private Dictionary<string, Device> _devices = new Dictionary<string, Device>();
        private ReadOnlyCollection<Device> _readOnlyDevices = new ReadOnlyCollection<Device>(new Device[0]);
        private string _adbPath = "adb.exe"; // the path of ADB
        #endregion
        #region Properties
        public IList<Device> Devices { get { return _readOnlyDevices; } }
        #endregion

        private Adb()
        {
            if (!File.Exists(_adbPath) && !FindADBPath())
                throw new Exception("adb.exe could not be found.");
        }

        /// <summary>
        /// Find the path of adb.exe in SDK tools, and set it to _adbPath.
        /// </summary>
        /// <returns>true if adb.exe was found; otherwise, false.</returns>
        private bool FindADBPath()
        {
            string path = null;
            for (int i = 0; i < REGISTRY_LOCATIONS.Length; ++i)
            {
                path = (string)Registry.GetValue(REGISTRY_LOCATIONS[i], "Path", null);

                // check if adb.exe is in the path found.
                if (path != null && File.Exists(path += @"\platform-tools\adb.exe"))
                {
                    _adbPath = path;
                    return true;
                }
            }

            // if we got here, then no path was found.
            return false;
        }

        /// <summary>
        /// Starts an ADB server on the local host.
        /// </summary>
        public void StartServer()
        {
            if (!IPAddress.IsLoopback(_serverAddress))
                throw new NotSupportedException("Cannot start a server on a remote machine.");

            try
            {
                string error = null;
                using (var p = new Process())
                    p.StartGetOutput(_adbPath, string.Format("{0} {1} start-server", "-P", _serverPort), null, e => error = string.Concat(error, '\n', e));

                // need to change exception type
                if (error != null)
                    throw new Exception(error);
            }
            catch (Exception ex) { throw new Exception("ADB failed to start.", ex); }
        }

        /// <summary>
        /// Kills the ADB server
        /// </summary>
        public void KillServer()
        {
            AdbSocket.ConnectWithService(null, false, "host:kill").Dispose();
        }

        /// <summary>
        /// Updates all connected devices.
        /// </summary>
        /// <returns>ADBManager.Devices</returns>
        public IList<Device> UpdateDevices()
        {
            using (var s = AdbSocket.ConnectWithService(null, true, "host:devices-l"))
                return UpdateDevicesInternal(s.ReceiveData(true, true).ToString(_encoding));
        }
        internal IList<Device> UpdateDevicesInternal(string rawData)
        {
            // Create a dictionary with the serial number as the key, and the match as the value.
            var deviceMatches = Regex.Matches(rawData, REGEX_DEVICE_INFO, RegexOptions.Multiline)
                .OfType<Match>()
                .ToDictionary(m => m.Result("$1"), m => m); // <string, Match>[]

            // Go through each device already in the dictionary, and update the device info of each device.
            foreach (var deviceInfo in _devices)
            {
                string sn = deviceInfo.Key;
                Device device = deviceInfo.Value;

                if (deviceMatches.ContainsKey(sn))
                {
                    device.UpdateDeviceInfo(deviceMatches[sn]);
                    deviceMatches.Remove(sn); // remove the match.
                }
                else
                    device.SetConnectionState(ConnectionState.Disconnected);
            }

            // if there are still entries in the dictionary, then we still need 
            // to add those devices.
            if (deviceMatches.Count > 0)
            {
                foreach (var device in deviceMatches)
                {
                    var d = new Device(device.Key, device.Value);
                    _devices.Add(device.Key, d);
                    OnDeviceAdded(d);
                }

                _readOnlyDevices = new ReadOnlyCollection<Device>(_devices.Values.ToArray());
            }

            return _readOnlyDevices;
        }

        /// <summary>
        /// Queries the ADB server version. Only the minor version is returned.
        /// </summary>
        /// <returns></returns>
        public int QueryADBVersion()
        {
            using (var s = new AdbSocket(true, TransportType.any, null))
            {
                s.SendService("host:version");
                return s.ReceiveData(true, true).FromHexToInt32();
            }
        }

        public void WaitForDevice(TransportType ttype, string serialNumber)
        {
            using (var s = new AdbSocket(true, ttype, serialNumber))
            {
                s.SendService(string.Format(":wait-for-{0}", ttype));
                s.ReadStatus().ThrowOnError(); // First OKAY for command received
                s.ReadStatus().ThrowOnError(); // Second OKAY for device connected

                this.UpdateDevices();
            }
        }
        public void WaitForDevice(TransportType ttype)
        {
            WaitForDevice(ttype, null);
        }
        public void WaitForDevice()
        {
            WaitForDevice(TransportType.any);
        }

        public bool Connect(IPAddress address, int port)
        {
            using (var s = AdbSocket.ConnectWithService(null, true, string.Format("host:connect:{0}:{1}", address.ToString(), port)))
                if (s.ReceiveData(true, true).ToString(_encoding).Contains("unable to connect"))
                    return false;

            Thread.Sleep(500);
            this.UpdateDevices();
            return true;
        }
        public bool Connect(IPAddress address)
        {
            return this.Connect(address, 5555);
        }

        public bool Disconnect()
        {
            using (var s = AdbSocket.ConnectWithService(null, false, "host:disconnect:"))
                s.ReceiveData(true, true).ToString(_encoding);

            Thread.Sleep(500);
            this.UpdateDevices();
            return true;
        }
    }
}
