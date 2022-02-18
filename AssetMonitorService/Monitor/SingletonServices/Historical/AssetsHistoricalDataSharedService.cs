﻿using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model;
using AssetMonitorService.Monitor.Model.Historical;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class AssetsHistoricalDataSharedService : AssetsSharedServiceBase<AssetsHistoricalDataSharedService, AssetHistoricalData>,
        IAssetsHistoricalDataSharedService
    {
        public const int NumberOfSamplesInHistoricalBuffer = 60; // ToDo add configuration how many samples there should be (based on live data frequency + how offen it is saved to DB)
        private readonly IAssetsPingSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;

        public AssetsHistoricalDataSharedService(
            IAssetsPingSharedService assetsPingShared,
            IAssetsPerformanceDataSharedService assetsPerformanceDataShared,
            IAssetsSnmpDataSharedService assetsSnmpDataShared,
            IServiceScopeFactory scopeFactory,
            ILogger<AssetsHistoricalDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
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
                var tags = new List<TagValue>();

                // Ping
                var pingShared = _assetsPingShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                foreach (var tag in HistoricalTagsPing.Tags)
                {
                    switch (tag.Tagname)
                    {
                        case "PingState":
                            tags.Add(pingShared.PingStateValue);
                            break;
                        case "PingResponseTime":
                            tags.Add(pingShared.PingResponseTimeValue);
                            break;
                        default:
                            break;
                    }
                }

                // Performance Data
                var performanceShared = _assetsPerformanceDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var agentTags = (await repository.GetAgentTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in agentTags)
                {
                    if (tag.HistorizationTagConfigs?.Any() ?? false)
                    {
                        if (performanceShared.Data.ContainsKey(tag))
                        {
                            tags.Add(performanceShared.Data[tag]);
                        }
                    }
                }

                // SNMP Data
                var snmpShared = _assetsSnmpDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var snmpTags = (await repository.GetSnmpTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in snmpTags)
                {
                    if (tag.HistorizationTagConfigs?.Any() ?? false)
                    {
                        if (snmpShared.Data.ContainsKey(tag))
                        {
                            tags.Add(snmpShared.Data[tag]);
                        }
                    }
                }

                // Add all tags to Historical data list
                this.AssetsData.Add(new AssetHistoricalData(tags, NumberOfSamplesInHistoricalBuffer, asset.Id));
            }
        }

        public void UpdateAssetsHistoricalValues()
        {
            foreach (var asset in this.AssetsData)
            {
                asset.UpdateData();
            }
        }
    }
}