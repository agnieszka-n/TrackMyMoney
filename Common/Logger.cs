using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Common
{
    public static class Logger
    {
        public static void LogError(object source, Exception ex)
        {
            NLog.Logger logger = LogManager.GetLogger(source.GetType().Name);
            logger.Error(ex);
            Trace.WriteLine(ex);
        }

        public static void LogError(object source, Exception ex, string leadingMessage)
        {
            NLog.Logger logger = LogManager.GetLogger(source.GetType().Name);
            logger.Error(leadingMessage);
            logger.Error(ex);
            Trace.WriteLine(ex);
        }
    }
}
