using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace MarkTheWorld.ExceptionsHandling
{
    public class FileExceptionLogger : ExceptionLogger
    {
        private const string LOG_PATH = @"C:\00\logs\errorLogs.log";

        public override void Log(ExceptionLoggerContext context)
        {
            File.AppendAllText(
                LOG_PATH,
                String.Format("{0} | {1}", DateTime.UtcNow, context.Exception));
        }
    }
}