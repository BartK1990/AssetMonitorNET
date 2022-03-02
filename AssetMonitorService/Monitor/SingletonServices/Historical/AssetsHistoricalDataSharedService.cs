using AssetMonitorDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Historical;
using AssetMonitorService.Monitor.Model.Live;
using AssetMonitorService.Monitor.Model.TagConfig;
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
        private readonly IAssetsIcmpSharedService _assetsPingShared;
        private readonly IAssetsPerformanceDataSharedService _assetsPerformanceDataShared;
        private readonly IAssetsSnmpDataSharedService _assetsSnmpDataShared;

        public AssetsHistoricalDataSharedService(
            IAssetsIcmpSharedService assetsPingShared,
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
                var historicalTags = new List<TagValue>();

                var pingSharedData = _assetsPingShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                var performanceSharedData = _assetsPerformanceDataShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;
                var snmpSharedData = _assetsSnmpDataShared.AssetsData?.Where(a => a.Id == asset.Id).FirstOrDefault()?.Data
                    .ToDictionary(k => (TagConfigBase)k.Key, v => v.Value) ?? null;

                var tags = (await repository.GetTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                foreach (var tag in tags)
                {
                    if (tag.HistoricalTagConfigs?.Any() ?? false)
                    {
                        // ICMP tags
                        if (tag.TagCommunicationRel.IcmpTagId != null)
                        {
                            AddHistoricalTag(historicalTags, pingSharedData, tag);
                            continue;
                        }

                        // Agent tags
                        if (tag.TagCommunicationRel.AgentTagId != null)
                        {
                            AddHistoricalTag(historicalTags, performanceSharedData, tag);
                            continue;
                        }

                        // SNMP tags
                        if (tag.TagCommunicationRel.SnmpTagId != null)
                        {
                            AddHistoricalTag(historicalTags, snmpSharedData, tag);
                            continue;
                        }
                    }
                }

                // Add all tags to Historical data list
                this.AssetsData.Add(new AssetHistoricalData(historicalTags, NumberOfSamplesInHistoricalBuffer, asset.Id));
            }
        }

        private static void AddHistoricalTag(List<TagValue> historicalTags, Dictionary<TagConfigBase, TagValue> sharedData, Tag tag)
        {
            if (sharedData == null)
            {
                return;
            }

            var key = sharedData.Keys.FirstOrDefault(k => k.Id == tag.Id);
            if (key != null)
            {
                foreach (var historicalConfigs in tag.HistoricalTagConfigs)
                {
                    historicalTags.Add(sharedData[key]);
                }
            }
        }

        public void UpdateAssetsHistoricalValues()
        {
            foreach (var asset in this.AssetsData)
            {
                asset.UpdateData();
            }
        }

        public async Task<bool> UpdateAssetActualSnmpValuesByIdAsync(int assetId)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();

            var snmpAssetValues = (await repository.GetSnmpAssetValuesByAssetIdAsync(assetId)).ToList();
            var snmpTags = (await repository.GetSnmpTagsByAssetIdAsync(assetId)).ToList();

            var assetData = _assetsSnmpDataShared.AssetsData.FirstOrDefault(ad => ad.Id == assetId);

            foreach (var tag in snmpTags)
            {
                var snmpTag = new TagSnmp(tag);
                if (!assetData.Data.ContainsKey(snmpTag))
                {
                    continue;
                }
                var snmpTagValue = assetData.Data[snmpTag].Value;
                if(snmpTagValue == null)
                {
                    continue;
                }
                var valueToUpdate = snmpAssetValues.FirstOrDefault(sa => sa.SnmpTagId == tag.Id);
                if (valueToUpdate == null)
                {
                    repository.Add(new SnmpAssetValue() { AssetId = assetId, SnmpTagId = tag.Id, Value = snmpTagValue.ToString() });
                    continue;
                }
                valueToUpdate.Value = snmpTagValue.ToString();
                repository.Update(valueToUpdate);
            }
            return await repository.SaveAllAsync();
        }
    }
}
