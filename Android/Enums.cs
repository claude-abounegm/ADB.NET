/*
 * Enums.cs
 * Written by Claude Abounegm
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android
{
    /// <summary>
    /// Specifies the location to which the device should connect to.
    /// </summary>
    public enum TransportType
    {
        /// <summary>
        /// Directs the command to a USB device.
        /// </summary>
        usb,
        /// <summary>
        /// Directs the command to an emulator or any device connected on LAN.
        /// </summary>
        local,
        /// <summary>
        /// Represents any device connected (USB/Emulator/Local).
        /// </summary>
        any
    }

    public enum ConnectionType
    {
        USB,
        Emulator,
        LAN
    }

    /// <summary>
    /// Used for Device.Reboot().
    /// </summary>
    public enum RebootOptions
    {
        /// <summary>
        /// Reboots the device normally.
        /// </summary>
        Normal,
        /// <summary>
        /// Reboots the device in bootloader. Might not be supported on all devices.
        /// </summary>
        Bootloader,
        /// <summary>
        /// Reboots the device in download mode. Usually used on Samsung devices.
        /// </summary>
        Download,
        /// <summary>
        /// Reboots the device in recovery mode. Does not work on all devices.
        /// </summary>
        Recovery,
        /// <summary>
        /// Reboots the device in recovery mode and activates sideload. Does not work on all devices.
        /// </summary>
        Sideload
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ConnectionState
    {
        Any = -1,
        Offline,
        Bootloader,
        Online,
        Recovery,
        Sideload,
        Unauthorized,
        Disconnected,
        Unknown
    }
}
