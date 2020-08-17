using System;
using System.IO;
using UnityEngine;

namespace Insights.Logging
{
    internal static class InsightsLoggerLogFileManager
    {
        public static object LogFileSyncRoot { get; } = new object();

        /// <summary>
        /// The time used for the current log file name. It is updated each minute to the whole minute.
        /// </summary>
        public static DateTime LogFileTime { get; set; }

        /// <summary>
        /// The StreamWriter used to write to the log file.
        /// </summary>
        public static StreamWriter LogFileWriter { get; private set; }

        /// <summary>
        /// Creates a log file based on the specified timestamp. If the log file already exists, it will be opened.
        /// </summary>
        /// <param name="timestampMinute"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The log writer lifecycle is managed at the class scope.")]
        public static void CreateLogFile(DateTime timestampMinute)
        {
            // Set the log file name.
            var logFileName = $"CitiesSkylinesInsights-{timestampMinute:yyyyMMdd}-{timestampMinute:HHmm}.log";

            // Set the log file path.
            var logFilePath = Path.Combine(
                Application.dataPath,
                Path.Combine(
                    "Mods/Insights",
                    logFileName));

            // Create directory. No action if the directory already exists.
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            // Dispose the existing writer. This forces a flush.
            LogFileWriter?.Dispose();

            // Create a new writer. Use append to ensure a log file is not overwritten
            // if a session is restarted within the same rollover period.
            LogFileWriter = File.AppendText(logFilePath);
        }
    }
}
