using System;

using System.IO;


namespace ComposerAgent
{
    public static class FileLogger
    {        

        public static string FileName {
            get; set;
        } 

        public static void WriteLine(string Line, Exception e = null)
        {            
            if (e != null) Line += " (" + e.Message + ")";

            try { 
                using (StreamWriter sw = File.AppendText(FileName))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " +  Line);
                    Console.WriteLine(Line);
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Unable to write to log file: " + ex.Message);
            }

        }

    }
}
