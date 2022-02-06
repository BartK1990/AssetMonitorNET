using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsSnmpDataSharedService : AssetsSharedServiceBase<AssetsSnmpDataSharedService, AssetSnmpData>, 
        IAssetsSnmpDataSharedService
    {
        public AssetsSnmpDataSharedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsSnmpDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetSnmpAssetsWithTagSetAsync()).ToList();
            foreach (var a in assets)
            {
                var assetProperties = await repository.GetAssetPropertiesByIdAsync(a.Id);

                if (!int.TryParse(assetProperties.AssetPropertyValues
                    .Where(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpUdpPort)
                    .FirstOrDefault().Value
                    , out var udpPort))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpUdpPort]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                if (!int.TryParse(assetProperties.AssetPropertyValues
                    .Where(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpTimeout)
                    .FirstOrDefault().Value
                    , out var timeout))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpTimeout]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                if (!int.TryParse(assetProperties.AssetPropertyValues
                    .Where(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpRetries)
                    .FirstOrDefault().Value
                    , out var retries))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpRetries]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                var community = assetProperties.AssetPropertyValues
                    .Where(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpCommunity)
                    .FirstOrDefault().Value;
                if (string.IsNullOrEmpty(community))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpCommunity]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                AssetsData.Add(new AssetSnmpData(a.SnmpTagSet.SnmpTag)
                {
                    Id = a.Id,
                    IpAddress = a.IpAddress,
                    Version = (SnmpVersionEnum)a.SnmpTagSet.Version.Id,
                    Community = community,
                    UdpPort = udpPort,
                    Timeout = timeout,
                    Retries = retries
                });

            }
        }
    }
}
