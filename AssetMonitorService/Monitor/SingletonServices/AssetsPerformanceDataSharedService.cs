using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPerformanceDataSharedService : AssetsSharedServiceBase<AssetsPerformanceDataSharedService, AssetPerformanceData>, 
        IAssetsPerformanceDataSharedService
    {
        public AssetsPerformanceDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsPerformanceDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAgentAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                var tags = (await repository.GetAgentTagsByAssetIdAsync(asset.Id)).ToList();

                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(asset.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                var agentTags = tags
                    .Select(tag => new TagAgent(tag)).ToList();

                // Only for localhost no TCP port is required
                if (asset.IpAddress == IPAddress.Loopback.ToString())
                {
                    AssetsData.Add(new AssetPerformanceData(agentTags)
                    {
                        Id = asset.Id,
                        IpAddress = asset.IpAddress,
                        TcpPort = null,
                    });
                    continue;
                }

                // Properties
                int tcpPort = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.AgentTcpPort, int.Parse, 9560);

                AssetsData.Add(new AssetPerformanceData(agentTags)
                {
                    Id = asset.Id,
                    IpAddress = asset.IpAddress,
                    TcpPort = tcpPort
                });
            }
        }
    }
}
