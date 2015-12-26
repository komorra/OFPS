using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace OFPSEngine
{
    public class Logger
    {
        private static ILog log = LogManager.GetLogger(typeof (Logger));

        static Logger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = @"Logs\EngineLog.txt";
            roller.Layout = patternLayout;            
            roller.RollingStyle = RollingFileAppender.RollingMode.Date;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }

        public static void Debug(string formatString, params object[] parameters)
        {
            log.DebugFormat(formatString, parameters);
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Info(string formatString, params object[] parameters)
        {
            log.InfoFormat(formatString, parameters);
        }

        public static void Warn(string message)
        {
            log.Warn(message);
        }

        public static void Warn(string formatString, params object[] parameters)
        {
            log.WarnFormat(formatString, parameters);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Error(string formatString, params object[] parameters)
        {
            log.ErrorFormat(formatString, parameters);
        }

        public static void Fatal(string message)
        {
            log.Fatal(message);
        }

        public static void Fatal(string formatString, params object[] parameters)
        {
            log.FatalFormat(formatString, parameters);
        }
    }
}
