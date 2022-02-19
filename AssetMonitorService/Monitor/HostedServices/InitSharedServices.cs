using AssetMonitorService.Monitor.SingletonServices;
using AssetMonitorService.Monitor.SingletonServices.Historical;
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
        private readonly IHistoricalTablesSharedService _historicalTablesShared;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;

        public InitSharedServices(ILogger<InitSharedServices> _logger,
        IAssetsCollectionSharedService assetsCollectionShared,
            IAssetsPingSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IHistoricalTablesSharedService historicalTablesShared,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared
            )
        {
            this._logger = _logger;
            this._assetsCollectionShared = assetsCollectionShared;
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._historicalTablesShared = historicalTablesShared;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{this.GetType().Name} - Hosted Service started initialization");
            await this._assetsCollectionShared.UpdateAssetsListBase();
            await this._assetsPingShared.UpdateAssetsListBase();
            await this._assetsPerformanceDataShared.UpdateAssetsListBase();
            await this._assetsSnmpDataShared.UpdateAssetsListBase();
            await this._assetsHistoricalDataShared.UpdateAssetsListBase();
            await this._historicalTablesShared.DatabaseStructureUpdateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
