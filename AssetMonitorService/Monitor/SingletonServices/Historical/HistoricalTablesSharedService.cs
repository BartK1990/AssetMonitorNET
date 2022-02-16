using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorHistoryDataAccess.Models;
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

        public async Task DatabaseStructureUpdate()
        {
            using var scope = _scopeFactory.CreateScope();
            var assetRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            var historyDynamicRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();
            var historyRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryRepository>();

            var generatedHistoricalTables = new List<string>();

            var assets = await assetRepo.GetAllAssetsAsync();

            foreach (var asset in assets)
            {
                var columnsConfigs = new List<TableColumnConfig>();
                var tagHistInfos = new List<TagHistoricalInfo>();

                // Initial columns - always present - ping state
                tagHistInfos.AddRange(TagHistoricalPing.Tags);

                var agentTags = (await assetRepo.GetAgentTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                tagHistInfos.AddRange(agentTags.Select(a => new TagHistoricalInfo() 
                { Tagname = $"Agent.{a.Tagname}", ValueDataTypeId = a.ValueDataTypeId, HistoricalTagConfigs = a.HistorizationTagConfigs, IsNull = true }));

                var snmpTags = (await assetRepo.GetSnmpTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                tagHistInfos.AddRange(snmpTags.Select(a => new TagHistoricalInfo() 
                { Tagname = $"Snmp.{a.Tagname}", ValueDataTypeId = a.ValueDataTypeId, HistoricalTagConfigs = a.HistorizationTagConfigs, IsNull = true }));

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
                var newTableName = @$"TenMinData_{asset.Id}";
                await historyDynamicRepo.CreateOrUpdateTimeSeriesTable(newTableName, columnsConfigs);
                generatedHistoricalTables.Add(newTableName);
            }
            // Update Historical data table containing names of created tables
            var historicalTables = (await historyRepo.GetAllTablesAsync()).ToList();
            foreach (var tableName in generatedHistoricalTables)
            {
                if (!historicalTables.Select(s => s.Name).Contains(tableName))
                {
                    historyRepo.Add(new HistoricalDataTable() { Name = tableName });
                }
            }
            await historyRepo.SaveAllAsync();
        }
    }
}
