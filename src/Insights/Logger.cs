using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Insights
{
    internal static class Logger
    {
        private static string LogTimestampFormat = "yyyy/MM/dd HH:mm:ss.ffffff";

        private static string LogFileDirectory = "Logs";
        private static string LogFileName = "Gameplay.log";
        private static string LogFilePath;

        static Logger()
        {
            var modFileCodeBase = Assembly
                .GetExecutingAssembly()
                .CodeBase;

            Debug.Log($"Insights: Mod File Code Base: {modFileCodeBase}");

            //var modFileDirectory = Path.GetDirectoryName(modFilePath);

            //Debug.Log($"Insights: Mod File Directory: {modFileDirectory}");

            //LogFilePath = Path.Combine(
            //    modFileDirectory,
            //    Path.Combine(
            //        LogFileDirectory,
            //        LogFileName));

            //Debug.Log($"Insights: Log File Path: {modFileDirectory}");
        }

        internal static void Log(string message)
        {
            var timestamp = DateTime.Now.ToString(LogTimestampFormat);

            using (var writer = File.AppendText(LogFilePath))
            {
                writer.WriteLine($"{timestamp} {message}");
            }
        }
    }
}
