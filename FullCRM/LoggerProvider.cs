using FullCRM.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerService _loggerService;
        public LoggerProvider(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(_loggerService);
        }

        public void Dispose()
        {

        }
    }
}
