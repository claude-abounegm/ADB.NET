/*
 * AdbSocket.cs
 * Written by Claude Abounegm
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Android.Extensions;

namespace Android
{
    public class AdbSocket : IDisposable
    {
        public static AdbSocket ConnectWithService(Device device, bool startServer, string service)
        {
            var s = new AdbSocket(startServer, TransportType.any, device == null ? null : device.SerialNumber);
            if(!s.Connect())
            {
                if (startServer)
                    throw new Exception("Could not connect to the ADB server.");
                else return null;
            }

            s.SendService(service);
            return s;
        }

        private const int MAX_TRIALS = 1;
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static Encoding _encoding = Adb._encoding;
        private bool _startServer;
        private TransportType _ttype;
        private string _serialNumber;
        private bool _transportPerformed;

        private string AddHostPrefix(string commandStr)
        {
            if (_serialNumber != null)
                return string.Format("host-serial:{0}{1}", _serialNumber, commandStr);

            return string.Format("host{0}{1}",
                _ttype == TransportType.any ? "" : "-" + _ttype,
                commandStr);
        }

        public AdbSocket(bool autoStartServer, TransportType ttype, string serialNumber)
        {
            _startServer = autoStartServer;
            _ttype = ttype;
            _serialNumber = serialNumber;
            _transportPerformed = false;
        }

        public bool Connect()
        {
            if (_socket.Connected)
                return true;

            int trials = 0;
            do
            {
                try
                {
                    ++trials;
                    _socket.Connect(Adb._serverAddress, Adb._serverPort);
                }
                catch (SocketException)
                {
                    if (_startServer && trials == MAX_TRIALS)
                        Adb.Instance.StartServer();
                }
            } while (_startServer && !_socket.Connected && trials <= MAX_TRIALS);

            return _socket.Connected;
        }

        public void SendService(string service)
        {
            if (!this.Connect())
                throw new Exception("Could not connected to ADB server.");

            if (service.StartsWith(":"))
                service = AddHostPrefix(service);

            if (!_transportPerformed && !service.StartsWith("host"))
            {
                // "transport:{0}" is used for switching transport with a specified serial number
                // "transport-usb" is used for switching transport to the only USB transport
                // "transport-local" is used for switching transport to the only local transport
                // "transport-any" is used for switching transport to the only transport
                if (_serialNumber == null)
                    this.SendService(string.Format("host:transport-{0}", _ttype));
                else
                    this.SendService(string.Format("host:transport:{0}", _serialNumber));

                this.ReadStatus().ThrowOnError();
                _transportPerformed = true;
            }

            this.SendData(_encoding.GetBytes(string.Format("{0:x4}{1}", service.Length, service)));
        }
        public void SendData(byte[] buffer)
        {
            if (this.SendBytes(buffer, buffer.Length) != buffer.Length)
                throw new Exception("Write failure during connection");
        }
        public int SendBytes(byte[] buffer, int count)
        {
            int sentBytes = 0;
            int offset = 0;

            do
            {
                sentBytes = _socket.Send(buffer, offset, count, SocketFlags.None);
                offset += sentBytes;
                count -= sentBytes;
            }
            while (sentBytes > 0 && count > 0);

            return offset;
        }

        public AdbData ReadStatus()
        {
            byte[] buffer = new byte[4];

            Exception ex = null;
            if (this.ReceiveBytes(ref buffer, 4) != 4)
                ex = new Exception("Protocolt fault: couldn't retrieve status (OKAY or FAIL).");

            if (buffer[0] == 'F' && buffer[1] == 'A' && buffer[2] == 'I' && buffer[3] == 'L')
                ex = new Exception(string.Format("Protocol fault: {0}", _encoding.GetString(this.ReceiveData(false, true))));
            else if (buffer[0] != 'O' || buffer[1] != 'K' || buffer[2] != 'A' || buffer[3] != 'Y')
                ex = new Exception(string.Format("Protocal fault: status {0} was not recognized.", _encoding.GetString(buffer)));

            return new AdbData(buffer, ex == null, ex);
        }
        public byte[] ReceiveData(bool readStatus, bool readContent)
        {
            byte[] buffer = new byte[4];
            if (readStatus)
                this.ReadStatus().ThrowOnError();

            if (readContent)
            {
                if (_socket.Receive(buffer, 0, 4, SocketFlags.None) != 4)
                    throw new Exception();

                int len = buffer.FromHexToInt32();
                if(len > 0)
                {
                    buffer = new byte[len];
                    if (this.ReceiveBytes(ref buffer, len) != len)
                        throw new Exception();
                }

                return buffer;
            }
            return new byte[0];
        }
        public int ReceiveBytes(ref byte[] buffer, int count)
        {
            // initialize the byte buffer if not initialized or too short.
            if (buffer == null || buffer.Length < count)
                buffer = new byte[count];

            int readBytes = 0;
            int offset = 0;

            do
            {
                readBytes = _socket.Receive(buffer, offset, count, SocketFlags.None);
                offset += readBytes;
                count -= readBytes;
            }
            while (readBytes > 0 && count > 0);

            return offset;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_socket != null)
                    _socket.Dispose();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~AdbSocket()
        {
            Dispose(false);
        }
    }
}
