using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Quartz;

namespace HostedService.Quartz.Services
{
    public class QuartzService : IHostedService
    {
        private readonly Logger _logger;
        private readonly IScheduler _scheduler;

        public QuartzService(IScheduler scheduler)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _scheduler = scheduler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Info("开始Quartz调度...");
            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("停止Quartz调度...");
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}