using AssetMonitorDataAccess.Models;
using AssetMonitorDataAccess.Models.Enums;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.SingletonServices.Historical
{
    public static class TagHistoricalPing
    {
        public static List<TagHistoricalInfo> Tags = new List<TagHistoricalInfo>()
        {
            new TagHistoricalInfo()
            {
                Tagname = "Ping.State",
                ValueDataTypeId = (int)TagDataTypeEnum.Boolean,
                HistoricalTagConfigs = new List<HistoricalTagConfig>()
                {
                    new HistoricalTagConfig() { HistorizationTypeId = (int)HistoricalTypeEnum.Last }
                },
                IsNull = true
            },
            new TagHistoricalInfo()
            {
                Tagname = "Ping.ResponseTime",
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
