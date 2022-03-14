using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Alarm;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Alarm
{
    public class AssetsAlarmDataSharedService : AssetsSharedServiceBase<AssetsAlarmDataSharedService, AssetAlarmData>,
        IAssetsAlarmDataSharedService
    {
        private readonly IAssetsIcmpSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;

        public AssetsAlarmDataSharedService(
            IAssetsIcmpSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IServiceScopeFactory scopeFactory,
            ILogger<AssetsAlarmDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
            this._assetsPingShared = assetsPingShared;
            this._assetsPerformanceDataShared = assetsPerformanceDataShared;
            this._assetsSnmpDataShared = assetsSnmpDataShared;
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                var alarmTags = new Dictionary<AlarmTagInfo, TagValue>();

                var pingSharedData = _assetsPingShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                var performanceSharedData = _assetsPerformanceDataShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                var snmpSharedData = _assetsSnmpDataShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;

                var tags = (await repository.GetTagsWithAlarmByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in tags)
                {
                    if (tag.AlarmTagConfigs?.Any() ?? false)
                    {
                        // ICMP tags
                        if (tag.TagCommunicationRel.IcmpTagId != null)
                        {
                            AddAlarmTag(alarmTags, pingSharedData, tag);
                            continue;
                        }

                        // Agent tags
                        if (tag.TagCommunicationRel.AgentTagId != null)
                        {
                            AddAlarmTag(alarmTags, performanceSharedData, tag);
                            continue;
                        }

                        // SNMP tags
                        if (tag.TagCommunicationRel.SnmpTagId != null)
                        {
                            AddAlarmTag(alarmTags, snmpSharedData, tag);
                            continue;
                        }
                    }
                }

                // Add all tags to Alarm data list
                this.AssetsData.Add(new AssetAlarmData(alarmTags, asset.Id));
            }
        }

        private static void AddAlarmTag(Dictionary<AlarmTagInfo, TagValue> alarmTags, Dictionary<TagConfigBase, TagValue> sharedData, Tag tag)
        {
            if(sharedData == null)
            {
                return;
            }

            var key = sharedData.Keys.FirstOrDefault(k => k.Id == tag.Id);
            if (key != null)
            {
                foreach (var alarmConfigs in tag.AlarmTagConfigs)
                {
                    alarmTags.Add(
                        new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId),
                        sharedData[key]);
                }
            }
        }

        public void UpdateAssetsAlarmValues()
        {
            foreach (var asset in this.AssetsData)
            {
                asset.UpdateData();
            }
        }
    }
}
