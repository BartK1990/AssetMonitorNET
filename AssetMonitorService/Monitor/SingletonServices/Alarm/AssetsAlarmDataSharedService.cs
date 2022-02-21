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
                var tags = new Dictionary<TagValue, AlarmTagInfo>();

                // Ping
                var pingShared = _assetsPingShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var pingTags = (await repository.GetIcmpTagsWithAlarmByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in pingTags)
                {
                    if (tag.AlarmTagConfigs?.Any() ?? false)
                    {
                        foreach (var alarmConfigs in tag.AlarmTagConfigs)
                        {
                            switch (tag.Tagname)
                            {
                                case "PingState":
                                    tags.Add(pingShared.PingStateValue, 
                                        new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId));
                                    break;
                                case "PingResponseTime":
                                    tags.Add(pingShared.PingResponseTimeValue,
                                        new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId));
                                    break;
                            }
                        }
                    }
                }

                // Performance Data
                var performanceShared = _assetsPerformanceDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var agentTags = (await repository.GetAgentTagsWithAlarmByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in agentTags)
                {
                    if (tag.AlarmTagConfigs?.Any() ?? false)
                    {
                        if (performanceShared.Data.ContainsKey(tag))
                        {
                            foreach (var alarmConfigs in tag.AlarmTagConfigs)
                            {
                                tags.Add(performanceShared.Data[tag],
                                    new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId));
                            }
                        }
                    }
                }

                // SNMP Data
                var snmpShared = _assetsSnmpDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var snmpTags = (await repository.GetSnmpTagsWithAlarmByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in snmpTags)
                {
                    if (tag.AlarmTagConfigs?.Any() ?? false)
                    {
                        if (snmpShared.Data.ContainsKey(tag))
                        {
                            foreach (var alarmConfigs in tag.AlarmTagConfigs)
                            {
                                tags.Add(snmpShared.Data[tag],
                                    new AlarmTagInfo(alarmConfigs.Value, alarmConfigs.ActivationTime, alarmConfigs.Description, (AlarmTypeEnum)alarmConfigs.AlarmTypeId));
                            }
                        }
                    }
                }

                // Add all tags to Alarm data list
                this.AssetsData.Add(new AssetAlarmData(tags, asset.Id));
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
