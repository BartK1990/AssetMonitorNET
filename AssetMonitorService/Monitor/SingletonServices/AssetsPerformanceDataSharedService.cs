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
                var assetProperties = await repository.GetAssetPropertiesByIdAsync(a.Id);

                // Only for localhost no TCP port is required
                if(a.IpAddress == IPAddress.Loopback.ToString())
                {
                    AssetsData.Add(new AssetPerformanceData(a.AgentTagSet.AgentTag)
                    {
                        Id = a.Id,
                        IpAddress = a.IpAddress,
                        TcpPort = null,
                    });
                    continue;
                }

                if (int.TryParse(assetProperties.AssetPropertyValues
                    .Where(p=>p.AssetPropertyId == (int)AssetPropertyNameEnum.AgentTcpPort)
                    .FirstOrDefault().Value
                    , out var tcpPort))
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
