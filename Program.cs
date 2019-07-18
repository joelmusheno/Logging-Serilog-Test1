using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Gofmx.Logging.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var logger = Log.ForContext<Program>();

            logger.Debug("Logging Debug");

            logger.Information("Logging Information");

            // structured logging
            var ec = new Example()
            {
                Name = "Kee Test 1",
                CreateDate = DateTime.Now
            };
            //logger.Information($"Example Class Property Name {ec.Name}");

            logger.Information("Example Class Property Name {name}", ec.Name);
            
            logger.Information("Example Class Decomposition {@example}", ec);

            logger.Error(new Exception("Test Exception"), "Logging Error");
        
            Log.CloseAndFlush();
        }
    }
}
