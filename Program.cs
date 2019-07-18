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
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var theApplication = serviceProvider.GetService<App>();
            theApplication.Run();


            Log.CloseAndFlush();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddSerilog()
                .AddDebug());
                 
            serviceCollection.AddLogging();


            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
                
            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton(configuration);

            // // Add services
            // serviceCollection.AddTransient<IBackupService, BackupService>();

            // Add app
            serviceCollection.AddTransient<App>();
        }
    }
}
