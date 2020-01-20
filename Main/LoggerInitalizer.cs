using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyMoney.Main
{
    public static class LoggerInitalizer
    {
        public static void InitializeFileLogger(string fileName)
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("fileTarget")
            {
                Encoding = Encoding.UTF8,
                FileName = fileName,
                Layout = "${longdate} ${level:uppercase=true}\t${logger}\n${message}"
            };

            config.AddTarget(fileTarget);
            config.AddRuleForAllLevels(fileTarget);
            LogManager.Configuration = config;
        }
    }
}
