using Betacomare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Betacomare.Classes
{
    public class Log
    {
        public static void ExceptionToDBLog(Exception e, AdventureWorksLt2019Context _context)
        {
            
            //new throw BadHttpRequestException
            string[] stackTrace = e.StackTrace.Split("\r\n"); // split the string that give us more information about the location of the error

            string atMethod = e.TargetSite + ";";
            foreach (string stack in stackTrace)
                atMethod += stack + ";";
            _context.ErrorLogs.Add(new ErrorLog
            {
                ErrorTime = DateTime.Now,
                UserName = string.Empty,
                ErrorNumber = 0,
                ErrorSeverity = 0,
                ErrorState= 0,
                ErrorProcedure = string.Empty,
                ErrorLine = 0,
                ErrorMessage = e.Message,
            });
           
            //cmdSaveExToLog.Parameters.Add("@Data", SqlDbType.DateTime).Value = DateTime.Now;
            //cmdSaveExToLog.Parameters.Add(new SqlParameter("@Data", DateTime.Now));

        }
    }
}
