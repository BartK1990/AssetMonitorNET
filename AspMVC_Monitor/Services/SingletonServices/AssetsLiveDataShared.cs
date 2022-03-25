using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.gRPC;
using AspMVC_Monitor.Models;
using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorSharedGRPC.Helpers;
using AssetMonitorSharedGRPC.Server;
using Microsoft.Extensions.DependencyInjection;
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

        private readonly ILogger<AssetsLiveDataShared> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public List<AssetLiveData> AssetsData { get; private set; }
        private bool _assetsDataConfigurationLoaded = false;

        public AssetsLiveDataShared(ILogger<AssetsLiveDataShared> logger,
            IServiceScopeFactory scopeFactory)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
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

                _logger.LogInformation($"Reading tag values for Assets from server side");
                foreach (var asset in reply.AssetsData)
                {
                    var assetData = AssetsData.FirstOrDefault(ad=>ad.Id == asset.AssetId);
                    if(assetData == null)
                    {
                        _logger.LogError($"Wrong configuration for Asset Id: [{asset.AssetId}]. Cannot read values from server side");
                        continue;
                    }
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
            try
            {
                var client = GrpcHelper<IAssetMonitorDataService>.CreateSecureClient(IPAddress.Loopback.ToString(), TcpPort);

                if (newConfigurationLoaded)
                {
                    _assetsDataConfigurationLoaded = true;
                    _logger.LogInformation($"New Assets Data configuration loaded. Sending confirmation to server side");
                    _ = await client.GetAssetsDataConfiguration(
                        new AssetsDataConfigurationRequest { NewConfigurationLoaded = newConfigurationLoaded });
                    
                    await UpdateAssetsSharedTagSets();
                    return;
                }

                AssetsData = new List<AssetLiveData>();
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
                            DataType = (TagDataTypeEnum)tag.DataType,
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

        public async Task UpdateAssetsSharedTagSets()
        {
            try
            {
                if (!_assetsDataConfigurationLoaded)
                {
                    return;
                }
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

                foreach (var asset in AssetsData)
                {
                    asset.TagsIdForSharedTagSets = new Dictionary<int, IDictionary<int, int>>();
                }

                var tagSharedSets = (await repository.GetAllTagSharedSetsAsync());
                foreach (var set in tagSharedSets)
                {
                    var tagsShared = (await repository.GetTagSharedBySetIdAsync(set.Id));
                    foreach (var asset in AssetsData)
                    {
                        var SharedTagsIds = new Dictionary<int, int>();
                        foreach (var tag in asset.Tags)
                        {
                            var sharedTag = tagsShared.FirstOrDefault(n => n.Tagname == tag.Value.Tagname);
                            if (sharedTag != null)
                            {
                                SharedTagsIds.Add(sharedTag.Id, tag.Key);
                            }
                        }
                        asset.TagsIdForSharedTagSets.Add(set.Id, SharedTagsIds);
                    }
                }
                _logger.LogInformation($"Shared tag sets loaded");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error during loading shared tags for Assets");
                _logger.LogDebug($"Exception: {ex.Message}");
            }
        }
    }
}
