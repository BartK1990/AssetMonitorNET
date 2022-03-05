using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsTimedPerformanceDataService : AssetsTimedDataServiceBase<AssetsTimedPerformanceDataService, 
        IAssetPerformanceDataService,
        AssetPerformanceData>
    {
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataSharedService;

        public AssetsTimedPerformanceDataService(IAssetsPerformanceDataSharedService assetsPerformanceDataSharedService, 
            ILogger<AssetsTimedPerformanceDataService> logger,
            IServiceScopeFactory scopeFactory,
            TimeSpan? scanTime = null
            ) : base(scopeFactory: scopeFactory, logger: logger, scanTime: scanTime)
        {
            this._assetsPerformanceDataSharedService = assetsPerformanceDataSharedService;
        }

        protected override IEnumerable<AssetPerformanceData> GetAssets()
        {
            return _assetsPerformanceDataSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetPerformanceDataService iAssetService, AssetPerformanceData asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} request to: {asset.IpAddress}:{asset.TcpPort}");
            int scanTimeSecond = Convert.ToInt32(this.ScanTime.TotalSeconds);
            await iAssetService.UpdateAsset(asset, scanTimeSecond);
        }
    }
}
