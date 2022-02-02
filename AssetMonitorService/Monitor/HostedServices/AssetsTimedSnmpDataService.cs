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
    public class AssetsTimedSnmpDataService : AssetsTimedDataServiceBase<AssetsTimedSnmpDataService,
        IAssetSnmpDataService,
        AssetSnmpData>
    {
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataSharedService;

        public AssetsTimedSnmpDataService(IAssetsSnmpDataSharedService assetsSnmpDataSharedService,
            ILogger<AssetsTimedSnmpDataService> logger,
            IServiceScopeFactory scopeFactory,
            TimeSpan? scanTime = null
            ) : base(scopeFactory: scopeFactory, logger: logger, scanTime: scanTime)
        {
            this._assetsSnmpDataSharedService = assetsSnmpDataSharedService;
        }

        protected override IEnumerable<AssetSnmpData> GetAssets()
        {
            return _assetsSnmpDataSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetSnmpDataService iAssetService, AssetSnmpData asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} request to: {asset.IpAddress}:{asset.UdpPort}");
            await iAssetService.UpdateAsset(asset);
        }
    }
}
