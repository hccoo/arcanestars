using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcaneStars.JPService;
using ArcaneStars.Service;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ArcaneStars.Infrastructure.Logs;
using ArcaneStars.Service.Configurations;

namespace ArcaneStars.JPServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            IocProvider.Container = host.Services;
            IDProvider.InitIDWorker();
            SerilogProvider.StartWithMysql(IocProvider.GetService<IServiceConfigurationAgent>()?.LogConnectionString,"jpservice_log");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new DynamicProxyServiceProviderFactory()) // AspectCoreServiceProviderFactory()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
