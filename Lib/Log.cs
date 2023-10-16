using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public static class Log
    {
        #region Print Formatted exception (e) to the file log (path)
        /* -- Print an exception on the log file (path) -- */
        public static void PrintExToFormatLog(Exception e, string path)
        {
            DateTime dataTime = DateTime.Now; // restituisce, come oggetto DataTime, l'ora attuale
            string[] stackTrace = e.StackTrace.Split("\r\n");       // split the string that give us more information about the location of the error


            // if the file for logs doe not exist, it will be created
            File.AppendAllText(path, $"# {Environment.UserName} - {dataTime} #\r\n" +   // username who generated the exception + date and time
                                     $"Exception:   {e.GetType().Name}\r\n" +           // group/type/caption of the exception
                                     $"At Method:   {e.TargetSite}\r\n");               // whitin which method

            foreach (string stack in stackTrace)
                File.AppendAllText(path, $"             {stack}\r\n");                  // at method... (more information)

            File.AppendAllText(path, $"Message:     {e.Message}\r\n" +                  // message in the throw of the exception
                                      "\r\n");
        }
        #endregion

        #region Print exception (e) to the file log (path)
        public static void PrintExToLog(Exception e, string path)
        {
            DateTime dataTime = DateTime.Now; // restituisce, come oggetto DataTime, l'ora attuale
            File.AppendAllText(path, $"# {dataTime} #\r\n" + e.ToString() + "\r\n\r\n");
        }
        #endregion
    }
}
