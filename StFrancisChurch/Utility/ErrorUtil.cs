using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace StFrancisChurch.Utility
{
    public class ErrorUtil
    {
        static string filename = string.Format("{0}//{1:dd-MMM-yyyy}.txt", System.Configuration.ConfigurationManager.AppSettings["Logfile"], DateTime.Now);

        public static void Log(string message)
        {
            using (StreamWriter w = File.AppendText(System.Configuration.ConfigurationManager.AppSettings["Logfile"] + "error_log.txt"))
            {
                w.WriteLine(message + " - " + DateTime.Now);
            }
        }

        public static void LogInfo(string MethodName, object message)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"));
                sb.AppendLine("caller: " + MethodName + "\n: " + message);

                using (System.IO.StreamWriter str = new System.IO.StreamWriter(filename, true))
                {
                    str.WriteLineAsync(sb.ToString());
                }
            }
            catch { }
        }

        public static string LogError(Exception ex)
        {
            string message = "";
            try
            {
                message = GetExceptionMessages(ex);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Date and Time: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"));
                sb.AppendLine("ErrorMessage: \n " + message);
                sb.AppendLine(ex.StackTrace);

                using (System.IO.StreamWriter str = new System.IO.StreamWriter(filename, true))
                {
                    str.WriteLineAsync(sb.ToString());
                }
            }
            catch { }
            return message;
        }

        static string GetExceptionMessages(Exception ex)
        {
            string ret = string.Empty;
            if (ex != null)
            {
                ret = ex.Message;
                if (ex.InnerException != null)
                    ret = ret + "\n" + GetExceptionMessages(ex.InnerException);
            }
            return ret;
        }
    }
}