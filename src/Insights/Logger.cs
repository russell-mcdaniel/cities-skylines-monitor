using System;
using System.Diagnostics;

namespace Insights
{
    internal static class Logger
    {
        private static string LogTimestampFormat = "yyyy/MM/dd HH:mm:ss.ffffff";

        internal static void Log(string message)
        {
            var timestamp = DateTime.Now.ToString(LogTimestampFormat);

            Debug.WriteLine($"{timestamp} {message}");
        }
    }
}
