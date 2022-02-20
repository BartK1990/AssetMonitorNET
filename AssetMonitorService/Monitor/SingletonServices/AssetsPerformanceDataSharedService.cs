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
            var assets = (await repository.GetAgentAssetsWithTagSetAsync()).ToList();
            foreach (var a in assets)
            {
                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(a.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                // Only for localhost no TCP port is required
                if (a.IpAddress == IPAddress.Loopback.ToString())
                {
                    AssetsData.Add(new AssetPerformanceData(a.AgentTagSet.AgentTag)
                    {
                        Id = a.Id,
                        IpAddress = a.IpAddress,
                        TcpPort = null,
                    });
                    continue;
                }

                var agentTcpPort = assetProperties?
                    .FirstOrDefault(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.AgentTcpPort)?.Value ?? null;
                if(agentTcpPort == null)
                {
                    agentTcpPort = "9560";
                    _logger.LogError($"No Agent TCP port specified for Asset (Id): {a.Id} | default {agentTcpPort} used");
                }
                if (int.TryParse(agentTcpPort, out var tcpPort))
                {
                    AssetsData.Add(new AssetPerformanceData(a.AgentTagSet.AgentTag)
                    {
                        Id = a.Id,
                        IpAddress = a.IpAddress,
                        TcpPort = tcpPort
                    });
                }
                else
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.AgentTcpPort]} for Asset: {a.Name} (Id: {a.Id})");
                }
            }
        }
    }
}
