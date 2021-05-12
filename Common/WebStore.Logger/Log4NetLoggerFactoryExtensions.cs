using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string filePath)
        {
            if (filePath is not { Length: > 0 }) 
                throw new ArgumentException("Указан неверный путь к файлу", nameof(filePath));
            if (Path.IsPathRooted(filePath)) return filePath;

            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir!, filePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configFile = "log4net.config")
        {
            factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(configFile)));

            return factory;
        }
    }
}
