using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class InitSharedServices : IHostedService
    {
        private readonly ILogger<InitSharedServices> _logger;
        private readonly IAssetsCollectionSharedService _assetsCollectionShared;
        private readonly IAssetsPingSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;

        public InitSharedServices(ILogger<InitSharedServices> _logger,
        IAssetsCollectionSharedService assetsCollectionShared,
            IAssetsPingSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared)
        {
            this._logger = _logger;
            this._assetsCollectionShared = assetsCollectionShared;
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service started initialization");
            await this._assetsCollectionShared.UpdateAssetsListBase();
            await this._assetsPingShared.UpdateAssetsListBase();
            await this._assetsPerformanceDataShared.UpdateAssetsListBase();
            await this._assetsSnmpDataShared.UpdateAssetsListBase();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
