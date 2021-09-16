using NLog;
using SAT.SERVICES.Interfaces;
using System;
using System.Reflection;
using NLog.Config;
using NLog.Targets;
using System.Diagnostics;

namespace SAT.SERVICES.Services
{
    public class LoggerService : ILoggerService
    {
        #region Initializador
#if DEBUG
        private static Logger _logger = ConfigureLogger("DebugLog");
#else
        private static Logger _logger = ConfigureLogger("ReleaseLog");
#endif
        #endregion

        public static void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        public static void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public static void LogWarn(string message)
        {
            _logger.Warn(message);
        }

        #region Implenetacoes Interface

        void ILoggerService.LogDebug(string message) { LogDebug(message); }
        void ILoggerService.LogError(string message) { LogError(message); }
        void ILoggerService.LogInfo(string message) { LogInfo(message); }
        void ILoggerService.LogWarn(string message) { LogWarn(message); }

        #endregion

        #region Configuracao de Logs

        /// Create Custom Logger using parameters passed.
        /// </summary>
        /// <param name="name">Name of file.</param>
        /// <param name="LogEntryLayout">Give "" if you want just message. If omited will switch to full log paramaters.</param>
        /// <param name="logFileLayout">Filename only. No extension or file paths accepted.</param>
        /// <param name="absoluteFilePath">If you want to save the log file to different path thatn application default log path, specify the path here.</param>
        /// <returns>New instance of NLog logger completly isolated from default instance if any</returns>
        private static Logger ConfigureLogger(string name)
        {
            string assemblyPathOfCallingProject = DirectoryOfCallingClass().Split("bin")[0];
            string LogEntryLayout = "${ date:format=dd.MM.yyyy HH\\:mm\\:ss.fff} thread[${threadid}] ${logger} (${level:uppercase=true}): ${message}. ${exception:format=ToString}";
            string logFileLayout = "{0}/Logs/{1}/{2}.txt";
            string absoluteFilePath = "";

            var factory = new LogFactory();
            var target = new FileTarget();
            target.Name = name;
            if (absoluteFilePath == "")
                target.FileName = string.Format(logFileLayout, assemblyPathOfCallingProject, name, DateTime.Now.ToString("dd-MM-yyyy"));
            else
                target.FileName = string.Format(absoluteFilePath + "//" + logFileLayout, name);
            if (LogEntryLayout == "") //if user specifes "" then use default layout.
                target.Layout = "${longdate} ${level:uppercase=true} ${message}";
            else
                target.Layout = LogEntryLayout;
            var defaultconfig = LogManager.Configuration;
            var config = new LoggingConfiguration();
            config.AddTarget(name, target);

            var ruleInfo = new LoggingRule("*", NLog.LogLevel.Trace, target);

            config.LoggingRules.Add(ruleInfo);

            factory.Configuration = config;

            return factory.GetCurrentClassLogger();
        }

        /// <summary>
        /// Create Custom Logger using a seperate configuration file.
        /// </summary>
        /// <param name="name">Name of file.</param>
        /// <returns>New instance of NLog logger completly isolated from default instance if any</returns>
        private static Logger CreateCustomLoggerFromConfig(string configname)
        {
            var factory = new LogFactory(new XmlLoggingConfiguration(configname));
            return factory.GetCurrentClassLogger();
        }

        #endregion

        #region Log Trace

        private static string NameOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }

        private static string DirectoryOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = (declaringType.Module).Assembly.Location;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }

        #endregion
    }
}
