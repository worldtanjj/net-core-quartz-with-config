using Microsoft.Extensions.Hosting;
using System.IO;
using HostedService.Quartz.Jobs;
using HostedService.Quartz.Q1.Foundation.Quartz.HostedService;
using HostedService.Quartz.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz.Impl;
using Quartz.Spi;
using NLog.Extensions.Logging;
using System.Linq;
using Quartz;
using Quartz.Infrastructure.GameBackend;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HostedService.Quartz
{
    class Program
    {
        static void Main()
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddEnvironmentVariables("ASPNETCORE_");
                    //configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", true, true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
                    configApp.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<GameBackendContext>(options =>
                        options.UseMySql(hostContext.Configuration.GetConnectionString("MySql_GameBackend")));

                    services.AddLogging();
                    var serviceProvider = services.BuildServiceProvider();
                    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                    loggerFactory.AddNLog();

                    services.AddSingleton<IJobFactory, JobFactory>();
                    services.AddHostedService<QuartzService>();
                    services.AddSingleton(provider =>
                    {
                        var option = new QuartzOption(hostContext.Configuration);
                        var sf = new StdSchedulerFactory(option.ToProperties());
                        var scheduler = sf.GetScheduler().Result;
                        scheduler.JobFactory = provider.GetService<IJobFactory>();
                        return scheduler;
                    });

                    var jobs = typeof(Program).Assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IJob))).ToList();
                    foreach (var job in jobs)
                    {
                        services.AddSingleton(job);
                        //services.AddSingleton<ajob, ajob>();

                    }
                })
                .UseConsoleLifetime()
                .Build();

            host.Run();
        }
    }
}
