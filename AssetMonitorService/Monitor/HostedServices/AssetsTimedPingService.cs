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
    public class AssetsTimedPingService : AssetsTimedServiceBase<AssetsTimedPingService, IAssetPingService>
    {
        public AssetsTimedPingService(ILogger<AssetsTimedPingService> logger,
                IServiceScopeFactory scopeFactory,
                TimeSpan? scanTime = null) : base(logger, scopeFactory, scanTime)
        {
        }

        protected override async Task<IEnumerable<Asset>> GetAssets(IAssetMonitorRepository repository)
        {
            return await repository.GetAllAssetsAsync();
        }

        protected override async Task GetTask(IAssetPingService iAssetService, Asset asset)
        {
            _logger.LogInformation($"Service {this.GetType().Name} ping to: {asset.IpAddress}");
            await iAssetService.PingHostAsync(asset.IpAddress);
        }
    }
}
