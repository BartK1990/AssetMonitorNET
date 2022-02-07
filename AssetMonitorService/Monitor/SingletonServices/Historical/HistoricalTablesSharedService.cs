using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Data.Repositories.Historical;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class HistoricalTablesSharedService : IHistoricalTablesSharedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HistoricalTablesSharedService> _logger;

        public HistoricalTablesSharedService(IServiceScopeFactory scopeFactory,
            ILogger<HistoricalTablesSharedService> logger)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
        }

        public async Task DatabaseUpdate()
        {
            using var scope = _scopeFactory.CreateScope();
            var assetRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            var historyRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();

            var assets = await assetRepo.GetAllAssetsAsync();

            foreach (var asset in assets)
            {
                var columnsConfigs = new List<TableColumnConfig>();
                var tagHistInfos = new List<TagHistoricalInfo>();

                // Initial columns - always present - ping state
                tagHistInfos.Add(new TagHistoricalInfo()
                {
                    Tagname = "Ping.State",
                    ValueDataTypeId = (int)TagDataTypeEnum.Boolean,
                    HistoricalTagConfigs = new List<HistoricalTagConfig>()
                    {
                        new HistoricalTagConfig() { HistorizationTypeId = (int)HistoricalTypeEnum.Last }
                    },
                    IsNull = true
                });
                tagHistInfos.Add(new TagHistoricalInfo()
                {
                    Tagname = "Ping.ResponseTime",
                    ValueDataTypeId = (int)TagDataTypeEnum.Long,
                    HistoricalTagConfigs = new List<HistoricalTagConfig>()
                    {
                        new HistoricalTagConfig() { HistorizationTypeId = (int)HistoricalTypeEnum.Average }
                    },
                    IsNull = true
                });

                var agentTags = (await assetRepo.GetAgentTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                tagHistInfos.AddRange(agentTags.Select(a => new TagHistoricalInfo() { Tagname = $"Agent.{a.Tagname}", ValueDataTypeId = a.ValueDataTypeId, HistoricalTagConfigs = a.HistorizationTagConfigs, IsNull = true }));

                var snmpTags = (await assetRepo.GetSnmpTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                tagHistInfos.AddRange(snmpTags.Select(a => new TagHistoricalInfo() { Tagname = $"Snmp.{a.Tagname}", ValueDataTypeId = a.ValueDataTypeId, HistoricalTagConfigs = a.HistorizationTagConfigs, IsNull = true }));

                foreach (var tagHistInfo in tagHistInfos)
                {
                    var type = (TagDataTypeEnum)tagHistInfo.ValueDataTypeId;
                    foreach (var hType in tagHistInfo.HistoricalTagConfigs)
                    {
                        string sqlTypeName = "";
                        switch (type)
                        {
                            case TagDataTypeEnum.Boolean:
                                sqlTypeName = @"BIT";
                                break;
                            case TagDataTypeEnum.Integer:
                                sqlTypeName = @"INT";
                                break;
                            case TagDataTypeEnum.Long:
                                sqlTypeName = @"BIGINT";
                                break;
                            case TagDataTypeEnum.Float:
                                sqlTypeName = @"FLOAT";
                                break;
                            case TagDataTypeEnum.Double:
                                sqlTypeName = @"FLOAT";
                                break;
                            case TagDataTypeEnum.String:
                                sqlTypeName = @"NVARCHAR(20)";
                                break;
                            default:
                                break;
                        }
                        if (!string.IsNullOrEmpty(sqlTypeName))
                        {
                            columnsConfigs.Add(new TableColumnConfig()
                            {
                                Name = $"{tagHistInfo.Tagname}.{(HistoricalTypeEnum)hType.HistorizationTypeId}",
                                Type = sqlTypeName,
                                IsNull = tagHistInfo.IsNull
                            });
                        }
                    }
                }
                await historyRepo.CreateOrUpdateTimeSeriesTable(@$"TenMinData_{asset.Id}", columnsConfigs);
            }
        }

        private class TagHistoricalInfo
        {
            public string Tagname;
            public int ValueDataTypeId;
            public ICollection<HistoricalTagConfig> HistoricalTagConfigs;
            public bool IsNull;
        }
    }
}
