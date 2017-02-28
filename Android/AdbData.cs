/*
 * AdbData.cs
 * Written by Claude Abounegm
 */

using System;

namespace Android
{
    public class AdbData
    {
        public byte[] RawData { get; private set; }
        public bool Success { get; private set; }
        public Exception Exception { get; private set; }

        public AdbData(byte[] data, bool success, Exception ex)
        {
            RawData = data;
            Success = success;
            Exception = ex;
        }

        public void ThrowOnError()
        {
            if (Exception != null)
                throw Exception;
        }

        public override string ToString()
        {
            if (Exception != null)
                return Exception.Message;
            return "Operation was successful.";
        }
    }
}
