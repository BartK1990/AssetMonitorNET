using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsPerformanceDataSharedService : AssetsSharedServiceBase<AssetPerformanceData>, IAssetsPerformanceDataSharedService
    {
        private readonly ILogger<AssetsPerformanceDataSharedService> _logger;

        public AssetsPerformanceDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsPerformanceDataSharedService> logger) : base(scopeFactory)
        {
            this._logger = logger;
        }

        protected override void UpdateAssetsList(IAssetMonitorRepository repository)
        {
            AssetsData = new List<AssetPerformanceData>();
            var assets = repository.GetAgentAssetsWithPropertiesAndTagSetAsync().Result.ToList();
            foreach (var a in assets)
            {
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

                if (int.TryParse(a.AssetPropertyValues
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
                    _logger.LogError($"Wrong Tcp port for Asset: {a.Name} (Id: {a.Id})");
                }
            }
        }
    }
}
