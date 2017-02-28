using Android;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using ADB.NET_Console;

namespace ADBNET_Console
{
    class Program
    {
        static Adb ADB = Adb.Instance;

        static void Main(string[] args)
        {
            Console.WriteLine("{0}", Marshal.SizeOf(typeof(Point)));
            return;

            ADB.Connect(IPAddress.Parse("10.1.112.48"));
            PrintDevices();

            var device = ADB.UpdateDevices()[0];

            // wake the device's screen
            device.ExecuteShellCommand(true, "input keyevent KEYCODE_POWER");

            // get a screenshot
            var x = device.ExecuteShellCommand(false, "screencap", "-p");
            using (BinaryWriter writer = new BinaryWriter(File.Create("test.png")))
                writer.Write(x);
        }

        public static void PrintDevices()
        {
            Console.WriteLine("Devices");

            if (ADB.Devices.Count == 0)
                Console.WriteLine("No devices found");
            else
                foreach (var x in ADB.Devices)
                    Console.WriteLine("{0} {1} {2}", x.SerialNumber, x.ConnectionType, x.ConnectionState);
        }
    }
}