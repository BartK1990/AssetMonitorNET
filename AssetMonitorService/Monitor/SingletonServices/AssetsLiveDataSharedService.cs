using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Alarm;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
using AssetMonitorService.Monitor.SingletonServices.Alarm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices
{
    public class AssetsLiveDataSharedService : AssetsSharedServiceBase<AssetsLiveDataSharedService, AssetLiveData>,
        IAssetsLiveDataSharedService
    {
        private readonly IAssetsIcmpSharedService _assetsIcmpShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;
        private readonly IAssetsAlarmDataSharedService _assetsAlarmDataShared;

        public AssetsLiveDataSharedService(
            IAssetsIcmpSharedService assetsIcmpShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IAssetsAlarmDataSharedService assetsAlarmDataShared,
            IServiceScopeFactory scopeFactory,
            ILogger<AssetsLiveDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
            this._assetsIcmpShared = assetsIcmpShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
            this._assetsAlarmDataShared = assetsAlarmDataShared;
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                var tagsConfigured = (await repository.GetTagsIncludeAlarmByAssetIdAsync(asset.Id)).ToList();
                if (!tagsConfigured.Any())
                {
                    _logger.LogWarning($"{this.GetType().Name} - No configured tags for asset Id: [{asset.Id}]");
                    continue;
                }

                var tags = new List<TagLive>();

                foreach (var tagConfig in tagsConfigured)
                {
                    // Alarms
                    List<TagAlarmValue> TagAlarmValues = null;
                    var alarmsForTags = _assetsAlarmDataShared.AssetsData.FirstOrDefault(a => a.Id == asset.Id)?.Data;
                    if(alarmsForTags?.Any() ?? false)
                    {
                        var alarms = alarmsForTags.Keys.Where(t => t.Tagname == tagConfig.Tagname);
                        if (alarms?.Any() ?? false)
                        {
                            TagAlarmValues = alarms.ToList();
                        }
                    }

                    // Range
                    var tagRange = await repository.GetAssetTagRangeByAssetIdTagIdAsync(assetId: asset.Id, tagId: tagConfig.Id);

                    // ICMP
                    var icmpTags = _assetsIcmpShared.AssetsData?.FirstOrDefault(a => a.Id == asset.Id)?.Data
                        ?.ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                    if(AddAssetLiveData(tags, tagConfig, TagAlarmValues, icmpTags, tagRange))
                    {
                        continue;
                    }

                    // Performance
                    var performanceTags = _assetsPerformanceDataShared.AssetsData?.FirstOrDefault(a => a.Id == asset.Id)?.Data
                        ?.ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                    if (AddAssetLiveData(tags, tagConfig, TagAlarmValues, performanceTags, tagRange))
                    {
                        continue;
                    }

                    // SNMP
                    var snmpTags = _assetsSnmpDataShared.AssetsData?.FirstOrDefault(a => a.Id == asset.Id)?.Data
                        ?.ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                    if (AddAssetLiveData(tags, tagConfig, TagAlarmValues, snmpTags, tagRange))
                    {
                        continue;
                    }

                }
                this.AssetsData.Add(new AssetLiveData(tags, asset.Id, asset.Name));
            }
        }

#nullable enable
        private bool AddAssetLiveData(List<TagLive> tags, Tag tagConfig, List<TagAlarmValue>? TagAlarmValues, Dictionary<TagConfigBase, TagValue> sharedTags, AssetTagRange? tagRange)
        {
            bool added = false;
            if (!sharedTags?.Any() ?? true)
            {
                return added;
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var tagKey = sharedTags.Keys.FirstOrDefault(t => t.Id == tagConfig.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (tagKey != null)
            {
                double? rangeMin = null;
                double? rangeMax = null;
                if (tagRange != null)
                {
                    rangeMax = tagRange.RangeMax;
                    rangeMin = tagRange.RangeMin;
                }
                var tagLive = new TagLive(tagConfig, sharedTags[tagKey], TagAlarmValues, rangeMin, rangeMax);
                tags.Add(tagLive);
                added = true;
            }
            return added;
        }
#nullable disable
    }
}
