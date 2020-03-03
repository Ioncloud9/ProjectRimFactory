using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;
using Verse;

namespace ProjectRimFactory.Logging
{
    public class VerseLogger : LoggerBase
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        protected override void DoLogMessage(ILogToken lt, string msg, LoggingLevel level)
        {
            if (level == LoggingLevel.ERROR)
            {
                Log.Error(msg, true);
            }
            else
            {
                Log.Message($"[PRF.{level.Name}] {msg}");
            }
        }

        protected override void DoLogExceptionMessage(ILogToken lt, string message, Exception e, LoggingLevel level)
        {
            var sb = new StringBuilder();
            sb.Append($"[PRF.{level.Name}] {message}: ");
            sb.AppendLine(e.ToString());
            var curEx = e;
            while (curEx != null)
            {
                sb.AppendLine(curEx.ToString());
                curEx = curEx.InnerException;
            }
            if (level == LoggingLevel.ERROR)
            {
                Log.Error(sb.ToString());
            }
            else
            {
                Log.Message(sb.ToString());
            }
        }

        protected override void DoLogBeginScope(ILogScope scope, ILoggingContext ctx, ILogToken lt, string message) { }
        protected override void DoLogEndScope(ILogScope scope, ILoggingContext ctx, ILogToken lt, string message) { }
    }
}
