using AssetMonitorService.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsTimedPollService : IHostedService, IDisposable
    {
        private readonly ILogger<AssetsTimedPollService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public AssetsTimedPollService(ILogger<AssetsTimedPollService> logger, IServiceScopeFactory scopeFactory)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service is running.");

            _timer = new Timer(
                GetAssetsData,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(10)
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
            var assetService = scope.ServiceProvider.GetRequiredService<IAssetGetDataService>();

            try
            {
                _logger.LogInformation(assetService.GetAssetsDataAsync().Result);
            }
            catch
            {
                _logger.LogWarning($"Cannot connect");
            }

        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
