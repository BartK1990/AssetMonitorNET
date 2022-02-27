using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
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
            var assets = (await repository.GetAgentTagSetByAssetIdAsync()).ToList();
            foreach (var asset in assets)
            {
                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(asset.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                // Only for localhost no TCP port is required
                if (asset.IpAddress == IPAddress.Loopback.ToString())
                {
                    AssetsData.Add(new AssetPerformanceData(asset.AgentTagSet.AgentTag)
                    {
                        Id = asset.Id,
                        IpAddress = asset.IpAddress,
                        TcpPort = null,
                    });
                    continue;
                }

                // Properties
                int tcpPort = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.AgentTcpPort, int.Parse, 9560);

                AssetsData.Add(new AssetPerformanceData(asset.AgentTagSet.AgentTag)
                {
                    Id = asset.Id,
                    IpAddress = asset.IpAddress,
                    TcpPort = tcpPort
                });
            }
        }
    }
}
