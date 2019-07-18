using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace Gofmx.Logging.Test
{
    public class App
    {
        private readonly ILogger<App> _logger;

        public App(ILogger<App> logger)
        {
            _logger = logger;
        }

        public void Run()
        {

            _logger.LogTrace("Logging Trace");

            _logger.LogDebug("Logging Debug");

            // structured logging
            var ec = new Example()
            {
                Name = "Kee Test 1",
                CreateDate = DateTime.Now
            };

            _logger.LogInformation("Example Class Property Name {name}", ec.Name);
            
            _logger.LogInformation("Example Class Decomposition {@example}", ec);

            _logger.LogError(new Exception("Test Exception"), "Logging Error");

        }
    }
}
