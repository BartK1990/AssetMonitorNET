using AspMVC_Monitor.Services.ScopedServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.HostedServices
{
    public abstract class TimedServiceBase<TTimed> : IHostedService, IDisposable
    {
        private const int ScanTimeInSecondsDefault = 10;

        protected readonly ILogger<TTimed> _logger;
        protected readonly IApplicationPropertiesService _appProperties;
        public TimeSpan ScanTime { get; private set; }

        protected readonly object taskLock = new object();
        protected Timer _timer;
        protected List<Task> _taskList = new List<Task>();

        public TimedServiceBase(ILogger<TTimed> logger, IApplicationPropertiesService appProperties)
        {
            this._logger = logger;
            this._appProperties = appProperties;
            this.ScanTime = TimeSpan.FromSeconds(GetScanTimeInSecondsWrapper());
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

        protected abstract int GetScanTimeInSeconds();

        private int GetScanTimeInSecondsWrapper()
        {
            var scanTime = GetScanTimeInSeconds();
            return scanTime > 0 ? scanTime : ScanTimeInSecondsDefault;
        }

    }
}
