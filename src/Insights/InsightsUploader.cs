using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Insights.Logging;
using Insights.Utilities;

namespace Insights
{
    public class InsightsUploader : IDisposable
    {
        private const string FileNamePattern = "\\d{8}-\\d{6}.log";
        private const long FileSizeMaximum = 65536;

        private const string UploaderErrorMessage = "The uploader encountered an error.";

        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(InsightsUploader));

#if DEBUG
        //private readonly Uri _uploadUri = new Uri("https://localhost:44300/upload");
        private readonly Uri _uploadUri = new Uri("http://localhost:55300/upload");
#else
        private readonly Uri _uploadUri = new Uri("https://www.siliconesis.com/cities-skylines/insights/api/v1.0/upload");
#endif

        private readonly string _archiveDirectory = $"{LogFileManager.GetLogFileDirectory(LogFileType.Game)}Archive";
        private Regex _fileNameExpression = new Regex(FileNamePattern);

        private ManualResetEvent _cancellationEvent;

        private Thread _processor;
        private readonly TimeSpan _processorSleepTimeout = TimeSpan.FromSeconds(15);
        private readonly TimeSpan _processorStopTimeout = TimeSpan.FromSeconds(3);

        public InsightsUploader()
        {
            // Create the archive directory.
            Directory.CreateDirectory(_archiveDirectory);

            // Configure server certificate validation.
            ServicePointManager.ServerCertificateValidationCallback = ProcessLogFileUploadServerCertificate;
        }

        public void Start()
        {
            Logger.LogDebug($"{nameof(Start)} > Starting...");

            if (_disposed)
                throw new ObjectDisposedException(nameof(InsightsUploader));

            _cancellationEvent = new ManualResetEvent(false);

            _processor = new Thread(new ThreadStart(ProcessLogFiles));
            _processor.Name = $"{nameof(InsightsUploader)}LogFileProcessor";
            _processor.Start();

            Logger.LogDebug($"{nameof(Start)} > Started.");
        }

        public void Stop()
        {
            Logger.LogDebug($"{nameof(Stop)} > Stopping...");

            if (_disposed)
                throw new ObjectDisposedException(nameof(InsightsUploader));

            _cancellationEvent.Set();

            if (!_processor.Join(_processorStopTimeout))
            {
                // TODO: Properly handle thread abort in processing thread.
                _processor.Abort();
            }

            _cancellationEvent.Close();

            Logger.LogDebug($"{nameof(Stop)} > Stopped.");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The uploader should not throw in order to avoid disrupting the game.")]
        private void ProcessLogFiles()
        {
            while (!_cancellationEvent.WaitOne(0))
            {
                try
                {
                    // Get the list of files and sort them. The naming convention should yield
                    // a list that sorts in ascending order by date (i.e. oldest first).
                    var filePaths = Directory.GetFiles(
                        LogFileManager.GetLogFileDirectory(LogFileType.Game),
                        "*.log");

                    Array.Sort(filePaths);

                    // TODO: [Edge Case] Upload the last file on shutdown.
                    // TODO: [Edge Case] Invalid file name sorts after last valid file causing attempt to upload active file.

                    // Process each file. Skip the most recent file because it may be open.
                    for (int i = 0; i < filePaths.Length - 1; i++)
                    {
                        if (_cancellationEvent.WaitOne(0))
                            break;

                        ProcessLogFile(filePaths[i]);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(UploaderErrorMessage, ex);
                }
                finally
                {
                    // Cancellation-aware sleep.
                    _cancellationEvent.WaitOne(_processorSleepTimeout);
                }
            }
        }

        private void ProcessLogFile(string filePath)
        {
            Logger.LogDebug($"{nameof(ProcessLogFile)} > Processing log file \"{Path.GetFileName(filePath)}\".");

            if (!ProcessLogFileValidate(filePath))
            {
                ProcessLogFileArchive(filePath);

                return;
            }

            if (ProcessLogFileUpload(filePath))
            {
                ProcessLogFileArchive(filePath);
            }
        }

        private void ProcessLogFileArchive(string filePath)
        {
            Logger.LogDebug($"{nameof(ProcessLogFileArchive)} > Archiving log file.");

            var archiveFilePath = Path.Combine(
                _archiveDirectory,
                Path.GetFileName(filePath));

            File.Move(filePath, archiveFilePath);
        }

        private bool ProcessLogFileUpload(string filePath)
        {
            Logger.LogDebug($"{nameof(ProcessLogFileUpload)} > Uploading log file.");

            try
            {
                var json = ProcessLogFileUploadCreateRequestJson(filePath);

                Logger.LogDebug($"{nameof(ProcessLogFileUpload)} > Request JSON: {json}");

                var request = (HttpWebRequest)WebRequest.Create(_uploadUri);
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Post;

                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    Logger.LogDebug($"{nameof(ProcessLogFileUpload)} > Request response code is {response.StatusCode}.");
                }

                return true;
            }
            catch (WebException ex)
            {
                Logger.LogError($"{nameof(ProcessLogFileUpload)} > Unable to upload log file.", ex);

                return false;
            }
        }

        private string ProcessLogFileUploadCreateRequestJson(string filePath)
        {
            Logger.LogDebug($"{nameof(ProcessLogFileUploadCreateRequestJson)} > Creating request JSON.");

            var fileName = Path.GetFileName(filePath);
            var fileContent = File.ReadAllText(filePath);
            var fileContentEscaped = JsonHelper.Escape(fileContent);

            var json = $"{{ \"FileName\": \"{fileName}\", \"FileContent\": \"{fileContentEscaped}\" }}";

            return json;
        }

        /// <summary>
        /// Process server certificate validation.
        /// </summary>
        /// <remarks>
        /// Support for secure requests has been deferred. This method remains to support future work.
        /// </remarks>
        private bool ProcessLogFileUploadServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            Logger.LogDebug($"{nameof(ProcessLogFileUploadServerCertificate)} > Validating server certificate.");

            if (policyErrors != SslPolicyErrors.None)
            {
                var message = string.Empty;

                if ((policyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable)
                {
                    message = "The server did not present a certificate.";
                }
                else
                {
                    if ((policyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
                    {
                        message = "The server certificate name does not match the domain name.";
                    }

                    if ((policyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                    {
                        if (!string.IsNullOrEmpty(message))
                            message += " ";

                        message += "The server certificate chain has additional problems.";
                    }
                }

                Logger.LogWarn($"{nameof(ProcessLogFileUploadServerCertificate)} > The server certificate could not be validated. {message}");
            }

            return true;
        }

        private bool ProcessLogFileValidate(string filePath)
        {
            Logger.LogDebug($"{nameof(ProcessLogFileValidate)} > Validating log file.");

            // Check the file name.
            if (!_fileNameExpression.IsMatch(Path.GetFileName(filePath)))
            {
                Logger.LogWarn($"{nameof(ProcessLogFileValidate)} > Skipping log file with invalid name.");

                return false;
            }

            // Check the file size.
            if (new FileInfo(filePath).Length > FileSizeMaximum)
            {
                Logger.LogWarn($"{nameof(ProcessLogFileValidate)} > Skipping log file exceeding maximum size.");

                return false;
            }

            return true;
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cancellationEvent?.Close();
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
