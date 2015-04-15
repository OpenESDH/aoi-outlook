using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenEsdh.Outlook.Model.Logging
{
    public class Logger
    {
        private const string SourceName = "Application";
        private const string ServiceName = "OpenESDH.Outlook";
        private static Logger _logger = null;
        private static object _lock = new object();

        public static Logger Current
        {
            get
            {
                if(_logger==null)
                {
                    lock(_lock)
                    {
                        if(_logger==null)
                        {
                            _logger = new Logger();
                        }
                    }
                }
                return _logger;
            }
        }

        private const int MaxEventLogEntryLength = 30000;

        public string Source { get; set; }

        public  void LogDebug(string message, bool debugLoggingEnabled, string source = "")
        {
            if (debugLoggingEnabled == false) { return; }

            Log(message, EventLogEntryType.Information, source);
        }

        public  void LogInformation(string message, string source = "")
        {
            Log(message, EventLogEntryType.Information, source);
        }

        public  void LogWarning(string message, string source = "")
        {
            Log(message, EventLogEntryType.Warning, source);
        }

        public  void LogException(Exception ex, string source = "")
        {
            if (ex == null) { throw new ArgumentNullException("ex"); }

            if (Environment.UserInteractive)
            {
                Console.WriteLine(ex.ToString());
            }

            Log(ex.ToString(), EventLogEntryType.Error, source);
        }


        private  void Log(string message, EventLogEntryType entryType, string source)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    source = GetSource();
                }

                string possiblyTruncatedMessage = EnsureLogMessageLimit(message);

                using (EventLog eventlog = new EventLog(SourceName))
                {
                    eventlog.Source = ServiceName;
                    eventlog.BeginInit();
                    if (!EventLog.SourceExists(eventlog.Source))
                    {
                        EventLog.CreateEventSource(eventlog.Source, eventlog.Log);
                    }
                    eventlog.EndInit();
                    //EventLog.WriteEntry(SourceName, possiblyTruncatedMessage, entryType);
                    eventlog.WriteEntry(source + ":" + message, entryType, 1);
                }
                if (Environment.UserInteractive)
                {
                    Console.WriteLine(message);
                }
            }catch
            {

            }
        }

        private  string GetSource()
        {
            if (!string.IsNullOrWhiteSpace(Source)) { return Source; }

            try
            {
                var assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                {
                    assembly = Assembly.GetExecutingAssembly();
                }


                if (assembly == null)
                {
                    assembly = new StackTrace().GetFrames().Last().GetMethod().Module.Assembly;
                }

                if (assembly == null) { return "Unknown"; }

                return assembly.GetName().Name;
            }
            catch
            {
                return "Unknown";
            }
        }

        private  string EnsureLogMessageLimit(string logMessage)
        {
            if (logMessage.Length > MaxEventLogEntryLength)
            {
                string truncateWarningText = string.Format(CultureInfo.CurrentCulture, "... | Log Message Truncated [ Limit: {0} ]", MaxEventLogEntryLength);
                logMessage = logMessage.Substring(0, MaxEventLogEntryLength - truncateWarningText.Length);
                logMessage = string.Format(CultureInfo.CurrentCulture, "{0}{1}", logMessage, truncateWarningText);
            }

            return logMessage;
        }
    }

}
