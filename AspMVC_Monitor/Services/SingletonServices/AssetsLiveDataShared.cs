using AspMVC_Monitor.gRPC;
using AspMVC_Monitor.Models;
using AssetMonitorSharedGRPC.Helpers;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.SingletonServices
{
    public class AssetsLiveDataShared : IAssetsLiveDataShared
    {
        public const int TcpPort = 9561; // ToDo make configurable in DB

        public List<AssetLiveData> AssetsData;

        private readonly ILogger<AssetsLiveDataShared> _logger;

        private bool _assetsDataConfigurationLoaded = false;

        public AssetsLiveDataShared(ILogger<AssetsLiveDataShared> logger)
        {
            this._logger = logger;
        }

        public async Task UpdateAssetsLiveData()
        {
            if (!_assetsDataConfigurationLoaded)
            {
                _logger.LogInformation($"New Assets Data configuration request");
                await UpdateAssetsLiveDataConfiguration(false);
                return;
            }

            try
            {
                var client = GrpcHelper<IAssetMonitorDataService>.CreateSecureClient(IPAddress.Loopback.ToString(), TcpPort);

                var reply = await client.GetAssetsData(
                    new AssetsDataRequest { Init = 1 });

                if (reply.ConfigurationUpdate)
                {
                    _logger.LogInformation($"New Assets Data configuration on server side");
                    await UpdateAssetsLiveDataConfiguration(false);
                    return;
                }

                foreach (var asset in reply.AssetsData)
                {
                    var assetData = AssetsData.FirstOrDefault(ad=>ad.Id == asset.AssetId);
                    foreach (var tag in asset.Tags)
                    {
                        var liveTag = assetData.Tags[tag.TagId];
                        liveTag.Value = ByteConverterHelper.ByteArrayToObject(tag.Value);
                        liveTag.InAlarm = tag.InAlarm;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve Assets Data from Asset Monitor service");
                _logger.LogDebug($"Exception: {ex.Message}");
            }
        }

        private async Task UpdateAssetsLiveDataConfiguration(bool newConfigurationLoaded)
        {
            AssetsData = new List<AssetLiveData>();
            try
            {
                var client = GrpcHelper<IAssetMonitorDataService>.CreateSecureClient(IPAddress.Loopback.ToString(), TcpPort);

                if (newConfigurationLoaded)
                {
                    _assetsDataConfigurationLoaded = true;
                    _logger.LogInformation($"New Assets Data configuration loaded. Sending confirmation to server side");
                    _ = await client.GetAssetsDataConfiguration(
                        new AssetsDataConfigurationRequest { NewConfigurationLoaded = newConfigurationLoaded });
                    return;
                }

                _logger.LogInformation($"Loading new Assets Data configuration");

                var reply = await client.GetAssetsDataConfiguration(
                    new AssetsDataConfigurationRequest { NewConfigurationLoaded = newConfigurationLoaded });

                foreach (var asset in reply.AssetsData)
                {
                    var liveTags = new Dictionary<int, TagLiveValue>();
                    foreach (var tag in asset.Tags)
                    {
                        liveTags.Add(tag.TagId, new TagLiveValue()
                        {
                            Id = tag.TagId,
                            Tagname = tag.Tagname,
                            RangeMax = tag.RangeMax,
                            RangeMin = tag.RangeMin
                        });
                    }
                    AssetsData.Add(new AssetLiveData(
                        id: asset.AssetId,
                        name: asset.Name,
                        ipAddress: asset.IPAddress,
                        tags: liveTags
                        ));
                }

                await UpdateAssetsLiveDataConfiguration(true);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Cannot retrieve Assets Data from Asset Monitor service");
                _logger.LogDebug($"Exception: {ex.Message}");
            }
        }
    }
}
