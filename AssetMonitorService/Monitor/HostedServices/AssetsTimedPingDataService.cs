using AssetMonitorService.Monitor.Model;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsTimedPingDataService : AssetsTimedDataServiceBase<AssetsTimedPingDataService, 
        IAssetPingDataService, 
        AssetPing>
    {
        private readonly IAssetsPingSharedService _assetsPingSharedService;

        public AssetsTimedPingDataService(IAssetsPingSharedService assetsPingSharedService,
            ILogger<AssetsTimedPingDataService> logger,
            IServiceScopeFactory scopeFactory,
            TimeSpan? scanTime = null
            ) : base(scopeFactory: scopeFactory, logger: logger, scanTime: scanTime)
        {
            this._assetsPingSharedService = assetsPingSharedService;
        }

        protected override IEnumerable<AssetPing> GetAssets()
        {
            return _assetsPingSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetPingDataService iAssetService, AssetPing asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} ping to: {asset.IpAddress}");
            await iAssetService.UpdateAsset(asset);
        }
    }
}
