using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStore.Logger
{

    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string category, XmlElement configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(logger_repository.Name, category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, configuration);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _log.IsDebugEnabled,
            LogLevel.Debug => _log.IsDebugEnabled,
            LogLevel.Information => _log.IsInfoEnabled,
            LogLevel.Warning => _log.IsWarnEnabled,
            LogLevel.Error => _log.IsErrorEnabled,
            LogLevel.Critical => _log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
       

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception error, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentOutOfRangeException(nameof(formatter));

            var log_message = formatter(state, error);

            if (string.IsNullOrEmpty(log_message) && error is null) return;

            switch (logLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(log_message);
                    break;


                case LogLevel.Information:
                    _log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _log.Error(log_message, error);
                    break;

                case LogLevel.Critical:
                    _log.Fatal(log_message, error);
                    break;
            }
        }
    }
}
