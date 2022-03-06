using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Services;
using AssetMonitorService.Monitor.Services.Asset.Live;
using AssetMonitorService.Monitor.SingletonServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsTimedSnmpDataService : AssetsTimedDataServiceBase<AssetsTimedSnmpDataService,
        IAssetSnmpDataService,
        AssetSnmpData>
    {
        private const int ScanTimeInSecondsDefault = 10;

        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataSharedService;

        public AssetsTimedSnmpDataService(IAssetsSnmpDataSharedService assetsSnmpDataSharedService,
            ILogger<AssetsTimedSnmpDataService> logger,
            IServiceScopeFactory scopeFactory,
            IApplicationPropertiesService appProperties
            ) : base(scopeFactory: scopeFactory, logger: logger, appProperties: appProperties)
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

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.AssetsTimedSnmpDataScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }

    }
}
