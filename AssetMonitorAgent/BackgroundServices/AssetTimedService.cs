using AssetMonitorAgent.SingletonServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorAgent.BackgroundServices
{
    public class AssetTimedService : IHostedService, IDisposable
    {
        private readonly ILogger<AssetTimedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetDataSharedService _assetDataSharedService;
        private Timer _timer;
        public TimeSpan ScanTime { get; private set; }

        public AssetTimedService(ILogger<AssetTimedService> logger, IServiceScopeFactory scopeFactory, IAssetDataSharedService assetDataSharedService,
            TimeSpan? scanTime = null)
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
            this._scopeFactory = scopeFactory;
            this._assetDataSharedService = assetDataSharedService;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is running");

            _timer = new Timer(
                TimedJob,
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

        private void TimedJob(object state)
        {
            GetAssetData();
            GetScanTime();
        }

        private void GetAssetData()
        {
            _assetDataSharedService.UpdateData();
        }

        private void GetScanTime()
        {
            var newScanTime = _assetDataSharedService.ScanTime;
            if (newScanTime != this.ScanTime.TotalSeconds)
            {
                if(newScanTime > 0)
                {
                    ScanTime = TimeSpan.FromSeconds(newScanTime);
                    _timer.Change(TimeSpan.Zero, ScanTime);
                    _logger.LogInformation($"{this.GetType().Name} - Hosted Service new scan time (seconds): {newScanTime}");
                }
                else
                {
                    _logger.LogWarning($"{this.GetType().Name} - Hosted Service wrong scan time passed: {newScanTime}. Scan time not updated");
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
