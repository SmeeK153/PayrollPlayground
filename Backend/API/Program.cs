using System;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Starting service");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped service because of an exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateHostBuilder([AllowNull] string[] args = null) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseLogging()
                .ConfigureShutdownTimeout()
                .UseConfigurations()
                .UseNLog()
                .UseKestrel()
                .UseStartup<Startup>();
    }
}