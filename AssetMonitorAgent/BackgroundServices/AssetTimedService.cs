using AssetMonitorAgent.Services;
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
        public readonly TimeSpan ScanTime;

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
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is running.");

            _timer = new Timer(
                GetAssetsData,
                null,
                TimeSpan.Zero,
                ScanTime
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void GetAssetsData(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var assetService = scope.ServiceProvider.GetRequiredService<IAssetPerformanceService>();

            _assetDataSharedService.CpuUsage = assetService.CpuUsage;
            _assetDataSharedService.MemoryAvailableMB = assetService.MemoryAvailableMB;
            _assetDataSharedService.MemoryTotalMB = assetService.MemoryTotalMB;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
