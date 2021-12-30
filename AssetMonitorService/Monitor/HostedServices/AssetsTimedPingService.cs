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
    public class AssetsTimedPingService : AssetsTimedServiceBase<AssetsTimedPingService, 
        IAssetPingService, 
        AssetPing>
    {
        private IAssetsPingSharedService _assetsPingSharedService;

        public AssetsTimedPingService(IAssetsPingSharedService assetsPingSharedService,
                ILogger<AssetsTimedPingService> logger,
                IServiceScopeFactory scopeFactory,
                TimeSpan? scanTime = null
                ) : base(logger, scopeFactory, scanTime)
        {
            this._assetsPingSharedService = assetsPingSharedService;
        }

        protected override IEnumerable<AssetPing> GetAssets()
        {
            return _assetsPingSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetPingService iAssetService, AssetPing asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} ping to: {asset.IpAddress}");
            await iAssetService.PingHostAsync(asset.IpAddress);
        }
    }
}
