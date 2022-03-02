using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
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
            var assets = (await repository.GetSnmpAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                var tags = (await repository.GetSnmpTagsByAssetIdAsync(asset.Id)).ToList();

                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(asset.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                // Properties
                int udpPort = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.SnmpUdpPort, int.Parse, 161);
                int timeout = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.SnmpTimeout, int.Parse, 6000);
                int retries = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.SnmpRetries, int.Parse, 1);
                string community = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.SnmpCommunity, FuncString, "public");

                int version = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.SnmpVersion, int.Parse, 2);
                if(!Enum.IsDefined(typeof(SnmpVersionEnum), version))
                {
                    version = (int)SnmpVersionEnum.V2;
                    _logger.LogError($"Wrong SNMP Version for Asset: {asset.Name} (Id: {asset.Id}) | Default [{version}] used");
                }

                var snmpTags = tags
                    .Select(tag => new TagSnmp(tag)).ToList();
                
                AssetsData.Add(new AssetSnmpData(snmpTags)
                {
                    Id = asset.Id,
                    IpAddress = asset.IpAddress,
                    Version = (SnmpVersionEnum)version,
                    Community = community,
                    UdpPort = udpPort,
                    Timeout = timeout,
                    Retries = retries
                });

            }
        }
    }
}
