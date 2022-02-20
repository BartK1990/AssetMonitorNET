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
                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(a.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                var snmpUdpPort = assetProperties?
                    .FirstOrDefault(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpUdpPort)?.Value ?? null;
                if (snmpUdpPort == null)
                {
                    snmpUdpPort = "161";
                    _logger.LogError($"No SNMP UDP port specified for Asset (Id): {a.Id} | default [{snmpUdpPort}] used");
                }
                if (!int.TryParse(snmpUdpPort, out var udpPort))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpUdpPort]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                var snmpTimeout = assetProperties?
                    .FirstOrDefault(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpTimeout)?.Value ?? null;
                if (snmpTimeout == null)
                {
                    snmpTimeout = "8000";
                    _logger.LogError($"No SNMP timeout specified for Asset (Id): {a.Id} | default [{snmpTimeout}] used");
                }
                if (!int.TryParse(snmpTimeout, out var timeout))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpTimeout]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                var snmpRetries = assetProperties?
                    .FirstOrDefault(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpRetries)?.Value ?? null;
                if (snmpRetries == null)
                {
                    snmpRetries = "1";
                    _logger.LogError($"No SNMP retries specified for Asset (Id): {a.Id} | default [{snmpRetries}] used");
                }
                if (!int.TryParse(snmpRetries, out var retries))
                {
                    _logger.LogError($"Wrong {AssetPropertyNameDictionary.Dict[AssetPropertyNameEnum.SnmpRetries]} for Asset: {a.Name} (Id: {a.Id})");
                    return;
                }

                var community = assetProperties?
                    .FirstOrDefault(p => p.AssetPropertyId == (int)AssetPropertyNameEnum.SnmpCommunity)?.Value ?? null;
                if (community == null)
                {
                    community = "public";
                    _logger.LogError($"No SNMP community specified for Asset (Id): {a.Id} | default [{community}] used");
                }
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
