﻿using NLog;
using NLog.Config;
using NLog.Targets;

namespace TestWinformApp.Internal
{
    internal static class Logging
    {
        public static Logger Logger
        {
            get
            {
                if (!_isSetup)
                    Setup();
                return LogManager.GetLogger("default");
            }
        }

        #region Private
        private static bool _isSetup;
        private static object _lock = new object();
        private static void Setup()
        {
            lock (_lock)
            {
                if (_isSetup) return;

                var config = new LoggingConfiguration();
                var layout = @"${longdate} | ${level:uppercase=true:padding=-5} | ${message:withException=True}";
                var logConsole = new ConsoleTarget("logconsole")
                {
                    Layout = layout
                };
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
                LogManager.Configuration = config;
                _isSetup = true;
            }
        }
        #endregion
    }
}
