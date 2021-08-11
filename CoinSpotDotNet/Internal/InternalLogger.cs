using Microsoft.Extensions.Logging;
using System;

namespace CoinSpotDotNet.Internal
{
    internal class InternalLogger : ILogger
    {
        private readonly string name;

        public InternalLogger(string name)
        {
            this.name = name;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var label = LabelFor(logLevel);
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss} [{label}]: {name} - {formatter(state, exception)}");
        }

        private static string LabelFor(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "TRACE",
                LogLevel.Debug => "DEBUG",
                LogLevel.Information => "INFO",
                LogLevel.Warning => "WARN",
                LogLevel.Error => "ERROR",
                LogLevel.Critical => "CRIT",
                LogLevel.None => "NONE",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Invalid LogLevel")
            };
        }
    }
}
