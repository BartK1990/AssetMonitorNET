using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Services;
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
    public class AssetsTimedPerformanceDataService : AssetsTimedServiceBase<AssetsTimedPerformanceDataService, IAssetGetPerformanceDataService>
    {
        public const int AgentTcpPort = 9560;

        public AssetsTimedPerformanceDataService(ILogger<AssetsTimedPerformanceDataService> logger,
                IServiceScopeFactory scopeFactory,
                TimeSpan? scanTime = null) : base(logger, scopeFactory, scanTime)
        {
        }

        protected override async Task<IEnumerable<Asset>> GetAssets(IAssetMonitorRepository repository)
        {
            return await repository.GetWindowsAssetsAsync();
        }

        protected override async Task GetTask(IAssetGetPerformanceDataService iAssetService, Asset asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} request to: {asset.IpAddress}:{AgentTcpPort}");
            await iAssetService.GetAssetsDataAsync(asset.IpAddress, AgentTcpPort);
        }
    }
}
