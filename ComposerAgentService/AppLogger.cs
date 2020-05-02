using System;
using System.IO;

namespace ComposerAgentService
{
    public enum LoggerSeverity
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
    public static class AppLogger
    {

        public static string FileName
        {
            get; set;
        }        

        public static LoggerSeverity SeverityThreshold { get; set; }


        public static void WriteLine(string Line, string args)
        {
            WriteLine(String.Format(Line, args));
        }
        public static void WriteLine(string Line, string[] args) {
            WriteLine(String.Format(Line, args));
        }        

        public static void WriteLine(LoggerSeverity Severity, string Line, string[] args)
        {
            WriteLine(String.Format(Line, args),Severity);
        }

        public static void WriteLine(string Line, Exception e = null)
        {
            WriteLine(Line, LoggerSeverity.Debug, e);
        }
        public static void WriteLine(string Line, LoggerSeverity Severity, Exception e = null)
        {
            string strSeverity;
            if (e != null) Line += " (" + e.Message.Replace("\n", " ") + ")";
            try
            {
                if (Severity >= SeverityThreshold)
                {
                    switch (Severity)
                    {
                        case LoggerSeverity.Critical:
                            strSeverity = "CRITICAL";
                            break;
                        case LoggerSeverity.Error:
                            strSeverity = "ERROR";
                            break;
                        case LoggerSeverity.Warning:
                            strSeverity = "WARNING";
                            break;
                        case LoggerSeverity.Info:
                            strSeverity = "INFORMATION";
                            break;
                        case LoggerSeverity.Debug:
                        default:
                            strSeverity = "DEBUG";
                            break;
                    }
                    using (StreamWriter sw = File.AppendText(FileName))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + ": " + strSeverity + " " + Line);
                        Console.WriteLine(Line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to write to log file: " + ex.Message);
            }

        }

    }
}
