using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public static class HistoricalTagsPing
    {
        public static List<HistoricalTagInfo> Tags = new List<HistoricalTagInfo>()
        {
            new HistoricalTagInfo()
            {
                Tagname = "PingState",
                ColumnNameSuffix = "ICMP",
                ValueDataTypeId = (int)TagDataTypeEnum.Boolean,
                HistoricalTagConfigs = new List<HistoricalTagConfig>()
                {
                    new HistoricalTagConfig() { HistorizationTypeId = (int)HistoricalTypeEnum.Last }
                },
                IsNull = true
            },
            new HistoricalTagInfo()
            {
                Tagname = "PingResponseTime",
                ColumnNameSuffix = "ICMP",
                ValueDataTypeId = (int)TagDataTypeEnum.Long,
                HistoricalTagConfigs = new List<HistoricalTagConfig>()
                {
                    new HistoricalTagConfig() { HistorizationTypeId = (int)HistoricalTypeEnum.Average }
                },
                IsNull = true
            }
        };
    }
}
