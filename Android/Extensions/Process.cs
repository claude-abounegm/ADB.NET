/*
 * Extensions\Process.cs
 * Written by Claude Abounegm
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Android.Extensions
{
    public static class ProcessHelper
    {
        public static void StartGetOutput(this Process p, string filename, string arguments, int timeout, Action<string> outReceived, Action<string> errorReceived)
        {
            p.StartInfo = new ProcessStartInfo(filename, arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            p.OutputDataReceived += (object sender, DataReceivedEventArgs e) => { if (e.Data != null && outReceived != null) outReceived(e.Data); };
            p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => { if (e.Data != null && errorReceived != null) errorReceived(e.Data); };

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            if (!p.WaitForExit(timeout) && !p.HasExited)
                p.Kill();
        }

        public static void StartGetOutput(this Process p, string filename, string arguments, Action<string> outReceived, Action<string> errorReceived)
        {
            StartGetOutput(p, filename, arguments, int.MaxValue, outReceived, errorReceived);
        }

        public static string[] StartGetOutput(this Process p, string filename, string arguments, int timeout)
        {
            List<string> outp = new List<string>();
            var lock_o = new object();
            StartGetOutput(p, filename, arguments, timeout, 
                s => { lock (lock_o) outp.Add(s); }, 
                s => { lock (lock_o) outp.Add(s); }
            );
            return outp.ToArray();
        }

        public static string[] StartGetOutput(this Process p, string filename, string arguments)
        {
            return StartGetOutput(p, filename, arguments, int.MaxValue);
        }
    }
}
