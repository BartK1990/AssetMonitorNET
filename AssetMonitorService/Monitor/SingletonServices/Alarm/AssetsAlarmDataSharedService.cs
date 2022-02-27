using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using AssetMonitorService.Monitor.Model.Alarm;
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
                var alarmTags = new Dictionary<TagValue, AlarmTagInfo>();

                var pingShared = _assetsPingShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var performanceShared = _assetsPerformanceDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var snmpShared = _assetsSnmpDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var tags = (await repository.GetTagsWithAlarmByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in tags)
                {
                    if (tag.AlarmTagConfigs?.Any() ?? false)
                    {
                        if (tag.TagCommunicationRel.IcmpTagId != null)
                        {
                            var icmpTag = new TagIcmp() { Id = tag.Id };
                            if (pingShared.Data.ContainsKey(icmpTag))
                            {
                                foreach (var alarmConfigs in tag.AlarmTagConfigs)
                                {
                                    alarmTags.Add(pingShared.Data[icmpTag],
                                        new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId));
                                }
                            }
                        }

                        // ToDo add performance and SNMP
                    }
                }

                // Add all tags to Alarm data list
                this.AssetsData.Add(new AssetAlarmData(alarmTags, asset.Id));
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
