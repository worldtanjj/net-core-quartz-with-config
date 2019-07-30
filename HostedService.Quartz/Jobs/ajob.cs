using Microsoft.Extensions.Logging;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class ajob : IJob
    {
        private readonly Logger _logger;

        public ajob()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Thread.Sleep(1000 * 5);
            _logger.Info(string.Format("[{0:yyyy-MM-dd hh:mm:ss:ffffff}]任务执行！{1}", DateTime.Now, "ajob"));
            //_logger.LogInformation(string.Format("[{0:yyyy-MM-dd hh:mm:ss:ffffff}]任务执行！{1}", DateTime.Now, "ajob"));
            return Task.CompletedTask;
        }
    }
}
