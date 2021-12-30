using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
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
    //_taskList.Add(assetService.GetAssetsDataAsync(a.IpAddress, AgentTcpPort));
    //_logger.LogInformation($"Service {this.GetType().Name} request to: {a.IpAddress}:{AgentTcpPort}");
    //var assets = assetRepository.GetWindowsAssetsAsync().Result.ToList();
    public class AssetsTimedPerformanceDataService : AssetsTimedServiceBase<AssetsTimedPerformanceDataService, 
        IAssetGetPerformanceDataService,
        AssetPerformanceData>
    {
        public const int AgentTcpPort = 9560;
        private IAssetsPerformanceDataSharedService _assetsPerformanceDataSharedService;

        public AssetsTimedPerformanceDataService(IAssetsPerformanceDataSharedService assetsPerformanceDataSharedService, 
            ILogger<AssetsTimedPerformanceDataService> logger,
            IServiceScopeFactory scopeFactory,
            TimeSpan? scanTime = null) : base(logger, scopeFactory, scanTime)
        {
            this._assetsPerformanceDataSharedService = assetsPerformanceDataSharedService;
        }

        protected override IEnumerable<AssetPerformanceData> GetAssets()
        {
            return _assetsPerformanceDataSharedService.AssetsData;
        }

        protected override async Task GetTask(IAssetGetPerformanceDataService iAssetService, AssetPerformanceData asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} request to: {asset.IpAddress}:{AgentTcpPort}");
            await iAssetService.GetAssetsDataAsync(asset.IpAddress, AgentTcpPort);
        }
    }
}
