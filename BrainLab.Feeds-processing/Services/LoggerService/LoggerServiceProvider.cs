using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Services.LoggerService
{
    public class LoggerServiceProvider
    {
        public ILogger _logger { get; }
        public LoggerServiceProvider(ILogger<LoggerServiceProvider> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        public void Log(string message, object[] args)
        {
            _logger.LogInformation(message, args);
        }

    }
}
