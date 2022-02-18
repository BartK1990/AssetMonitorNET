using AssetMonitorService.Monitor.SingletonServices.Historical;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Model.Historical
{
    public class AssetHistoricalDataColumnRelation
    {
        public int Id { get; private set; }

        public string TableName { get; set; }

        public Dictionary<HistoricalColumnInfo, TagHistoricalValue> Data { get; set; }

        public AssetHistoricalDataColumnRelation(int id)
        {
            this.Id = id;
            Data = new Dictionary<HistoricalColumnInfo, TagHistoricalValue>();
        }
    }
}
