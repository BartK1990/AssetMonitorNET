using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public abstract class AssetsTimedServiceBase<TTimed> : IHostedService, IDisposable
    {
        protected readonly ILogger<TTimed> _logger;
        public readonly TimeSpan ScanTime;

        protected readonly object taskLock = new object();
        protected Timer _timer;
        protected List<Task> _taskList = new List<Task>();

        public AssetsTimedServiceBase(ILogger<TTimed> logger, TimeSpan? scanTime = null)
        {
            if (scanTime != null)
            {
                this.ScanTime = (TimeSpan)scanTime;
            }
            else
            { // Default value
                this.ScanTime = TimeSpan.FromSeconds(10);
            }
            this._logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is running. Timer scan time: {ScanTime}");

            _timer = new Timer(
                TimedJobWrapper,
                null,
                TimeSpan.Zero,
                ScanTime
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is stopping");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected abstract void TimedJob();

        private void TimedJobWrapper(object state)
        {
            try
            {
                TimedJob();
            }
            catch (Exception e)
            {
                _logger.LogError($"{this.GetType().Name} - Hosted Service exception!");
                _logger.LogDebug(e.Message);
            }
        }
    }
}