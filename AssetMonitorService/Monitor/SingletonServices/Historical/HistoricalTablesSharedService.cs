using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorHistoryDataAccess.Models;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Data.Repositories.Historical;
using AssetMonitorService.Monitor.Model.Historical;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public class HistoricalTablesSharedService : IHistoricalTablesSharedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<HistoricalTablesSharedService> _logger;
        private readonly IAssetsHistoricalDataSharedService _assetsHistoricalDataShared;

        private List<AssetHistoricalDataColumnRelation> _assetDataColumnRelations;

        public HistoricalTablesSharedService(IServiceScopeFactory scopeFactory,
            ILogger<HistoricalTablesSharedService> logger,
            IAssetsHistoricalDataSharedService assetsHistoricalDataShared)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
            this._assetsHistoricalDataShared = assetsHistoricalDataShared;
        }

        public async Task DatabaseStructureUpdateAsync()
        {
            _assetDataColumnRelations = new List<AssetHistoricalDataColumnRelation>();

            using var scope = _scopeFactory.CreateScope();
            var assetRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorRepository>();
            var historyDynamicRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();
            var historyRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryRepository>();

            var generatedHistoricalTables = new List<string>();

            var assets = _assetsHistoricalDataShared.AssetsData;

            foreach (var asset in assets)
            {
                // For each asset in corresponding Historical Shared service create instance
                var assetDataColumnRelation = new AssetHistoricalDataColumnRelation(asset.Id);
                
                var columnsConfigs = new List<TableColumnConfig>();
                var tagHistInfos = new List<HistoricalTagInfo>();

                var tags = (await assetRepo.GetTagsWithHistoricalByAssetIdAsync(asset.Id)).ToList();
                tagHistInfos.AddRange(tags.Select(a => new HistoricalTagInfo()
                { Tagname = a.Tagname, ValueDataTypeId = a.ValueDataTypeId, HistoricalTagConfigs = a.HistoricalTagConfigs, IsNull = true }));

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
                            var historicalType = (HistoricalTypeEnum)hType.HistorizationTypeId;
                            var columnName = $"{tagHistInfo.Tagname}.{historicalType}";
                            columnsConfigs.Add(new TableColumnConfig()
                            {
                                Name = columnName,
                                Type = sqlTypeName,
                                IsNull = tagHistInfo.IsNull
                            });
                            var tagHistoricalValue = asset.Data.Values.FirstOrDefault(v => v.Tagname == tagHistInfo.Tagname);
                            if(tagHistoricalValue!= null)
                            {
                                assetDataColumnRelation.Data.Add(new HistoricalColumnInfo(columnName, historicalType) , tagHistoricalValue);
                            }
                        }
                    }
                }
                var newTableName = @$"TenMinData_{asset.Id}";

                await historyDynamicRepo.CreateOrUpdateTimeSeriesTableAsync(newTableName, columnsConfigs);

                assetDataColumnRelation.TableName = newTableName;
                _assetDataColumnRelations.Add(assetDataColumnRelation);

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

        public async Task InsertTimedDataForAllAssetsAsync(string timestamp)
        {
            if (!_assetDataColumnRelations?.Any() ?? true)
            {
                return;
            }

            if(!DateTime.TryParse(timestamp, out _))
            {
                throw new ArgumentException("Wrong timestamp format provided for method");
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            using var scope = _scopeFactory.CreateScope();
            var historyDynamicRepo = scope.ServiceProvider.GetRequiredService<IAssetMonitorHistoryDapperRepository>();

            foreach (var asset in _assetDataColumnRelations)
            {
                var columnsValues = new List<TableColumnValue>();
                foreach (var keyValue in asset.Data)
                {
                    var columnValue = new TableColumnValue() { Name = keyValue.Key.Name };
#nullable enable
                    object? value = null;
                    switch (keyValue.Key.Type)
                    {
                        case HistoricalTypeEnum.Maximum:
                            value = keyValue.Value.ValueMax;
                            break;
                        case HistoricalTypeEnum.Average:
                            value = keyValue.Value.ValueAvg;
                            break;
                        case HistoricalTypeEnum.Minimum:
                            value = keyValue.Value.ValueMin;
                            break;
                        default:
                            value = keyValue.Value.ValueLast;
                            break;
                    }
#nullable disable
                    if (value == null)
                    {
                        columnValue.Value = "NULL";
                    }
                    else
                    {
                        columnValue.Value = Convert.ToString(value, CultureInfo.InvariantCulture);
                    }
                    columnsValues.Add(columnValue);
                }

                await historyDynamicRepo.InsertToTimeSeriesTableAsync(asset.TableName, columnsValues, timestamp);
            }
        }
    }
}
