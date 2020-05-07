using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions
{
    public static partial class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder builder)
        {
            builder.ConfigureLogging((context, builder) =>
                builder
                    .ClearProviders()
                    .AddConfiguration(context.Configuration.GetSection("Logging"))
                    .AddDebug()
                    .AddEventSourceLogger()
                /* ...Add more logging as needed */);
            return builder;
        }

        public static IWebHostBuilder ConfigureShutdownTimeout(this IWebHostBuilder builder)
        {
            builder.UseShutdownTimeout(new TimeSpan(0, 0, 25));
            return builder;
        }
        
        public static IWebHostBuilder UseConfigurations(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, builder) =>
                builder
                    .AddJsonFile("appsettings.json", false)
                /* ...Add more configuration files as needed */);
            return builder;
        }
    }
}