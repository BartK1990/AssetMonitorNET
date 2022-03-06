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
    public class AssetsTimedIcmpDataService : AssetsTimedDataServiceBase<AssetsTimedIcmpDataService, 
        IAssetIcmpDataService, 
        AssetIcmpData>
    {
        private const int ScanTimeInSecondsDefault = 10;

        private readonly IAssetsIcmpSharedService _assetsPingSharedService;

        public AssetsTimedIcmpDataService(IAssetsIcmpSharedService assetsPingSharedService,
            ILogger<AssetsTimedIcmpDataService> logger,
            IServiceScopeFactory scopeFactory,
            IApplicationPropertiesService appProperties
            ) : base(scopeFactory: scopeFactory, logger: logger, appProperties: appProperties)
        {
            this._assetsPingSharedService = assetsPingSharedService;
        }

        protected override IEnumerable<AssetIcmpData> GetAssets()
        {
            return _assetsPingSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetIcmpDataService iAssetService, AssetIcmpData asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} ping to: {asset.IpAddress}");
            await iAssetService.UpdateAsset(asset);
        }

        protected override int GetScanTimeInSeconds()
        {
            return _appProperties.GetProperty(ApplicationPropertyNameEnum.AssetsTimedIcmpDataScanTime, int.Parse, ScanTimeInSecondsDefault).Result;
        }

    }
}
